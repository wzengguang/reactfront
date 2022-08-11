import axios from 'axios';
import qs from 'querystring';
import { getToken, TokenKey } from './storage';
import { redirectLogin } from './history';

const CancelToken = axios.CancelToken;
const source = CancelToken.source();

const request = axios.create({
    baseURL: '',
    timeout: 30000,
    cancelToken: source.token,
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
        'Cache-Control': 'no-cache',
    },
});

request.interceptors.request.use(
    function (config) {
        config.headers = {
            [TokenKey]: getToken() || '',
            ...config.headers,
        };

        config.cancelToken = source.token;
        config.xsrfCookieName = 'XSRF-TOKEN',
        config.xsrfHeaderName = 'X-XSRF-TOKEN',
        config.transformRequest = [
            function (data) {
                if (data instanceof FormData) {
                    return data;
                }
                return qs.stringify(data);
            },
        ];
        return config;
    },

    function (error) {
        return Promise.reject(error);
    },
);

request.interceptors.response.use(
    function(response) {
        return response;
    },

    function(error) {
        if (error.response && error.response.status === 601) {
            redirectLogin();
        }

        return Promise.reject(error.response);
    }
);

export default request;

export const get = request.get;
export const post = request.post;
export const put = request.put;
export const del = request.delete;

export const cancelSource = source;

export interface ResponseData<T extends unknown> {
    code: number,
    data: T,
    msg: string;

    errorCode: string;
    message: string;
    success: boolean;
}

export interface ResponsePageResult<T extends unknown> {
    limit: number,
    nextPage: boolean;
    page: number;
    results: T[];
    total: number;
}

export type ResponseListResult<T extends unknown> = {
    page: {
        page: number;
        limit: number;
        total: number;
    };
    results?: T[];
};