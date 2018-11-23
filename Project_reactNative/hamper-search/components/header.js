import React, { Component } from 'react';
import { StyleSheet, View, TextInput, Text, TouchableOpacity, Picker } from 'react-native';

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
            <View style={styles.container}>
               <Picker
                style={{ height: 60, width: 200, marginLeft: 'auto', marginRight: 'auto' }}
               selectedValue={this.props.category}
              
               onValueChange={(itemValue, itemIndex) => this.props.pickerChanged(itemValue, itemIndex)}>
               <Picker.Item label="Filter By Category"/>
                <Picker.Item value={0} label="Get All"/>
                {this.renderPickers(this.props.categories)}
               
               </Picker>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        padding: 15,
        backgroundColor: '#278e67',
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
