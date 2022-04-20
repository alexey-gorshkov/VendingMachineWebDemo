import { combineReducers } from 'redux';

import articlesReducer from '../pages/articles/store/reducer';
import loginReducer from '../pages/login/store/reducer';
import registerReducer from '../pages/register/store/reducer';

const rootReducer = combineReducers({
  articles: articlesReducer,
  login: loginReducer,
  register: registerReducer,
});

export default rootReducer;
