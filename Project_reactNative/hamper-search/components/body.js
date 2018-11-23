import React, { Component } from 'react';
import { StyleSheet, Text, View } from 'react-native';

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
            <View style={styles.container}>
                {this.renderHampers(this.props.hampers)}
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 15
    },
    text: {
        fontSize: 18
    }
});

export default Body;
