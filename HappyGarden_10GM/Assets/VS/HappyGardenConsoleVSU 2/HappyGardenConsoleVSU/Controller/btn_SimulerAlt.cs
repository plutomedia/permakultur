using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{

    public class btn_SimulerAlt : MonoBehaviour
    {
        ////public MainController mainController;
        public Initializer initializer;

        //skriv til canvas-variable
        public GameObject tekstfelt;    //textDisplay
        public GameObject inputfelt;




        public void UpdateAll()
        {
            Debug.Log("______________Kjører alle iterasjoner på simuleringen");

            initializer.Oppdater(1);
           // initializer.Oppdater(2);
            initializer.Oppdater(3);

            initializer.WriteEarthValues();
        }



    }
}