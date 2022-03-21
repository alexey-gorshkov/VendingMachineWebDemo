import { combineEpics } from 'redux-observable';

import * as app from '../pages/app/epics';
import * as articles from '../pages/articles/store/epics';

export default combineEpics(...Object.values(app), ...Object.values(articles));
