import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Header from './components/header';
import Body from './components/body';

import {HamperData,  CategoryData} from './services/dataservices';
class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
        cats: [],
        hampers: []
    };
}

GetAll(){
  cats: CategoryData();
  hampers: HamperData();
}
componentDidMount(){
  this.GetAll();
}
  render() {
    return (
      <div className="App">
        <Header/>

                 <Body results={this.state.hampers} />
      </div>
    );
  }
}

export default App;
