import { AxiosPromise, AxiosRequestConfig } from 'axios';
import { HttpInterceptor } from './httpInterceptor';

export class HttpClient {
  private _httpInterceptor: HttpInterceptor;
  constructor(httpInterceptor?: HttpInterceptor, module?: string) {
    if (httpInterceptor && !httpInterceptor.isEmpty) {
      this._httpInterceptor = httpInterceptor;
    } else {
      this._httpInterceptor = new HttpInterceptor(module);
    }
  }

  public post<TRequest, TResponse>(
    url: string,
    request: TRequest,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    return this._httpInterceptor.value.post(url, request, config);
  }
  public get<TResponse>(
    url: string,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    return this._httpInterceptor.value.get(url, config);
  }
  public put<TResponse>(
    url: string,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    return this._httpInterceptor.value.put(url, config);
  }
  public delete<TResponse>(
    url: string,
    config?: AxiosRequestConfig
  ): AxiosPromise<TResponse> {
    return this._httpInterceptor.value.delete(url, config);
  }
}
