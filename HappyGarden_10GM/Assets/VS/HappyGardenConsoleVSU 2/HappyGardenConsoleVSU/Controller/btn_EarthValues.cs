using UnityEngine;
using System.Collections;

namespace HappyGardenConsoleVSU
{

    public class btn_EarthValues : MonoBehaviour
    {
        ////public MainController mainController;
        public Initializer initializer;


        public void WriteEarthValues()
        {
            
            //Earth variable oppdateres
            //Debug.Log("klikket update environment variables\n\n\n\n");

           Debug.Log("__________________________________\nWrite Earth Values --->");
            initializer.WriteEarthValues();
        }
    }
}