import { defaultHttpInterceptor } from './httpInterceptor';
import BaseApiService from './baseApiService';
import { tryTypedPromise } from './httpExtensions';

import { ILoginRequest, ILoginResponse } from './dto/account';

export class AccountApiService extends BaseApiService {
  constructor() {
    super('auth', defaultHttpInterceptor('auth'));
  }

  login = (requst: ILoginRequest): Promise<ILoginResponse> => {
    const promise = this.post<ILoginRequest, ILoginResponse>(requst, '/');
    return tryTypedPromise(promise);
  };
}

export default new AccountApiService();
