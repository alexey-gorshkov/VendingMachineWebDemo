import { combineEpics } from 'redux-observable';

// import * as app from '../pages/app/epics';
import * as articles from '../pages/articles/store/epics';
import * as login from '../pages/login/store/epics';

export default combineEpics(...Object.values(login), ...Object.values(articles));
