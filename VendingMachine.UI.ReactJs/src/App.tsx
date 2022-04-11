import * as React from "react";
import { Routes, Route } from 'react-router';
import { Provider } from "react-redux";
import store, { history } from './store';
import { getPath } from './router-paths';

import Home from './pages/articles/articleListPage';
import { hot } from 'react-hot-loader/root'
import { BrowserRouter, Navigate, unstable_HistoryRouter as HistoryRouter } from "react-router-dom";
import PrivateRoute from "./security/privateRoute";
import RestrictedRoute from "./security/restrictedRoute";
import LoginPage from "./pages/login/loginPage";
import RegisterPage from "./pages/register/registerPage";

class App extends React.Component { 

  render() {
    return (
      <Provider store={store}>
        <HistoryRouter history={history}>
          <Routes>

            <Route path="/" element={
              <PrivateRoute>
                <Home />
              </PrivateRoute>
            } />
            <Route path="/login" element={
              <RestrictedRoute>
                <LoginPage />
              </RestrictedRoute>
            } />
            <Route path="/register" element={<RegisterPage />} />

            {/* <Route path={getPath('addArticle')} element={<AddArticle />} />
            <Route
              path={getPath('editArticle', ':articleId')}
              element={<EditArticle />}
            />
            <Route              
              path={getPath('viewArticle', ':articleId')}
              element={<ViewArticle />}
            /> */}
            
            <Route element={() => <div>Page not found!</div>} />
          </Routes>
        </HistoryRouter>
         
        {/* </ConnectedRouter> */}
      </Provider>
    );
  }
}

export default App;
