using UnityEngine;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class btn_Water3 : MonoBehaviour
    {
        public Initializer initializer;
        int fieldNr;

        public void PourWater()
        {
            fieldNr = 3;
            initializer.PourWater(fieldNr);
            //initializer.Oppdater(fieldNr);
            initializer.WriteEarthValues();
        }
    }
}