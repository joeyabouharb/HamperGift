const hampers = require('./hampers.json');
const categories = require('./cats.json');

export const HamperData = function(){
return  JSON.parse(hampers);
};

export const CategoryData = function() {
    return JSON.parse(categories);
};

