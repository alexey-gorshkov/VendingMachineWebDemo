import React, { Component } from 'react';

import { connect } from 'react-redux';
import { useParams } from "react-router-dom";
import BackLink from 'src/common/components/BackLink';
import Main from 'src/common/layouts/main';
import { RootState } from 'typesafe-actions';

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
