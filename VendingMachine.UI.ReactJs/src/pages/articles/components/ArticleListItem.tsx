import React from 'react';
import areEqual from 'fast-deep-equal';
import { connect } from 'react-redux';

import { deleteArticleAsync } from '../store/actions';
import { getPath } from '../../../router-paths';
// import FlexRow from '../../../common/components/FlexRow';
import { Link } from 'react-router-dom';
import { Article } from '../store/types';

const dispatchProps = {
  deleteArticle: deleteArticleAsync.request,
};

type Props = typeof dispatchProps & {
  article: Article;
};

const ArticleListItem = React.memo<Props>(({ article, deleteArticle }) => {
  return null;
  // return (
  //   <FlexRow>
  //     <div style={getStyle()}>{article.title}</div>
  //     <FlexRow itemsSpacing={20}>
  //       <Link to={getPath('viewArticle', article.id)}>View</Link>
  //       <Link to={getPath('editArticle', article.id)}>Edit</Link>
  //       <div
  //         className="link"
  //         onClick={() => deleteArticle(article)}
  //         style={{ color: 'darkred' }}
  //       >
  //         Delete
  //       </div>
  //     </FlexRow>
  //   </FlexRow>
  // );
}, areEqual);

const getStyle = (): React.CSSProperties => ({
  overflowX: 'hidden',
  textOverflow: 'ellipsis',
  width: '300px',
});

export default connect(
  null,
  dispatchProps
)(ArticleListItem);
