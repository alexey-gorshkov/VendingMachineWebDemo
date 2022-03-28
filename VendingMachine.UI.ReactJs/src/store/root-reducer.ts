import { combineReducers } from 'redux';
// import { connectRouter } from 'connected-react-router';
//import { routerReducer } from 'react-router-redux';
import { History } from 'history';

import articlesReducer from '../pages/articles/store/reducer';

const rootReducer = (history: History) => combineReducers({
  //router: routerReducer,
  articles: articlesReducer
});

export default rootReducer;
