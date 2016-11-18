using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HappyGardenConsoleVSU
{
    public class Weather {

        private static List<Vector2> rain = new List<Vector2>();
        private static List<Vector2> sun = new List<Vector2>();


        private int nedboer;
        private int sunHours;
        private int lightHours;
        private string name;
        private int meanTemp;

        private float[] rainMM;
        private int[] sunH;
        private int[] lightH;
        private int[] tempMed;
        private int howManyDays = 28;

        private int whichDay = 0; //In the Vector2: this will be the first index (day from 0 to 28)

        System.Random rnd = new System.Random();


        private Weather()
        {
            rainMM = new float[howManyDays];
            sunH = new int[howManyDays];
            lightH = new int[howManyDays];
            tempMed = new int[howManyDays];


            for (int i=0; i< howManyDays; i+= 1)
            {
                Initialize(i);
            }
        }


        //private static variable of Singleton type.
        private static Weather thisDay = new Weather();

        //declare a public static property that returns the
        //Singleton object as we can not call the private
        //constructor from outside the class.
        public static Weather ThisDay
        {
            get { return Weather.thisDay; }
            set { Weather.thisDay = value; }
        }



        private void Initialize(int dg)
        {
            int dag = dg;
            //Debug.Log("initialiserer Weather. dag "+dag);


            bool rainy = false;

            lightH[dag] = 12; //this dummynumber implies an equinox time. More or less.

            System.Random rnd = new System.Random();

            //rain or not randomized
            if (RandomNr(1, 5) > 2) rainy = false;
            else rainy = true;

            if (rainy)
            {
                rainMM[dag] = RandomNr(0, 50);
                sunH[dag] = RandomNr(0, 3); //if rain between 0 and 3 sun hours
                tempMed[dag] = RandomNr(10, 20);


            }
            else
            {
                rainMM[dag] = 0;
                sunH[dag] = RandomNr(0, 8);         //if not rain between 0 and 8 sun hours
                tempMed[dag] = RandomNr(15, 25);

                //Vector2 initializing for graphical display only



            }

            Debug.Log("rainMM["+dag+"]="+rainMM[dag]);
            rain.Add(new Vector2(dag, rainMM[dag]));
            sun.Add(new Vector2(dag, sunH[dag]*10));//ganger med 10 for å skalere det bedre til grafen





            //Debug.Log("Initierer dag " + dag);
            //Debug.Log("mm H2O "+ rainMM[dag]);
            //Debug.Log("Sun h "+ sunH[dag]);
            //Debug.Log("Light h "+ lightH[dag]);
            //Debug.Log("mean Temp "+ tempMed[dag]);
            //Debug.Log("");

            // String nytekst = String.Format("Water {0} N {1} C {2} K {3} Ca {4} Ph {5} X {6} Y {7} Z {8} Plant: {9}", nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, min_Mn, planteNavn);
            String nytekst = String.Format("Simulation day {0}, with weather:   mm Nedbør {1},  Soltimer {2},  Dagslystimer {3},  Gj.Sn temp {4}", dag, rainMM[dag], sunH[dag], lightH[dag], tempMed[dag]);
            Debug.Log(nytekst);

           
        }

        private int RandomNr(int min, int max)
        {
            int tilfTall = 0;
            //System.Random rnd = new System.Random();
            tilfTall = rnd.Next(min, max + 1);
           //Debug.Log(tilfTall);
            return tilfTall;
        }

        //disse vil bare gi de samme verdiene. random må kalles hver gang

        public int SunHours
        {
            //this function makes age public to the controller
            get { return sunH[whichDay]; }
            set { sunH[WhichDay] = value; }
        }

        public float Nedboer
        {
            //this function makes age public to the controller
            get { return rainMM[WhichDay]; }
            set { rainMM[WhichDay] = value; }
        }

        public int LightHours
        {
            //this function makes age public to the controller
            get { return lightH[WhichDay]; }
            set { lightH[WhichDay] = value; }
        }

        public int MeanTemp
        {
            //this function makes age public to the controller
            get { return tempMed[WhichDay]; }
            set { tempMed[WhichDay] = value; }
        }


        public int WhichDay
        {
            get { return whichDay; }
            set { whichDay = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //These to Vector2 lists are initially made for access for the graph-view
        public static List<Vector2> Sun
        {
            get { return sun; }
            set { sun = value; }
        }

        public static List<Vector2> Rain
        {
            get { return rain; }
            set { rain = value; }
        }
    }
}