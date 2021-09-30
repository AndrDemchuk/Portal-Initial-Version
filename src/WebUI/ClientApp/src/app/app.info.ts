import { environment } from "src/environments/environment";

export class AppInfo {

    private static _baseUrl: string;
    public static getBaseUrlStatic() {
        if (this._baseUrl) return this._baseUrl;
        const location = window.location;
    
        /* debug */
        if (environment.localApi) {
            return environment.localApi;
        }
    
        const url = location.protocol + '//' + location.host + '/';
        this._baseUrl = url;
        return url;
    }
}

