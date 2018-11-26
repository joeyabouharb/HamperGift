import React, { Component } from 'react';
import { StyleSheet, View, TextInput, Text, Image, TouchableOpacity, Picker, Button } from 'react-native';
import {  widthPercentageToDP as wp,
    heightPercentageToDP as hp} from 'react-native-responsive-screen';
class Header extends Component {
        renderPickers(categories){
           
            return categories.map( category =>
                {
                    return (
                        <Picker.Item key={category.CategoryId} label={category.CategoryName} value={category.CategoryId}/>
                    );
                }
            )
        }
    render() {
        return (
            <View style={styles.flexibleContainer}>
               <View style={styles.containerTitle}>
                   <Image source={require('./logo.png')} resizeMode='contain' style={{maxHeight: hp('14%')}}/>
                   <Text style={{fontSize: wp('6%')}}>Hamper Gift</Text>
               </View>
            <View style={styles.container}>
               <Picker
            style={styles.picker}
               selectedValue={this.props.category}
               onValueChange={(itemValue, itemIndex) => this.props.pickerChanged(itemValue, itemIndex )}>
               <Picker.Item label="Filter By Category"/>
                <Picker.Item value={0} label="Get All"/>
                {this.renderPickers(this.props.categories)}
               
               </Picker>
               <TextInput style={styles.searchInput} value={this.props.query}  placeholder='search for hampers' onChangeText={(text) => this.props.onSearchChange(text)}></TextInput>
            </View>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 0.6,
        height: hp('12.5%'),
        flexDirection: 'row',
        backgroundColor: '#278e67',
        alignItems: 'center'
      
    },
    flexibleContainer: {
        flex: 0.6,
        display: 'flex',
        flexDirection: 'column',
    }

    ,
    picker: {
        height: hp('15%'),
        width: wp('45%')
    },
    containerTitle: {
        height: hp('10%'),
       flex: 0.7,
       justifyContent: 'space-between',
        flexDirection: 'row',
        alignItems: 'center'
  
        
    },
    searchInput:{
        height: hp('5%'),
        width: wp('60%')
    },

});

export default Header;
