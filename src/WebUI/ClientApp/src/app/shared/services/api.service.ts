
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppInfo } from 'app/app.info';
import { ApiResponse } from 'app/models/api/api';
import { IUserSession } from 'app/models/api/user';
import { Observable } from 'rxjs';
import { ApiBase } from '../api/base';
import { ApiCache } from '../api/silly-cache';
import { NO_CACHE } from './../api/base';

@Injectable()
export class ApiService extends ApiBase {

    constructor(http: HttpClient, cache: ApiCache, appInfo: AppInfo) {
        super(http, cache, appInfo);
    }

    getCurrentAccount(): Observable<ApiResponse<IUserSession>> {
        return this.doGet({ url: 'account/current', cacheKeepMs: NO_CACHE });
    }

}
