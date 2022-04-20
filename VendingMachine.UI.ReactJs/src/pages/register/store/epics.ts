import { Epic } from 'redux-observable';
import { from, of, tap } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';
import { isActionOf, RootAction, RootState, Services } from 'typesafe-actions';
import { history } from 'src/store';

import {
  registerUserAsync
} from './actions';

type RootEpic = Epic<RootAction, RootAction, RootState, Services>;

export const registerUserEpic: RootEpic = (action$, state$, { api }) => 
  action$.pipe(
    filter(isActionOf(registerUserAsync.request)),
    switchMap(action =>
      from(api.auth.register(action.payload)).pipe(
        map(registerUserAsync.success),
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
        catchError(message => of(registerUserAsync.failure(message)))
      )
    )
  );
