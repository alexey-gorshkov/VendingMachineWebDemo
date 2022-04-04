import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import { loginUserAsync } from './actions';

export const isLoading = createReducer(false as boolean)
  .handleAction([loginUserAsync.request], (state, action) => true)
  .handleAction(
    [loginUserAsync.success, loginUserAsync.failure],
    (state, action) => false
  );

const loginReducer = combineReducers({
  isLoading
});

export default loginReducer;
export type LoginState = ReturnType<typeof loginReducer>;
