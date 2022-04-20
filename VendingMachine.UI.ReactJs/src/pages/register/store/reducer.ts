import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import { registerUserAsync } from './actions';

export const isLoading = createReducer(false as boolean)
  .handleAction([registerUserAsync.request], (state, action) => true)
  .handleAction([registerUserAsync.success, registerUserAsync.failure], (state, action) => false );

export const isLoggedIn = createReducer(false as boolean)
  .handleAction([registerUserAsync.request], (state, action) => false)
  .handleAction([registerUserAsync.success], (state, action) => action.payload.isSuccess ); 

const registerReducer = combineReducers({
  isLoading,
  isLoggedIn
});

export default registerReducer;
export type RegisterState = ReturnType<typeof registerReducer>;
