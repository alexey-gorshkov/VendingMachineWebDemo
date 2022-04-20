// import { routerActions } from 'connected-react-router';
import * as articlesActions from '../pages/articles/store/actions';
import * as loginActions from '../pages/login/store/actions';
import * as registerActions from '../pages/register/store/actions';

export default {
  // router: routerActions,
  articles: articlesActions,
  login: loginActions,
  register: registerActions,
};
