import React from 'react';

import ArticleForm from './ArticleForm';
import Main from '../../../common/layouts/main';
import BackLink from '../../../common/components/BackLink';
import { connect } from 'react-redux';
import { RootState } from 'typesafe-actions';

class AddArticle extends React.Component<any> {
  render() {
    return (
      <Main renderActionsMenu={() => <BackLink />}>
        <ArticleForm />
      </Main>
    )
  }
}
const mapStateToProps = (state: RootState) => ({
  // article: state.articles.articles.find(
  //   i => i.id === ownProps.match.params.articleId
  // ),
});

export default connect(mapStateToProps)(AddArticle);