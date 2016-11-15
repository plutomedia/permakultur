using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{

    public class btn_SimulerAlt : MonoBehaviour
    {
        public WMG_X_Tutor grafen;
        ////public MainController mainController;
        public Initializer initializer;

        //skriv til canvas-variable
        public GameObject tekstfelt;    //textDisplay
        public GameObject inputfelt;

        public int dayIndex;
        public int simulateFromDay;

        void start()
        {
            simulateFromDay = 0;
            //dette betyr at alle simuleringer skal gjøres fra dag 0
            //dette vil endres når input legges inn på visse dager
        }




        public void UpdateAll()
        {
            // Debug.Log("______________Kjører alle iterasjoner på simuleringen");

            // initializer.Oppdater(1);
            //// initializer.Oppdater(2);
            // initializer.Oppdater(3);

            // initializer.WriteEarthValues();

            initializer.UpdateMonth(simulateFromDay);
            Debug.Log("E T T E R    UPDATEMONTH. NOW WE MAKE ANOTHER GRAPH");


            //grafen = new WMG_X_Tutor();

            //grafen.updateGraphs();
                
            Debug.Log("E T T E R     MAKE ANOTHER GRAPH. END OF UPDATEALL BUTTON");
            
        }
    }
}