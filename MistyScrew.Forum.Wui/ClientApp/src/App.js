import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Main } from './components/Main';
import { BoardView } from './components/BoardView';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Main} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />

        <Route path='/:boardName' render={(props) => <BoardView boardName={props.match.params.boardName} />} />

      </Layout>
    );
  }
}
