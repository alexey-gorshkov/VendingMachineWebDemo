import React from 'react';

import ArticleList from '../pages/articles/components/ArticleList';
import ArticleActionsMenu from '../pages/articles/components/ArticleActionsMenu';
import Main from '../layouts/Main';

export default () => (
  <Main renderActionsMenu={() => <ArticleActionsMenu />}>
    <ArticleList />
  </Main>
);
