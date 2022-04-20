import { Epic } from 'redux-observable';
import { from, of, tap } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';
import { isActionOf, RootAction, RootState, Services } from 'typesafe-actions';
import { history } from 'src/store';

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
        tap((data) => {
          const response = data.payload;
          if (!response.isSuccess) return;
          localStorage.setItem('isLoggedIn', 'true');
          localStorage.setItem('token', response.token);
          const expiresDate = new Date();
          expiresDate.setSeconds(
            expiresDate.getSeconds() + response.expiresIn
          );
          localStorage.setItem('expiresDate', expiresDate.toString());

          history.replace("/");
        }),
        catchError(message => of(loginUserAsync.failure(message)))
      )
    )
  );
