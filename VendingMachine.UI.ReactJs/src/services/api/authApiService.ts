import { defaultHttpInterceptor } from './httpInterceptor';
import BaseApiService from './baseApiService';
import { tryTypedPromise } from './httpExtensions';

import { ILoginRequest, ILoginResponse, IRegisterRequest, IRegisterResponse } from './dto/account';

export class AccountApiService extends BaseApiService {
  constructor() {
    super('auth', defaultHttpInterceptor('auth'));
  }

  login = (requst: ILoginRequest): Promise<ILoginResponse> => {
    const promise = this.post<ILoginRequest, ILoginResponse>(requst, 'login');
    return tryTypedPromise(promise);
  };

  register = (requst: IRegisterRequest): Promise<IRegisterResponse> => {
    const promise = this.post<IRegisterRequest, IRegisterResponse>(requst, 'register');
    return tryTypedPromise(promise);
  };
}

export default new AccountApiService();
