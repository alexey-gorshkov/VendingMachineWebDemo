// import { StateType, ActionType } from 'typesafe-actions';
// import { Epic } from 'redux-observable';
// import { Services } from 'src/services/types';

// export type Store = StateType<typeof import('./index').default>;
// export type RootState = StateType<ReturnType<typeof import('./root-reducer').default>>;
// export type RootAction = ActionType<typeof import('./root-action').default>;
// export type RootEpic = Epic<RootAction, RootAction, RootState, Services>;

// interface Types {
//     RootAction: ActionType<typeof import('./root-action').default>;
// }

import { StateType, ActionType } from 'typesafe-actions';

declare module 'typesafe-actions' {
  export type Store = StateType<typeof import('./index').default>;

  export type RootState = StateType<typeof import('./root-reducer').default>;

  export type RootAction = ActionType<typeof import('./root-action').default>;
  interface Types {
    RootAction: RootAction;
  }
}
