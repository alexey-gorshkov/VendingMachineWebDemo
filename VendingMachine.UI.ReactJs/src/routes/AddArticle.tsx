import React from 'react';

import ArticleForm from '../pages/articles/components/ArticleForm';
import Main from '../layouts/Main';
import BackLink from '../common/components/BackLink';

export default class extends React.Component<any> {
  render() {
    return (
      <Main renderActionsMenu={() => <BackLink />}>
        <ArticleForm />
      </Main>
      )
  }
}
