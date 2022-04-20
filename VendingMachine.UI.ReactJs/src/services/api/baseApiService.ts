import config from 'src/config';
import { HttpClient } from './httpClient';
import axios, { AxiosPromise, AxiosRequestConfig } from 'axios';
import { HttpInterceptor, defaultHttpInterceptor } from './httpInterceptor';
import securityUtils from 'src/security/securityUtils';

class BaseApiService {
  protected _config = config;
  protected _defaultTimeout: number = 5 * 60 * 1000;
  private _client: HttpClient;
  private _cancelTokens = {};
  private _module: string;

  constructor(module: string, interceptor?: HttpInterceptor) {
    this._module = module;
    let targetInterceptor = interceptor;
    if (interceptor === null || interceptor === undefined) {
      targetInterceptor = defaultHttpInterceptor(module);
    }
    this._client = new HttpClient(targetInterceptor);
  }

  getBaseUrl = () => {
    return securityUtils.isAuthenticated()
      ? config.apiUrl[this._module]
      : config.publicApiUrl[this._module];
  };

  // getUserId = () => {
  //     let token = securityUtils.getToken();
  //     if (token) {
  //         return token.profile.userId;
  //     }
  //     return 0;
  // };

  fillHeaders = (config) => {
    //let token = securityUtils.getToken();
    //config.headers = {
    //    ...config.headers,
    //'UserLocale': Languages[i18n.language]
    //};
    // if (token && token.profile) {
    //     const userId = this.getUserId();
    //     const login = token.profile.sub;
    //     config.headers = {
    //         ...config.headers,
    //         userId: userId,
    //         login: login
    //     };
    // }
  };

  protected get<TResponse>(
    urlPath: string,
    config?: AxiosRequestConfig,
    skipCancellation: boolean = false
  ): AxiosPromise<TResponse> {
    const newConfig = this.getConfig(config);
    if (!skipCancellation) {
      const token = this._cancelTokens[urlPath];
      if (typeof token != typeof undefined && token !== null) {
        token.cancel('Operation canceled due to new request.');
        this._cancelTokens[urlPath] = null;
      }
      this._cancelTokens[urlPath] = axios.CancelToken.source();
      newConfig.cancelToken = this._cancelTokens[urlPath].token;
    }
    this.fillHeaders(newConfig);
    return this._client.get<TResponse>(this.getBaseUrl() + urlPath, newConfig);
  }

  protected delete<TResponse>(
    urlPath: string,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    const token = this._cancelTokens[urlPath];
    if (typeof token != typeof undefined && token !== null) {
      token.cancel('Operation canceled due to new request.');
      this._cancelTokens[urlPath] = null;
    }
    this._cancelTokens[urlPath] = axios.CancelToken.source();
    const newConfig = this.getConfig(config);
    newConfig.cancelToken = this._cancelTokens[urlPath].token;
    this.fillHeaders(newConfig);
    return this._client.delete<TResponse>(
      this.getBaseUrl() + urlPath,
      newConfig
    );
  }
  protected put<TResponse>(
    urlPath: string,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    const token = this._cancelTokens[urlPath];
    if (typeof token != typeof undefined && token !== null) {
      token.cancel('Operation canceled due to new request.');
      this._cancelTokens[urlPath] = null;
    }
    this._cancelTokens[urlPath] = axios.CancelToken.source();
    const newConfig = this.getConfig(config);
    newConfig.cancelToken = this._cancelTokens[urlPath].token;
    this.fillHeaders(newConfig);
    return this._client.put<TResponse>(this.getBaseUrl() + urlPath, newConfig);
  }
  protected post<TRequest, TResponse>(
    request: TRequest,
    urlPath: string,
    config?: AxiosRequestConfig,
    skipToken: boolean = false
  ): AxiosPromise<TResponse> {
    const newConfig = this.getConfig(config);
    this.fillHeaders(newConfig);
    if (!skipToken) {
      const token = this._cancelTokens[urlPath];
      if (typeof token != typeof undefined && token !== null) {
        token.cancel('Operation canceled due to new request.');
        this._cancelTokens[urlPath] = null;
      }
      this._cancelTokens[urlPath] = axios.CancelToken.source();
      newConfig.cancelToken = this._cancelTokens[urlPath].token;
      return this._client.post<TRequest, TResponse>(
        this.getBaseUrl() + urlPath,
        request,
        newConfig
      );
    }
    return this._client.post<TRequest, TResponse>(
      this.getBaseUrl() + urlPath,
      request,
      newConfig
    );
  }

  protected postDownloadFile<TRequest, TResponse>(
    request: TRequest,
    url: string
  ): AxiosPromise<TResponse> {
    return this.post(request, url, {
      responseType: 'blob',
    });
  }

  protected getBlob(
    urlPath: string,
    timeout: number = this._defaultTimeout
  ): AxiosPromise<Blob> {
    return this.get<Blob>(urlPath, {
      responseType: 'blob',
      timeout: timeout,
    });
  }

  protected postBlob<T>(
    request: T,
    urlPath: string,
    timeout: number = this._defaultTimeout
  ): AxiosPromise<Blob> {
    return this.post<T, Blob>(request, urlPath, {
      responseType: 'blob',
      timeout: timeout,
    });
  }

  private getConfig(config: AxiosRequestConfig) {
    // if (_.isNull(config) || _.isUndefined(config)){
    //     return {}
    // }else return config;
    return { ...config };
  }
}

export default BaseApiService;
