import React from 'react';

import ArticleList from '../pages/articles/components/ArticleList';
import ArticleActionsMenu from '../pages/articles/components/ArticleActionsMenu';
import Main from '../layouts/main';

export default class Home extends React.Component {
  
  render() {
    return <Main renderActionsMenu={() => <ArticleActionsMenu />}>
      <ArticleList />
    </Main>
  } 
};
