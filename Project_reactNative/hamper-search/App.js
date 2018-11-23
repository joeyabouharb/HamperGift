import React, { Component } from 'react';
import Header from './components/header';
import Body from './components/body';
import { StyleSheet, View} from 'react-native'
import {HamperData,  CategoryData, filterByCat} from './services/dataservices';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
        cats: [],
        hampers: [],
        cat: ''
    };
}

pickerChanged(id, index){

  this.setState({hampers:  filterByCat(id)})
}


GetAllHampers(){

 HamperData(response => {
   this.setState({hampers: response.Hamper});

 

 })
}

 GetAllCats(){
  CategoryData(response => {
    this.setState({cats: response.Categories});
  })
 }

componentDidMount(){
  this.GetAllHampers();
  this.GetAllCats();
}
  render() {
    return (
      <View style={styles.container}>
        <Header pickerChanged={this.pickerChanged.bind(this)} category={this.state.cat} categories={this.state.cats}/>

        <Body hampers={this.state.hampers} />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF4D3',
    paddingTop: 24
  },
});

export default App;
