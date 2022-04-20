import React, { FC } from 'react';
import { Link } from 'react-router-dom';

//import logo from '../assets/img/logo.svg';
// import FlexRow from '../common/components/FlexRow';

type Props = {
  renderActionsMenu?: () => JSX.Element;
};

const Main: FC<Props> = ({ children, renderActionsMenu }) => (
  <div className="App">
    <header className="App-header">
      <h1 className="App-title">Welcome to Vending Machine</h1>      
      {renderActionsMenu && renderActionsMenu()}
    </header>
    <main className="App-main">{children}</main>
  </div>
);

export default Main;
