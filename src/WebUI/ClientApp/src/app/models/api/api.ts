export interface IApiErrorInfo {
    code: number;
    message: string;
    field: string;
    details: string;
    data?: any;
}

export const API_COMMUNICATION_ERROR = -1;

export interface IApiErrorResponse {
    errors?: IApiErrorInfo[];
    wasConnectionError?: boolean;
    httpStatus?: number;
}

export class ApiResponse<T> {

    item?: T;
    items?: T[];

    cacheTime?: number;
    cacheEndTime?: number;

    constructor(items?: T[]) {
        this.items = items;
    }
}

