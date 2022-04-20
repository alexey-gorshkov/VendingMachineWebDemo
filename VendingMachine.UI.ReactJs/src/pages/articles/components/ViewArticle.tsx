import React, { Component } from 'react';
import { connect } from 'react-redux';
import BackLink from 'src/common/components/BackLink';
import Main from 'src/common/layouts/main';

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
