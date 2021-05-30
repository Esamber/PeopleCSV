import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import HomePage from './pages/Home';
import Upload from './pages/Upload';
import Generate from './pages/Generate';


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={HomePage} />
            <Route exact path='/upload' component={Upload} />
            <Route exact path='/generate' component={Generate} />
      </Layout>
    );
  }
}
