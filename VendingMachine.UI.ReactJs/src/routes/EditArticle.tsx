import React, { Component } from 'react';

import ArticleForm from '../pages/articles/components/ArticleForm';
import Main from '../layouts/Main';
import BackLink from '../common/components/BackLink';
import { connect } from 'react-redux';
import { RootState } from 'src/store/types';
import { useParams } from "react-router-dom";

type Props = {
};

class EditArticle extends Component<Props> {
  
  render() {
    const { id } = useParams();

    const article = 1;

    return (
      <Main renderActionsMenu={() => <BackLink />}>
        {/* <ArticleForm article={article} /> */}
      </Main>
    )
  }    
};

const mapStateToProps = (state: RootState) => ({
    
});

export default connect(mapStateToProps)(EditArticle);
