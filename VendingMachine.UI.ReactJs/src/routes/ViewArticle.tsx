import React, { Component } from 'react';
import { connect } from 'react-redux';

import ArticleView from '../pages/articles/components/ArticleView';
import Main from '../layouts/Main';
import BackLink from '../common/components/BackLink';
import { RootState } from 'typesafe-actions';

type OwnProps = {
};

type Props = ReturnType<typeof mapStateToProps>;

class ViewArticle extends Component<Props> {
  // if (!article) {
  //   return <div>'Article doesn\'t exist'</div>;
  // }

  render() {
    return (
      <Main renderActionsMenu={() => <BackLink />}>
        {/* <ArticleView article={null} /> */}
      </Main>
    );
  }  
};


const mapStateToProps = (state: RootState, ownProps: OwnProps) => ({
  // article: state.articles.articles.find(
  //   i => i.id === ownProps.match.params.articleId
  // ),
});

export default connect(mapStateToProps)(ViewArticle);
