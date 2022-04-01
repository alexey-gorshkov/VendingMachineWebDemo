import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as TestUtils from 'react-dom/test-utils';
import App from '../src/pages/App';

it('App is rendered', () => {
    // Render App in the document
    const appElement: any = TestUtils.renderIntoDocument<any>(<App/>);

    const appNode = ReactDOM.findDOMNode(appElement);

    // Verify text content
    expect(appNode.textContent).toEqual('Hello World!Foo to the barz');
});
