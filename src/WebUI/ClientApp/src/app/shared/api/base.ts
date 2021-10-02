import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Directive } from '@angular/core';
import { AppInfo } from 'app/app.info';
import { ApiResponse, API_COMMUNICATION_ERROR, IApiErrorInfo, IApiErrorResponse } from 'app/models/api/api';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ApiCache } from './silly-cache';

function extractApiError(err: HttpErrorResponse) {
    const error = err && err.error;
    if (error && error.errors) {
        error.httpStatus = err.status;
        return error as IApiErrorResponse; // if there are errors => it is our API error
    }
    return undefined; // some unknown error
}


export const CACHE_1_HOUR = 60 * 60 * 1000;
export const CACHE_10_MIN = 10 * 60 * 1000;
export const CACHE_5_MIN = 5 * 60 * 1000;
export const CACHE_1_MIN = 60 * 1000;
export const CACHE_5_SEC = 5 * 1000;
export const NO_CACHE = 0;

export class ApiCallOptions {
    /**
     * User initiated request will always bypass caching as represent direct desire to get newest data;
     */
    userInitiated?: boolean;
    relatedCacheResources?: string[];
}

export class ApiRequest {
    url: string;
    cacheKeepMs: number;
}

@Directive()
// tslint:disable-next-line:directive-class-suffix
export abstract class ApiBase {

    private static options = {
        headers: new HttpHeaders(
            {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Credentials': 'true',
                'X-Requested-With': 'XMLHttpRequest',
            }),
        withCredentials: true
    };
    private static formOptions = {
        headers: new HttpHeaders({
            'Access-Control-Allow-Credentials': 'true',
            'X-Requested-With': 'XMLHttpRequest'
        }), withCredentials: true
    };

    public constructor(private _http: HttpClient, private _apiCache: ApiCache,
        private _appInfo: AppInfo) {
    }

    private dropRelatedCaches(options?: ApiCallOptions): void {
        if (options && options.relatedCacheResources) {
            options.relatedCacheResources.forEach(relatedCache => {
                this._apiCache.dropCache(relatedCache);
            });
        }
    }

    private processCallOptions(url: string, options?: ApiCallOptions): ApiCallOptions {
        const r = { ...options };
        if (!r.relatedCacheResources) {
            r.relatedCacheResources = [url];
        } else {
            r.relatedCacheResources.push(url);
        }
        return r;
    }

    protected doGet<T>(request: ApiRequest, options?: ApiCallOptions): Observable<ApiResponse<T>> {

        const url = this._appInfo.getFromApi(request.url);
        const getObs = this._http.get<ApiResponse<T>>(url, ApiBase.options).pipe(
            catchError(error => this.mapHttpErrorToApiOne(error))
        );

        options = options || {}; // default options if not provided

        if (request.cacheKeepMs !== 0) { // with cache version

            if (!options.userInitiated) {
                // User initiated request will always bypass caching as represent direct desire to get newest data;
                const inCache = this._apiCache.get<T>(request.url);
                if (inCache) return inCache;
            }

            return getObs.pipe(
                tap((res: ApiResponse<T>) => {
                    if (!res) console.error(`Method Get by URL: ${url} returns null`);
                    this._apiCache.put(request.url, res, request.cacheKeepMs);
                })
            );

        }

        return getObs;
    }

    protected mapHttpErrorToApiOne(error: HttpErrorResponse) {
        const apiError = extractApiError(error);
        if (apiError) return throwError(apiError); // truthy the api error here
        console.error('Unknown API service error occurred', error);
        const message = error && (error.message || error.statusText || error.toString()) || 'Communication Error';
        const errorMsg: IApiErrorInfo = {
            message,
            field: undefined,
            code: API_COMMUNICATION_ERROR,
            details: undefined
        };
        const apiErrorWrap: IApiErrorResponse = {
            errors: [errorMsg],
            wasConnectionError: true,
            httpStatus: error.status
        };
        return throwError(apiErrorWrap);
    }

    protected doPostWithId<T>(urlStr: string, id: string | number, data: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        return this.callPost(urlStr + '/' + id, data, this.processCallOptions(urlStr, options));
    }

    protected doPost<T>(urlStr: string, data: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        return this.callPost(urlStr, data, this.processCallOptions(urlStr, options));
    }

    protected doPutWithId<T>(urlStr: string, id: string | number, data?: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        return this.callPut(urlStr + '/' + id, data, this.processCallOptions(urlStr, options));
    }

    protected doPut<T>(urlStr: string, data: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        return this.callPut(urlStr, data, this.processCallOptions(urlStr, options));
    }


    protected doDelete<T>(urlStr: string, id: string | number, startTime?: number, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        const suffix = startTime ? '/' + startTime : '';
        const url = this._appInfo.getFromApi(urlStr + '/' + id + suffix);
        return this._http.delete<ApiResponse<T>>(url, ApiBase.options)
            .pipe(
                tap((res: ApiResponse<T>) => {
                    if (!res) console.error(`Method Delete by URL: ${url} returns null`);
                    this._apiCache.dropCache(urlStr);
                    this.dropRelatedCaches(options);
                }),
                catchError(error => this.mapHttpErrorToApiOne(error))
            );
    }

    private callPost<T>(urlStr: string, data: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        const url = this._appInfo.getFromApi(urlStr);
        return this._http.post<ApiResponse<T>>(url, JSON.stringify(data), ApiBase.options)
            .pipe(tap((res: ApiResponse<T>) => {
                if (!res) console.error(`Method Post by URL: ${url} returns null`);
                this.dropRelatedCaches(options);
            }),
                catchError(error => this.mapHttpErrorToApiOne(error)));
    }

    private callPut<T>(urlStr: string, data: any, options?: ApiCallOptions): Observable<ApiResponse<T>> {
        const url = this._appInfo.getFromApi(urlStr);
        return this._http.put<ApiResponse<T>>(url, (data || data === 0) && JSON.stringify(data), ApiBase.options)
            .pipe(tap((res: ApiResponse<T>) => {
                if (!res) console.error(`Method Put by URL: ${url} returns null`);
                this.dropRelatedCaches(options);
            }),
                catchError(error => this.mapHttpErrorToApiOne(error)));
    }


    protected doPutForm<T>(urlStr: string, data: FormData): Observable<ApiResponse<T>> {
        const url = this._appInfo.getFromApi(urlStr);
        return this._http.put<ApiResponse<T>>(url, data, ApiBase.formOptions).pipe(
            catchError(error => this.mapHttpErrorToApiOne(error))
        );
    }


}
