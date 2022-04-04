import { Epic } from 'redux-observable';
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';
import { isActionOf, RootAction, RootState, Services } from 'typesafe-actions';

import {
  loginUserAsync
} from './actions';

type RootEpic = Epic<RootAction, RootAction, RootState, Services>;

export const loginUserEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(loginUserAsync.request)),
    switchMap(action =>
      from(api.auth.login(action.payload)).pipe(
        map(loginUserAsync.success),
        catchError(message => of(loginUserAsync.failure(message)))
      )
    )
  );
