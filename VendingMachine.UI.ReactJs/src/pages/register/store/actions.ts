import { IRegisterRequest, IRegisterResponse } from 'src/services/api/dto/account';
import { createAsyncAction } from 'typesafe-actions';

export const registerUserAsync = createAsyncAction(
  'REGISTER_USER_REQUEST',
  'REGISTER_USER_SUCCESS',
  'REGISTER_USER_FAILURE'
)<IRegisterRequest, IRegisterResponse, string>();
