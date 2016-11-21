using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{

    public class btn_SimulerAlt : MonoBehaviour
    {
        public Initializer initializer;

        //skriv til canvas-variable
        public GameObject tekstfelt;    //textDisplay
        public GameObject inputfelt;

        public int dayIndex;
        public int simulateFromDay;
        public int dayChosen;



        void start()
        {
            simulateFromDay = 0;
            
            //dette betyr at alle simuleringer skal gjøres fra dag 0
            //dette vil endres når input legges inn på visse dager
        }




        public void UpdateAll()
        {
            dayChosen = Initializer.DagValgt;

            Debug.Log("F Ø R UpdateMonth. dayChosen er "+dayChosen +" men her skal bare ALLE spot initialiseres og regnesut");

           initializer.UpdateMonth(0);
            
            Debug.Log("E T T E R     MAKE ANOTHER GRAPH. END OF UPDATEALL BUTTON");
            
        }
    }
}