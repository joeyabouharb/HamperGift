import * as React from 'react';
import Header from './components/header';
import Body from './components/body';
import { StyleSheet, View, Dimensions,} from 'react-native'
import * as dataservices from './services/dataservices';
import {  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
  listenOrientationChange as loc,
  removeOrientationListener as rol} from 'react-native-responsive-screen';


class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
        cats: [],
        hampers: [],
        cat: 0,
        query: '',
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
 rol()
}


componentDidMount(){
loc(this)
  this.GetAllHampers();
  this.GetAllCats();

}


onSearchChange(text){

     this.setState({hampers: dataservices.filterByQuery(text),
        query: text});
  

}



  render() {
    return (
      <View style={styles.container}>
   
      <Header style={styles.responsiveHeader} pickerChanged={this.pickerChanged.bind(this)} category={this.state.cat} categories={this.state.cats} query={this.state.query} onSearchChange={this.onSearchChange.bind(this)}/>

      <Body style={styles.responsiveBody} hampers={this.state.hampers} />
        </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FBF4D3',
        paddingTop: 25,
        flexDirection: 'column' ,
  },
  responsiveHeader: {
    width: wp('95%'),
   height: hp('16%'),
    flexDirection: 'row',
    justifyContent: 'center',
    position: 'absolute',
    zIndex: 1
  },
  responsiveBody: {
flex: 1,
alignItems: 'center',
justifyContent: 'space-between',
  

  }
});

export default App;
