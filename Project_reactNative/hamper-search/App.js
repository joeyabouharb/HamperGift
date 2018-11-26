import * as React from 'react';
import Header from './components/header';
import Body from './components/body';
import { StyleSheet, View, Dimensions} from 'react-native'
import * as dataservices from './services/dataservices';
import {  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
  listenOrientationChange as loc,
  removeOrientationListener as rol} from 'react-native-responsive-screen'
class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
        cats: [],
        hampers: [],
        cat: 0,
        query: ''
    };
}


pickerChanged(id, index){

  this.setState({hampers:  dataservices.filterByCat(id),
                    cat: id})
}


GetAllHampers(){

 dataservices.HamperData(response => {
   this.setState({hampers: response.Hamper});
 })
}

 GetAllCats(){
  dataservices.CategoryData(response => {
    this.setState({cats: response.Categories});
  })
 }

 componentWillUnmount() {
 rol();

}

componentDidMount(){
  loc(this);
  this.GetAllHampers();
  this.GetAllCats();
 
}


onSearchChange(text){
  this.setState({hampers: dataservices.filterByQuery(text),
                query: text})
  console.debug(this.state.hampers)

}

  render() {
   
    return (
      <View style={styles.container}>
      <View style={styles.responsiveBox}>
      <Header pickerChanged={this.pickerChanged.bind(this)} category={this.state.cat} categories={this.state.cats} query={this.state.query} onSearchChange={this.onSearchChange.bind(this)}/>

      <Body hampers={this.state.hampers} />
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF4D3',
    paddingTop: 30,
    alignItems: 'center'
  },
  responsiveBox: {
    width: wp('84.5%'),
    height: hp('17%'),
  
  }
});

export default App;
