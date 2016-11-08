using UnityEngine;
using System.Collections;

namespace HappyGardenConsoleVSU
{


    public class btn_UpdateE1 : MonoBehaviour
    {

        ////public MainController mainController;
        public Initializer initializer;
        public int day;

        void start()
        {
            day = 0;
        }



        public void UpdateEnvironment()
        {

            
            Weather weatherObject = Weather.ThisDay;
            weatherObject.WhichDay = day;
            Debug.Log("______________________________________________________________________________________\nUpdate environment --->  Day: "+day+" to "+(day+1));

            //Tilordner en global dato til værklassen
            Weather vaer = Weather.ThisDay;
day += 1;
            vaer.WhichDay = day;

            Debug.Log(vaer.WhichDay);

            initializer.Oppdater(1);
            initializer.Oppdater(2);
            initializer.Oppdater(3);

            initializer.WriteEarthValues();

        }
    }
}