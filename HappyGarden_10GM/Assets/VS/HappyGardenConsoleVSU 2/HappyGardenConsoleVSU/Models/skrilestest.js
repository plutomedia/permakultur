
//#pragma strict

var herbarium = {
    "Redikk": {
        "name": "Redikk",
        "display": "reddik.png",
        "water": 0.05,
        "sun": 1.2,
        "nutrients": {
            "p": 0.002,
            "mg": 0.004,
            "NaCl": 0.01
        }
    },
    "Mais": {
        "name": "Mais",
        "display": "Mais.png",
        "water": 0.15,
        "sun": 3,
        "nutrients": {
            "p": 0.03,
            "mg": 0.054,
            "NaCl": 0.001
        }
    }
};

/*
class Plant{
	var growth;
	var plantTime;
	var sun;
}
*/

function createPlant(nameOfPlant) {

    var plantData = herbarium[nameOfPlant];
    var plant = {};
    // Plant myNewPlant = new Plant();
    plant.name = plantData.name;
    plant.display = plantData.display;
    plant.nutrients = plantData.nutrients;
    plant.sun = plantData.sun;
    plant.water = plantData.water;
    plant.growth = 0;
    plant.plantTime = Date.now();
    //..... Her legger du til de andre attributtene.

    return plant;
}

function write(){
    var plant = createPlant("Redikk");
    var plant2 = createPlant("Mais");

    Debug.Log(plant); Debug.Log(plant2);
}

//console.log(plant); 

//console.log(plant2); 
