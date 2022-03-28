import React from 'react';
import cuid from 'cuid';
import { Form, FormikProps, Field, withFormik, ErrorMessage } from 'formik';
import { compose } from 'redux';
import { connect } from 'react-redux';
//import { push } from 'connected-react-router';

import { createArticleAsync, updateArticleAsync } from '../store/actions';
import { Article } from '../store/types';
import { RootState } from 'typesafe-actions';
// import { getPath } from '../../../router-paths';

type FormValues = Pick<Article, 'title' | 'content'> & {};

// const dispatchProps = {
//   createArticle: (values: FormValues) =>
//     createArticleAsync.request({
//       id: cuid(),
//       ...values,
//     }),
//   updateArticle: (values: Article) =>
//     updateArticleAsync.request({
//       ...values,
//     }),
//   redirectToListing: () => { /*push('/')*/  } ,
// };

type Props = {
  article?: Article;
  isSubmitting?: boolean; 
  isDirty?: boolean;
};

class InnerForm extends React.Component<Props> {
  
  render() {
    const { isSubmitting = false, isDirty = false } = this.props;

    return (
      <Form>
        <div>
          <label htmlFor="title">Title</label>
          <br />
          <Field
            name="title"
            placeholder="Title"
            component="input"
            type="text"
            required
            autoFocus
          />
          <ErrorMessage name="title" />
        </div>

        <div>
          <label htmlFor="title">Content</label>
          <br />
          <Field
            name="content"
            placeholder="Article content"
            component="textarea"
            required
            type="text"
          />
          <ErrorMessage name="content" />
        </div>

        <button type="submit" disabled={!isDirty || isSubmitting}>
          Submit
        </button>
      </Form>
    );
  }
}

const mapStateToProps = (state: RootState, ownProps: Props) => ({
  // article: state.articles.articles.find(
  //   i => i.id === ownProps.match.params.articleId
  // )
});

export default connect(mapStateToProps)(InnerForm);

// export default compose(
//   connect(
//     null,
//     dispatchProps
//   ),
//   withFormik<Props, FormValues>({
//     enableReinitialize: true,
//     // initialize values
//     mapPropsToValues: ({ article: data }) => ({
//       title: (data && data.title) || '',
//       content: (data && data.content) || '',
//     }),
//     handleSubmit: (values, form) => {
//       if (form.props.article != null) {
//         form.props.updateArticle({ ...form.props.article, ...values });
//       } else {
//         form.props.createArticle(values);
//       }

//       form.props.redirectToListing();
//       form.setSubmitting(false);
//     },
//   })
// )(InnerForm);
