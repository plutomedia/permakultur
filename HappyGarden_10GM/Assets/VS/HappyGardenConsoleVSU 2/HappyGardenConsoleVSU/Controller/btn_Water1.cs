﻿using UnityEngine;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class btn_Water1 : MonoBehaviour
    {
        public Initializer initializer;
        int fieldNr;

        public void PourWater()
        {
            fieldNr = 1;
            initializer.PourWater(fieldNr);
            //initializer.Oppdater(fieldNr);
            initializer.WriteEarthValues();
        }
    }
}