const hampers = require('./hampers.json');
const categories = require('./cats.json');

export const HamperData = function(callback){
 callback(hampers);
};

export const CategoryData = function(callback){
    callback(categories);
};

export function filterByCat(id){
    let data = [];
    let hamps = []
    HamperData(response => 
    {
     hamps = response.Hamper
    });
  
    hamps.map(hamp => {
        if(hamp.CategoryId == id){
        data.push(hamp);
        }
    });
    return data;
    
}