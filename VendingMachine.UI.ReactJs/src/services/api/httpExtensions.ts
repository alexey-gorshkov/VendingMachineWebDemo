import { AxiosResponse, AxiosPromise } from 'axios';
import { IHttpResponse } from './httpResponse';

const isSuccessResponse = <TResponse>(response: AxiosResponse<TResponse>) => {
  if (response && response.status === 200) return true;
  return false;
};

export type HttpUrlParam = {
  name: string;
  value: any;
};

export const getHttpUrlParams = (params: HttpUrlParam[]) => {
  if (params.length) {
    const result = `${params
      .filter((p) => p.value !== undefined && p.value !== null)
      .map((p) => `${p.name}=${p.value}`)
      .join('&')}`;
    return result.length > 0 ? `?${result}` : '';
  }
  return '';
};

export function tryPromise(promise: Promise<any>) {
  return promise.then((response: AxiosResponse<any>) => {
    let httpResponse: IHttpResponse<any> = {
      data: response.data,
      isSuccess: isSuccessResponse(response),
    };
    return [undefined, httpResponse];
  });
  // .catch((err) => {
  //     throw err;
  // });
}

export const tryTypedPromise = <TData>(
  promise: AxiosPromise<TData | null>
): Promise<TData | null> => {
  return promise
    .then((response) => response?.data)
    .catch((err) => {
      console.log(err); // logging or toastr
      throw err;
      //return undefined;
    });
};

export const tryTypedResponse = <TData>(
  promise: AxiosPromise<TData>
): Promise<TData> => {
  return promise
    .then((response) => response)
    .catch((err) => {
      console.log(err); // logging or toastr
      throw err;
      return undefined;
    });
};
