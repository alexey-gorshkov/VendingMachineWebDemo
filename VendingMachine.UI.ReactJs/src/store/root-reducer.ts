import { combineReducers } from 'redux';
// import { connectRouter } from 'connected-react-router';
//import { routerReducer } from 'react-router-redux';
import { History } from 'history';

import articlesReducer, { ArticleState } from '../pages/articles/store/reducer';
import { Reducer } from 'typesafe-actions';

const rootReducer = combineReducers({
  //router: routerReducer,
  articles: articlesReducer
});

export default rootReducer;
