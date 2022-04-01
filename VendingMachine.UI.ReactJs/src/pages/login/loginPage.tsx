import React, { Component } from 'react';
import {
  Formik,
  Form,
  FormikProps,
  Field,
  withFormik,
  ErrorMessage,
  FormikHelpers,
} from 'formik';

interface Values {
  email: string;
  password: string;
}

class LoginPage extends Component<any, any> {
  getYear = () => {
    return new Date().getFullYear();
  };

  render() {
    const { isSubmitting = false, isDirty = false } = this.props;

    return (
      <React.Fragment>
        <div className="text-center">
          <img className="m-4" src="/assets/img/logo-vm.png" alt="" height="50" />
        </div>
        <Formik
          initialValues={{
            email: 'testuser@testuser.com',
            password: 'testuser',
          }}
          validate={(values) => {
            const errors: any = {};
            if (!values.email) {
              errors.email = 'Required';
            } else if (
              !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)
            ) {
              errors.email = 'Invalid email address';
            }
            return errors;
          }}
          onSubmit={(values: Values,
            { setSubmitting }: FormikHelpers<Values>
          ) => {
            setTimeout(() => {
              alert(JSON.stringify(values, null, 2));
              setSubmitting(false);
            }, 500);
          }}
        >
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
              disabled={!isDirty || isSubmitting}
            >
              Sign in
            </button>
            <div className="mt-3">
                <a>Register</a>
            </div>

            <p className="mt-5 mb-3 text-muted">&copy; 2018-{this.getYear()}</p>
          </Form>
        </Formik>
      </React.Fragment>
      
    );
  }
}

export default LoginPage;
