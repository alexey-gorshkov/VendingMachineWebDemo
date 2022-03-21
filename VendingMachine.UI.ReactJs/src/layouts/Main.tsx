import React, { FC } from 'react';
import { Link } from 'react-router-dom';

import './Main.css';
//import logo from '../assets/img/logo.svg';
import FlexRow from '../common/components/FlexRow';

type Props = {
  renderActionsMenu?: () => JSX.Element;
};

const Main: FC<Props> = ({ children, renderActionsMenu }) => (
  <div className="App">
    <header className="App-header">
      <FlexRow
        grow={1}
        align="center"
        justify="space-between"
        style={{ padding: '0 60px' }}
      >
        <FlexRow align="center">
          {/* <img src={logo} className="App-logo" alt="logo" /> */}
          <Link className="App-link" to="/">
            Demo App
          </Link>
        </FlexRow>
        {renderActionsMenu && renderActionsMenu()}
      </FlexRow>
    </header>
    <main className="App-main">{children}</main>
  </div>
);

export default Main;
