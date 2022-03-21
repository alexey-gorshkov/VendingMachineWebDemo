import { combineReducers } from 'redux';
// import { connectRouter } from 'connected-react-router';
import { History } from 'history';

import articles from '../pages/articles/store/reducer';

const rootReducer = (history: History) =>
  combineReducers({
    //router: connectRouter(history),
    articles,
  });

export default rootReducer;
