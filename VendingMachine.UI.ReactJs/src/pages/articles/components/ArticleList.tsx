import React from 'react';
import { connect } from 'react-redux';

import selectors from '../store/selectors';
import ArticleListItem from './ArticleListItem';
import { Article } from '../store/types';
import { ArticleState } from '../store/reducer';
import { RootState } from 'typesafe-actions';

//type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps;

interface Props {
  isLoading: boolean;
  articles: Article[];
}

class ArticleList extends React.Component<Props> {

  getStyle = (): React.CSSProperties => ({
    textAlign: 'left',
    margin: 'auto',
    maxWidth: 500,
  });

  render() {
    const { isLoading, articles } = this.props;
    if (isLoading) {
      return <p style={{ textAlign: 'center' }}>Loading articles...</p>;
    }

    if (articles.length === 0) {
      return (
        <p style={{ textAlign: 'center' }}>
          No articles yet, please create new ....
        </p>
      );
    }

    return (
      <ul style={this.getStyle()}>
        {articles.map(article => (
          <li key={article.id}>
            <ArticleListItem article={article} />
          </li>
        ))}
      </ul>
    );
  }
};

const mapStateToProps = (state: RootState) => ({
  isLoading: selectors.isLoadingArticles(state.articles),
  articles: selectors.getArticles(state.articles),
});
const dispatchProps = {};

export default connect(mapStateToProps, dispatchProps)(ArticleList);
