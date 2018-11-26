import React, { Component } from 'react';
import { StyleSheet, View, TextInput, Text, Image, TouchableOpacity, Picker, Button } from 'react-native';

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
            <View>
               <View style={styles.containerTitle}>
                   <Image source={require('./logo.png')} resizeMode='contain'/>
                   <Text style={{fontSize: 25, alignSelf: 'center'}}>Hamper Gift</Text>
               </View>
            <View style={styles.container}>
               <Picker
                style={{ height: 60, width: 180}}
               selectedValue={this.props.category}
               onValueChange={(itemValue, itemIndex) => this.props.pickerChanged(itemValue, itemIndex )}>
               <Picker.Item label="Filter By Category"/>
                <Picker.Item value={0} label="Get All"/>
                {this.renderPickers(this.props.categories)}
               
               </Picker>
               <TextInput value={this.props.query}  placeholder='search for hampers' onChangeText={(text) => this.props.onSearchChange(text)}></TextInput>
            </View>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        padding: 15,
        backgroundColor: '#278e67',
        borderColor: 'lightblue',
        borderWidth: 1,
        borderBottomWidth: 0,
        margin: 5,
        elevation: 1,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.8,
        shadowRadius: 2

    },
    containerTitle: {
        flexDirection: 'row',
       justifyContent: 'space-between',
       marginRight: 10,
       
    },
    input: {
        flex: 1,
        backgroundColor: '#fff',
        borderWidth: 1,
        borderColor: '#eee',
        borderRadius: 5,
        paddingHorizontal: 8,
        paddingVertical: 5,
        fontSize: 18
    },
    button: {
        backgroundColor: '#fff',
        borderRadius: 5,
        padding: 5,
        marginLeft: 10
    },
    button_text: {
        fontSize: 18,
        paddingVertical: 5,
        paddingHorizontal: 8,
    }
});

export default Header;
