﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;



namespace HappyGardenConsoleVSU
{
    public class Initializer : MonoBehaviour
    {
        private static bool graphUpdated;
        public Text myText;
        public Text whatText;
        //public MainController mainController;
        public GameFrame gameFrame;
        //HappyGardenConsole console;

        public float lastFrameTime = 0.0f;
        public float thisInterval = 0.0f;
        public float lastFixed = 0.0f;
  
        //private Game game;
        //private Farm frm;
        //private  List<Field> flds;
        public Spot[,] spotts;
        private static Spot spotValgt;

        public Environments environments;
        Weather vaer;

        public static int dagenIdag, dagValgt;  //dagen som det skal simuleres fra. dvs input som fx vanning og kalking og planting
        public WMG_X_Tutor wmgTut;



        void Start()
        {
            graphUpdated = false;
            Debug.Log("HAPPY GARDEN MAIN");

            environments = new Environments();
            gameFrame = new GameFrame(whatText);  //This is not good mvc. here we hand the view to the model, and intend to change the view from the model. No listeners.

            InitializeEarthValues();
            //startverdier for get-set
            dagValgt = 0; //DVS dag 0
            spotValgt = Field.Spots[0, 0];
        }



        void Update()
        {
            //float delta = Time.realtimeSinceStartup - lastFrameTime;
            lastFrameTime = Time.realtimeSinceStartup;
            thisInterval = lastFrameTime-lastFixed;

            if (thisInterval > 8)
            {
                //Hvert 8+. sekund utføres dette
                lastFixed = lastFrameTime;                       
                String tid = String.Format("_________________________________\nVi oppdaterer GameFrame (time: {0})", lastFrameTime);
                //Debug.Log(tid);
            }
        }


        public void Oppdater(int iterasj)
        {
            //Debug.Log("Initializer.Update ---> from gameframe: "+this.gameFrame);
            int iterasjon = iterasj;
            vaer = Weather.ThisDay;

            string tempstring = String.Format("HappyGarden, Day {0}:   ", vaer.WhichDay);

            //InitializeEarthValues(); //Fills the Vector2 arrays with default data

            whatText.text = tempstring;

            gameFrame.Oppdater(iterasjon);


        }


        public void WriteEarthValues()
        {
            gameFrame.WriteEarthValues(myText);
          
        }


        public void PourWater(int fieldn)
        {
            int fieldnr = fieldn;
            gameFrame.PourWater(fieldnr);
        }



        public void Plant(string plantname, int fieldNr, int spotX, int spotY)
        {
            gameFrame.Plant(plantname, fieldNr, spotX, spotY);
        }


        public void InitializeEarthValues()
        {
            gameFrame.InitializeEarthType();

        }

        public void UpdateMonth(int dagenidag)
        {
            
            graphUpdated = true;
            dagenIdag = dagenidag;           
            Weather vair = Weather.ThisDay;


            for (int day = 0; day < 28; day ++)
            {
                Debug.Log(day+"x dag   [||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||>");

                Weather.ThisDay.WhichDay = day;
                dagenIdag = day;//brukes fra Spot for å holde rede på dag-index

                gameFrame.Oppdater(1);
                gameFrame.Oppdater(2);
                gameFrame.Oppdater(3);
                gameFrame.Oppdater(4);

                Debug.Log(day + "x dag slutt     <||||||||||||||||||||||||||||||||||||||||||||||||||||||]");
            }
           

            graphUpdated = true; //denne sjekkes fra viewet vha 'GraphUpdated'
            Debug.Log("END OF UPDATE.  MAKE A NEW GRAF");
        }


        public void printVector(List<Vector2> flekk, float max)
        {
            int c = flekk.Count;
            for (int i = 0; i < c; i++)
            {
                Debug.Log("flekk[i].y: " + flekk[i].y+",   i="+i);
            }
        }


        public static bool GraphUpdated
        {
            get
            {
                return graphUpdated;
            }
            set
            {
                graphUpdated = value;
            }
        }


        public static int DagenIdag
        {
            get
            {
                return dagenIdag;
            }
            set
            {
                dagenIdag = value;
            }
        }




        public static int DagValgt
        {
            get
            {
                return dagValgt;
            }
            set
            {
                dagValgt = value;
            }
        }

        public static Spot SpotValgt
        {
            get
            {
                return spotValgt;
            }
            set
            {
                spotValgt = value;
            }
        }

    }//class Initializer
}//namespace
