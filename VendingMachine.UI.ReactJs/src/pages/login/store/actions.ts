import { ILoginRequest, ILoginResponse } from 'src/services/api/dto/account';
import { createAsyncAction } from 'typesafe-actions';

export const loginUserAsync = createAsyncAction(
  'LOGIN_USER_REQUEST',
  'LOGIN_USER_SUCCESS',
  'LOGIN_USER_FAILURE'
)<ILoginRequest, ILoginResponse, string>();
