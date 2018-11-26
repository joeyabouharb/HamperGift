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
  if(id == 0){
      return hamps;
  }
    hamps.map(hamp => {
        if(hamp.CategoryId == id){
        data.push(hamp);
        }
    });
    return data;
    
}

export function filterByQuery(q){
    let hamps = [];
    let data = [];
    console.debug(q)
    HamperData(response => 
        {
         hamps = response.Hamper
        });

        hamps.map(hamp => {
            if(hamp.HamperName.toLowerCase().includes(q.toLowerCase())){
                data.push(hamp)
            }
        })

        return data;

}