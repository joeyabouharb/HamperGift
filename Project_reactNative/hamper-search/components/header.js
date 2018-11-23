import React, { Component } from 'react';
import { StyleSheet, View, TextInput, Text, TouchableOpacity } from 'react-native';

class Header extends Component {
    render() {
        return (
            <View style={styles.container}>
               
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
