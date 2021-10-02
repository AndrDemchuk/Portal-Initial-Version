import { Injectable } from '@angular/core';
import { ApiResponse } from 'app/models/api/api';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { Dictionary, IDictionary } from './../../models/dictionary';

export class CachedRecord<T> {
    value: T;
    size: number; // size in bytes 4bites
    expires: number; // Java Ms
    lastRequest: number; // Java Ms
    requestCount: number = 0; // may be used later on in advanced cleanup scenarios
}

const typeSizes = {
    undefined: () => 0,
    boolean: () => 4,
    number: () => 8,
    string: item => 2 * item.length,
    object: item => !item ? 0 : Object
        .keys(item)
        .reduce((total, key) => sizeOf(key) + sizeOf(item[key]) + total, 0)
};

const sizeOf = value => typeSizes[typeof value](value);

/**
 * SillyCache with expiration verification and total DATA size limits.
 * This cache does not calculate its own memory footprint in totals calculation.
 * This can be resolved later on
 */
export class SillyCache<T> {

    /**
     * Limit for single object in percents. If it is too big it wont be cached.
     */
    private _singeObjectLimitFactor = 0.20; //
    private _cacheLimit = 10 * 1024 * 1024;
    private _singeObjectLimit: number; //
    private _totalSize = 0;
    private _cacheTimeMs = 60 * 1000; // 60 sec.
    private _canOveruseSize: boolean = false;
    private _cache: IDictionary<CachedRecord<any>> = new Dictionary<CachedRecord<any>>();

    public diagnostic: boolean = false;

    public get cacheLimit() { return this._cacheLimit; }
    public get size() { return this._totalSize; }
    public get canOveruseSize() { return this._canOveruseSize; }
    public get singleObjectLimit() { return this._singeObjectLimit; }

    public get keys() {
        const keys = Object.keys(this._cache);
        return keys;
    }

    public constructor(canOveruseSize: boolean) {
        this._canOveruseSize = canOveruseSize;
        this._singeObjectLimit = this._singeObjectLimitFactor * this._cacheLimit;
    }

    public get(urlStr: string): T {
        return this.getFromCache(urlStr);
    }

    public put(key: string, value: T, keepMs?: number): CachedRecord<T> {
        const valueSize = sizeOf(value);

        keepMs = keepMs || this._cacheTimeMs;

        if (this.diagnostic) {
            console.log(`Cache: adding key ${key} with lifetime of ${keepMs / 1000} s.`);
        }

        if (keepMs <= 0) return; // it is going to expire anyway.

        if (this._singeObjectLimit <= valueSize) return; // we do not store value greater that max limit
        this.removeAt(key); // remove old value as incorrect in any case;
        if (!this.fulfillSizeLimits(valueSize)) return; // we can not fit this object in our store, even after cleanup

        const cached = new CachedRecord<T>();
        cached.expires = Date.now() + keepMs;
        cached.value = value;
        cached.size = valueSize;

        this._cache[key] = cached;

        if (this.diagnostic) {
            console.log(`Cache: used ${this._totalSize / 1024} of ${this._cacheLimit / 1024} Kb`);
        }

        return undefined;
    }

    public reset() {
        this._cache = new Dictionary<CachedRecord<any>>();
        this._totalSize = 0;
    }

    private getFromCache(key: string) {
        const cached = this._cache[key];
        if (!cached) return false;
        const now = Date.now();
        if (cached.expires > now) {
            cached.lastRequest = now;
            cached.requestCount++;

            console.log(`Cache: restored ${key} from cache. Obj stats:`);
            console.log(`\tsize: ${cached.size},
                requested: ${cached.requestCount},
                remainingTime: ${(cached.expires - now) / 1000} s.`);

            return cached.value;
        }
        this.removeAt(key); // remove outdated;
        return undefined;
    }

    public removeAll(keys: string[]) {
        keys.forEach(key => this.removeAt(key));
    }

    public removeAt(key: string) {
        const cached = this._cache[key];
        this._cache[key] = undefined; // in any case we get new value. reset cache for now;
        this._totalSize += cached && cached.size || 0;
    }

    private fulfillSizeLimits(valueSize: number): boolean {
        if (this._totalSize + valueSize > this._cacheLimit) {
            if (this.canOveruseSize) {
                this.markForCleanup();
                return true;
            } else {
                this.cleanUp();
                return this._totalSize + valueSize > this._cacheLimit;
            }
        }
        return true;
    }

    private markForCleanup() {
        setTimeout(() => this.cleanUp());
    }

    private cleanUp() {
        const currentSize = this._totalSize;
        const keys = Object.keys(this._cache);
        keys.forEach(key => this.removeAt(key));
        if (this.diagnostic) {
            const cleanedKb = (currentSize - this._totalSize) / 1024;
            console.log(`Cache: size after cleanup: ${this._totalSize}. Cleaned: ${cleanedKb} KB`);
        }
    }

}

@Injectable()
export class ApiCache {

    private _sillyCache: SillyCache<string>;
    public diagnostic: boolean = true;

    public constructor() {
        this._sillyCache = new SillyCache(false);
    }

    public get<T>(url: string): Observable<ApiResponse<T> | T> {
        const cached = this._sillyCache.get(url);
        if (cached) {
            const copy = JSON.parse(cached);
            return of(copy).pipe(delay(1));
        }
        return undefined;
    }

    public put<T>(url: string, result: ApiResponse<T>, keepMs?: number): CachedRecord<T> {
        if (!result) return;
        if (result.cacheEndTime) {
            keepMs = result.cacheEndTime - Date.now();
        }
        const copy = JSON.stringify(result);

        this._sillyCache.put(url, copy, keepMs);
    }

    /**
     * We may want to drop cache after user has added / deleted or updated related entities
     * @param urlStart related entity details
     */
    public dropCache(urlStart: string) {
        if (!urlStart) return;
        const dropPart = this._sillyCache.keys.filter(key => key.startsWith(urlStart));
        this._sillyCache.removeAll(dropPart);
    }

    public reset() {
        this._sillyCache.reset();
    }

}
