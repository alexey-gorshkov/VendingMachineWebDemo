import { combineReducers } from 'redux';
// import { connectRouter } from 'connected-react-router';
//import { routerReducer } from 'react-router-redux';
import { History } from 'history';

import articlesReducer from '../pages/articles/store/reducer';
import loginReducer from '../pages/login/store/reducer';

const rootReducer = combineReducers({
  //router: routerReducer,
  articles: articlesReducer,
  login: loginReducer
});

export default rootReducer;
