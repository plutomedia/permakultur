using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HappyGardenConsoleVSU
{
    

    class Mineral
    {
        public string name_eng;
        public string name_lat;

        public double average_amount;   //realistic dummy variable

        public Mineral()
        {
            initializeMe();

        }

        private void initializeMe()
        {
            name_eng = "<eng plant-name>";
            name_lat = "<latin plant-name >";
            average_amount = 0.025; //this is a dummy
                                    //when I decide  how to initialize the plant data and how to use them
                                    //this class will be further developed
        }

    }

        //every mineral have chemical properties
        //the plants contain minerals
        //the plants even 'produces minerals' like Nitrogen to the surrounding soil
        //the soil contain minerals, and the minerals are distributed un-evenly through
        //the earth layers. These layers are un-evenly accissible for different plants and
        //under different conditions (porosity, water, microorganism etc.)

        //this is not a complete chemical simulation, and this class will function like a
        //container general data like names, amount...
        //I am not sure yet.

        //one important treat is to be able to store the data to file.
        //this is done by JSON.
        //some data, like spesific, static data can be loaded wherever it is a saved game or not
        //other data, like the amount of minerals at a certain spot is saved by exit of the game
        //and is loaded at a re-start
    }
