import React from 'react';

import ArticleList from './components/articleList';
import ArticleActionsMenu from './components/ArticleActionsMenu';
import Main from '../../common/layouts/main';

export default class Home extends React.Component {
  
  render() {
    return <Main renderActionsMenu={() => <ArticleActionsMenu />}>
      <ArticleList />
    </Main>
  } 
};
