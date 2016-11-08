using UnityEngine;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class btn_Water2 : MonoBehaviour
    {
        public Initializer initializer;
        int fieldNr;

        public void PourWater()
        {
            fieldNr = 2;
            initializer.PourWater(fieldNr);
            //initializer.Oppdater(fieldNr);
            initializer.WriteEarthValues();
        }
    }
}