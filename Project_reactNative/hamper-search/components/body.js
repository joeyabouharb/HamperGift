import React, { Component} from 'react';
import { StyleSheet, Text, ScrollView, View } from 'react-native';
import {  widthPercentageToDP as wp,
    heightPercentageToDP as hp} from 'react-native-responsive-screen';
class Body extends Component {
  
    renderHampers(hampers) {

        
        return hampers.map(hamper => {
            //console.debug(hamper.HamperName)
            return (
                <Text key={hamper.HamperId}> {hamper.HamperName}</Text>     
            );
        })
    }

    render() {
        return (
     
            <ScrollView style={styles.container}>

                {this.renderHampers(this.props.hampers)}
                
            </ScrollView >
         
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1 ,
        height: hp('60%'),
        flexDirection: 'column',
    },
  
});

export default Body;
