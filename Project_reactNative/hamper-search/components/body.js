import React, { Component } from 'react';
import { StyleSheet, Text, View } from 'react-native';

class Body extends Component {
    renderHampers(hamper) {
        return hamper.map(movie => {
            return (
                <Text key={hamper.HamperId}> {hamper.HamperName} {hamper.Cost}</Text>
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
