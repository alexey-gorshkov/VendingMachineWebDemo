import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import config from 'src/config';
import securityUtils from 'src/security/securityUtils';
import * as toast from 'src/services/toast-service';

const defaultErrorInterceptor = (error): Promise<never> => {
  const response = error.response;
  const errorCode = error.code;

  if (error instanceof axios.Cancel) {
    return; // Promise.reject(error.message);
  }
  if (!response && !errorCode && error instanceof Error) {
    toast.error('Unknown error');
    return Promise.reject('Unknown error');
  }
  if (!response && error instanceof Error) {
    return Promise.reject();
  }

  if (response && response.status !== 200) {
    if (response.status === 404) {
      toast.error('404 not found');
      return Promise.reject('Status is not success');
    } else {
      //show message for dev
      if (response.data && response.data.Message && config.isDev) {
        toast.error('Error');
      } else {
        toast.error('Error');
      }
      return Promise.reject('Status is not success');
    }
  }
  if (errorCode === 'ECONNABORTED') {
    toast.error('Connection timeout');
    return Promise.reject('Connection timeout');
  }
  toast.error('Unknown error');
  return Promise.reject('Unknown error');
};

export type IHttpErrorHandler = (response) => Promise<never>;

export const defaultHttpInterceptor = (module: string) => {
  let result = new HttpInterceptor(module);

  var currentResult = result
    .withAuthHeader()
    .withErrorHandler(defaultErrorInterceptor);
  if (config.isDev) {
    currentResult.withCustomHeaders(config.settings.user);
  }
  return currentResult;
};

export const customHttpInterceptor = (
  errorHandler: IHttpErrorHandler,
  module: string
) => {
  let result = new HttpInterceptor(module);
  return result.withErrorHandler(errorHandler);
};

export class HttpInterceptor {
  private _instance: AxiosInstance;
  private _errorHandler: IHttpErrorHandler;
  constructor(module: string) {
    this._instance = axios.create({
      baseURL: config.apiUrl[module],
      timeout: 600000,
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json; charset=utf-8',
      },
    });
    this._instance.defaults.baseURL = config.apiUrl[module];
  }

  public get isEmpty(): boolean {
    if (!this._errorHandler) return true;
    return false;
  }

  public withErrorHandler(handler: IHttpErrorHandler): HttpInterceptor {
    this._errorHandler = handler;
    return this;
  }

  public withCustomHeaders(headers: {
    [index: string]: string;
  }): HttpInterceptor {
    this._instance.interceptors.request.use((config: AxiosRequestConfig) => {
      const isAuthenticated = securityUtils.isAuthenticated();
      if (isAuthenticated) {
        Object.keys(headers).forEach((key) => {
          config.headers[key] = encodeURI(headers[key]);
        });
      }
      return config;
    });
    return this;
  }

  public withAuthHeader(): HttpInterceptor {
    this._instance.interceptors.request.use((config: AxiosRequestConfig) => {
      let token = securityUtils.getToken();
      if (token && !securityUtils.isExpired()) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });
    return this;
  }

  public get value(): AxiosInstance {
    let self = this;
    this._instance.interceptors.response.use(
      (response) => response,
      (error) => {
        if (self._errorHandler) return self._errorHandler(error);
        return Promise.reject(error);
      }
    );
    return this._instance;
  }
}
