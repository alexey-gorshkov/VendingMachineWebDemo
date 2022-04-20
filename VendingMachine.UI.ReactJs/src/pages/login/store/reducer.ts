import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import { loginUserAsync } from './actions';

export const isLoading = createReducer(false as boolean)
  .handleAction([loginUserAsync.request], (state, action) => true)
  .handleAction([loginUserAsync.success, loginUserAsync.failure], (state, action) => false );

export const isLoggedIn = createReducer(false as boolean)
  .handleAction([loginUserAsync.request], (state, action) => false)
  .handleAction([loginUserAsync.success], (state, action) => action.payload.isSuccess ); 

const loginReducer = combineReducers({
  isLoading,
  isLoggedIn
});

export default loginReducer;
export type LoginState = ReturnType<typeof loginReducer>;
