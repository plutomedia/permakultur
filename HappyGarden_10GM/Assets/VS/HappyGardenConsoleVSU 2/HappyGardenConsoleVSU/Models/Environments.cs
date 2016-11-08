using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{
    public class Environments
    {
        public Weather vaer;

        public List<Weather> weatherList;
        //Weather class contains data from each day. List index or array of days.
        //first version one year where the simulation runs from day 0, be it whenever.

        public Environments()
        {
            Debug.Log("Environments Model------------------->");

            //Creates an object of Weather type
            vaer = Weather.ThisDay;



            vaer.Name = "drittvær";
            String ut = String.Format("vaer.Name={0} ,vaer.Name={1}", vaer.Name, vaer.Name);
            ///Debug.Log("String ut: "+ut);


            /// weather = new Weather(); //parameter can later contain date, year, climate or other
            //weather data list contains data from different statistics. Options can be chosen by user if implemented
        }


        

    }
}