using UnityEngine;
using System.Collections;

namespace HappyGardenConsoleVSU
{

    public class btn_UpdateE2 : MonoBehaviour
    {
        ////public MainController mainController;
        public Initializer initializer;


        public void UpdateEarth()
        {
            
            //Earth variable oppdateres
            //Debug.Log("klikket update environment variables\n\n\n\n");

            Debug.Log("__________________________________\nUpdate EARTH --->");
            initializer.Oppdater(2);
            initializer.WriteEarthValues();
        }
    }
}