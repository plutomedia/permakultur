using UnityEngine;
using System.Collections;

namespace HappyGardenConsoleVSU
{

    public class btn_UpdateP : MonoBehaviour
    {
        ////public MainController mainController;
        public Initializer initializer;


        public void UpdatePlant()
        {
            
            //Earth variable oppdateres
            //Debug.Log("klikket update environment variables\n\n\n\n");

            //Debug.Log("_____________________________________________________\nUpdate plant --->");
            initializer.Oppdater(3);
            initializer.WriteEarthValues();
        }
    }
}