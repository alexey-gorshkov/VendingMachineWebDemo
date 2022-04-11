import React, { Component } from 'react';
import {
  Formik,
  Form,
  FormikProps,
  Field,
  withFormik,
  ErrorMessage,
  FormikHelpers  
} from 'formik';
import { FormValues } from './store/types';
import { RootState } from 'typesafe-actions';
import { loginUserAsync } from './store/actions';
import { connect } from 'react-redux';
import { compose } from 'redux';
import { Navigate, useNavigate } from 'react-router-dom';

type Props = ReturnType<typeof mapStateToProps> & typeof dispatchProps;

type PropsWithFormik = Props & FormikProps<FormValues>;

class LoginPage extends Component<PropsWithFormik, any> {

  getYear = () => {
    return new Date().getFullYear();
  };

  render() {
    const { isSubmitting = false, dirty = false } = this.props;

    return (
      <React.Fragment>
        <div className="text-center">
          <img
            className="m-4"
            src="/assets/img/logo-vm.png"
            alt=""
            height="50"
          />
        </div>
        
        <Form className="form-signin">
          <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>

          <label htmlFor="email" className="sr-only">
            Email address
          </label>
          <Field
            className="form-control"
            name="email"
            placeholder="Email address"
            component="input"
            type="text"
            required
            autoFocus
          />
          <ErrorMessage name="email" />

          <label htmlFor="password" className="sr-only">
            Password
          </label>
          <Field
            className="form-control"
            name="password"
            placeholder="Password"
            component="input"
            type="password"
            required
            autoFocus
          />
          <ErrorMessage name="password" />

          <br />

          <button
            className="btn btn-lg btn-primary btn-block mt-2"
            type="submit"
            // disabled={!dirty || isSubmitting}
          >
            Sign in
          </button>
          <div className="mt-3">
            <a>Register</a>
          </div>

          <p className="mt-5 mb-3 text-muted">&copy; 2018-{this.getYear()}</p>
        </Form>
      </React.Fragment>
    );
  }
}

const mapStateToProps = (state: RootState) => ({
  isLoading: state.login.isLoading,
  isLoggedIn: state.login.isLoggedIn
});
const dispatchProps = {
  loginUser: loginUserAsync.request
};

export default compose(
  connect(
    mapStateToProps,
    dispatchProps
  ),
  withFormik<Props, FormValues>({    
    enableReinitialize: true,
    // initialize values
    mapPropsToValues: (data: Props) => ({
      email: 'testuser@testuser.com',
      password: 'testuser'
    }),
    handleSubmit: (values, form) => {
      // if (form.props.article != null) {
      //   form.props.updateArticle({ ...form.props.article, ...values });
      // } else {
      //   form.props.createArticle(values);
      // }      

      form.props.loginUser({
        email: values.email,
        password: values.password
      });

      //form.props.redirectToListing();
      form.setSubmitting(false);
    },
  })
)(LoginPage);
