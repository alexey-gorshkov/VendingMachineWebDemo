import * as React from "react";
import { Routes, Route } from 'react-router';
import { Provider } from "react-redux";
//import { ConnectedRouter } from "connected-react-router";
import store, { history } from './store';

import AddArticle from './routes/AddArticle';
import EditArticle from './routes/EditArticle';
import ViewArticle from './routes/ViewArticle';
import { getPath } from './router-paths';

import Home from './routes/Home';
import { hot } from 'react-hot-loader/root'
import { BrowserRouter } from "react-router-dom";

class App extends React.Component {
  render() {
    return (
      <Provider store={store}>
        {/* <ConnectedRouter history={history}> */}
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path={getPath('addArticle')} element={<AddArticle />} />
            <Route
              path={getPath('editArticle', ':articleId')}
              element={<EditArticle />}
            />
            <Route              
              path={getPath('viewArticle', ':articleId')}
              element={<ViewArticle />}
            />
            <Route element={() => <div>Page not found!</div>} />
          </Routes>
        </BrowserRouter>
         
        {/* </ConnectedRouter> */}
      </Provider>
    );
  }
}

export default App;
