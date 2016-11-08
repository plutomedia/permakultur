using System.Collections.Generic;
using System;
using UnityEngine;

using Newtonsoft.Json;
using System.IO;

namespace HappyGardenConsoleVSU
{
    [Serializable]
    class Plante
    {
        /// <summary>
        /// DENNE KLASSEN ER LAGET FOR Å PRØVE UT EN NY STRUKTUR
        /// MER EGNET FOR SERIALIZING. SÅ LANGT ER DET IKKE BRA
        /// </summary>
        /// 
        string name;
        string display;
        double water;
        double sun;

        Dictionary<string, double> nutrientsData;

        public Plante(string navn)
        {
            name = navn;
            initializeMe();
        }

        void initializeMe()
        {
            display = "nofile";
            water = 0.2;
            sun = 0.2;

            nutrientsData = new Dictionary<string, double>(){
                    {"P",0.002},
                    {"Mg",0.002},
                    {"Na",0.002}};
        }
    }

}


