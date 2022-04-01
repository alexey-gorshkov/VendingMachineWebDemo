import React, { Component } from 'react';
import { Form, FormikProps, Field, withFormik, ErrorMessage } from 'formik';

class RegisterPage extends Component<any, any> {

    getYear = () => {
        return new Date().getFullYear();
    };

    render() {
        return (
            <Form className="form-signin">
                <h1 className="h3 mb-3 font-weight-normal">New User Registration</h1>

                <label htmlFor="email" className="sr-only">Email address</label>
                <Field
                    name="email"
                    placeholder="Email address"
                    component="input"
                    type="text"
                    required
                    autoFocus
                />
                <ErrorMessage name="email" />

                <label htmlFor="password" className="sr-only">Password</label>
                <Field
                    name="password"
                    placeholder="Password"
                    component="input"
                    type="password"
                    required
                    autoFocus
                />
                <ErrorMessage name="password" />

                {/* <span *ngIf="(emailField.dirty || emailField.touched) && emailField.invalid && emailField.hasError('required')">
                    Email is required.
                </span>
                <span *ngIf="(emailField.dirty || emailField.touched) && emailField.invalid && emailField.hasError('email')">
                    Value has to be a valid email address.
                </span>
                <br />
                <span *ngIf="(passwordField.dirty || passwordField.touched) && passwordField.invalid && passwordField.hasError('required')">
                    Password is required.
                </span>
                <span *ngIf="(passwordField.dirty || passwordField.touched) && passwordField.invalid && passwordField.hasError('minlength')">
                    Password must be at least 4 characters long.
                </span> */}

                {/*<div className="mt-3">
                    <a (click)="register()">Register</a>
                </div> */}

                <button className="btn btn-lg btn-primary btn-block mt-2" type="submit">Sign in</button>
                <p className="mt-5 mb-3 text-muted">&copy; 2018-{ this.getYear() }</p>
            </Form>
        )
    }
}

export default RegisterPage;