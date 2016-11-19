﻿using UnityEngine;
using System.Collections.Generic;
using HappyGardenConsoleVSU;
using UnityEngine.UI;
using System;



namespace HappyGardenConsoleVSU
{
    public class WMG_X_Tutor : MonoBehaviour
    {
        public GameObject ScriptContainer;   //containerValues
        public GameObject emptyGraphPrefab;
        public  WMG_Axis_Graph graph;
        public GameObject daySlider;

        public WMG_Series series1;//1-28 serien kan hentes fra ...public
        public WMG_Series series2;//
        public WMG_Series series3;
        public WMG_Series series4;
        public WMG_Series series5;
        public WMG_Series series6;
        public WMG_Series series7;
        public WMG_Series series8;
        public WMG_Series series9;


        public List<Vector2> series1Data, testData, waterData, tilf1Data, tilf2Data;
        public static List<Vector2> soltimer, regnMM;
      
        

        /// Verdier fra Spot første siffer i vektor er dagnr
        public List<Vector2> air, waterMM, smallLife, humusQuality, nitrogen, organicMatter;

        public static Spot[,] zpots;
        public Spot thisSpot;  //denne har brukt ulike måter å bli tilordnet
        // har laget Initializer.SpotValgt[0,0] som default spot og
        //      Initializer.DagValgt (=0) som default dag


        //public Field field = Farm.MuldTeig;
        //public bool  useData4;
        public List<string> series1Data2; //streng variable. fx dager eller uker

        public Boolean notfirsttime;

        public bool showAll, showOnlyWeather, showOnlyEarth, showNothing;
        public bool spot00, spot01;

        public static Spot chosenSpot;
        public static int chosenDay;


         void Start()
        {


            zpots = Field.Spots;
            chosenSpot = zpots[0, 0];  //default start på spot(0,0)

            //  alternativt: chosenSpot=Initializer.valgSpot;

            

            notfirsttime = false;

            GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
            graphGO.transform.SetParent(this.transform, false);
            graph = graphGO.GetComponent<WMG_Axis_Graph>();

            Debug.Log("INITIALISER FRA START. Jordflekk OG zpots");


/* 
            if (spot00)
            {
                thisSpot = zpots[0, 0];
                Debug.Log("INITIALISER FRA START. Jordflekk (0,0), dvs zpots[0,0]");
            }
            else if (spot01)
            {
                thisSpot = zpots[0, 1];
                Debug.Log("INITIALISER FRA START. Jordflekk (0,1), dvs zpots[0,1]");
            }
*/
            thisSpot = chosenSpot;
            Debug.Log("FRA START WMGTUTOR, VALGT SPOT=chosenSpot:   " + chosenSpot.v_index+","+chosenSpot.h_index);



            Debug.Log("1graph   " + graph);

            initiateSecondGraph();

        }//start()



        
        void Update()
        {
            //Debug.Log(Initializer.GraphUpdated);
            if (Initializer.GraphUpdated)
            {
                Initializer.GraphUpdated = false;
                notfirsttime = true;             
            }
        }
        


        public void initiateSecondGraph()
        {

            Debug.Log("initiateSecondGraph metoden *********       *********       ************      *****"+zpots);

            thisSpot = chosenSpot;

            
            Debug.Log("InitiateSecndGraph. SJEKKER AT WATERMM IKKE ER TOMwatermm antall " + waterMM.Count);
            air = thisSpot.Air;
            smallLife = thisSpot.SmallLife;
            humusQuality = thisSpot.HumusQuality; ;
            nitrogen = thisSpot.Nitrogen;
            organicMatter = thisSpot.OrganicMatter;
            //testData = containerValues.MinVektorListe;// dette er kanskje måten eksterne vektorer kan bli tilgjengelige
            waterData = Weather.Rain;//mm nedbør


            //tilf1Data = containerValues.MinVektorListe3;
            soltimer = Weather.Sun;//vektorliste til graf
            waterMM = thisSpot.WaterMM; 

 
  
            series1 = graph.addSeries();    //legger inn series1. den hentes vha graphGO.Getcomponent se linje 20
            series2 = graph.addSeries();    // soltimer
            series3 = graph.addSeries();    // waterData
            series4 = graph.addSeries();
            series5 = graph.addSeries();
            series6 = graph.addSeries();
            series7 = graph.addSeries();
            series8 = graph.addSeries();
            series9 = graph.addSeries();

            graph.xAxis.AxisMaxValue = 28;

            series1.seriesName = "Water";
            series2.seriesName = "Soltimer";
            series3.seriesName = "Rain mm";
            series4.seriesName = "Oxygen";
            series5.seriesName = "tilf2";
            series6.seriesName = "smallLife";
            series7.seriesName = "humusQuality";
            series8.seriesName = "Nitrogen";
            series9.seriesName = "organicMatter";


            series1.lineColor = Color.grey;
            series2.lineColor = Color.yellow;
            series3.lineColor = Color.blue;
            series4.lineColor = Color.cyan;
            series5.lineColor = Color.white;
            series6.lineColor = Color.black;
            series7.lineColor = Color.grey;
            series8.lineColor = Color.red;
            series9.lineColor = Color.magenta;

            graph.Refresh();
        }


        public void printVector(List<Vector2> flekk, float max)
        {
            int range = flekk.Count;
            Debug.Log("lengde: " + range);
            for (int i = 0; i < range; i++)
            {
                Debug.Log("printVector  "+flekk[i].y);
            }
        }


        public List<Vector2> normaliserListe(List<Vector2> data, float max)
        {
            //hensikt: vektorene skal på samme graf og med ulike mål fx timer, mm, g;
            //siden vi har et gitt antall Vektorer, setter vi disse manuelt
            //Ved utvidelse til mer dynamiske lister senere, kan dette forandres til spørringer
            List<Vector2> Nliste;
            Nliste = data;

            //dummy som bare sender den samme listen tilbake

            return Nliste;
        }

        public void Oppdater()
        {
            if (true)
            {
                Debug.Log("valg gjort. eventuelt oppdatering");

                List<string> groups = new List<string>();
                List<Vector2> empty = new List<Vector2>();

                chosenSpot = Initializer.SpotValgt;
                thisSpot = chosenSpot;

                //PROBLEM MED WATERMM. SER UT TIL AT VERDIENE IKKE KAN HENTES HER I OPPDATER().

                waterMM = thisSpot.WaterMM;
                soltimer = Weather.Sun;
                waterData = Weather.Rain;
                air = thisSpot.Air;
                smallLife = thisSpot.SmallLife;
                humusQuality = thisSpot.HumusQuality; ;
                nitrogen = thisSpot.Nitrogen;
                organicMatter = thisSpot.OrganicMatter;


                Debug.Log("watermm antall " + waterMM.Count);
                Debug.Log("air antall " + air.Count);
                Debug.Log("smallLife antall " + smallLife.Count);
                Debug.Log("humusQuality antall " + humusQuality.Count);
                Debug.Log("nitrogen antall " + nitrogen.Count);
                Debug.Log("organicMatter antall " + organicMatter.Count);

             
                if (showOnlyWeather)
                {
                    series1.pointValues.SetList(waterMM); //series1.pointValues.SetList(data);
                    series2.pointValues.SetList(soltimer);
                    series3.pointValues.SetList(waterData);

                    series4.pointValues.SetList(empty);
                    series6.pointValues.SetList(empty);
                    series7.pointValues.SetList(empty);
                    series8.pointValues.SetList(empty);
                    series9.pointValues.SetList(empty);
                }
                else if (showOnlyEarth)
                {
                   series1.pointValues.SetList(waterMM); //series1.pointValues.SetList(data);
                   series2.pointValues.SetList(empty);
                    series3.pointValues.SetList(empty);
                    series4.pointValues.SetList(air); //her settes tilfeldige verdier serie en
                                                      //series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
                    series6.pointValues.SetList(smallLife);
                    series7.pointValues.SetList(humusQuality);
                    series8.pointValues.SetList(nitrogen);
                    series9.pointValues.SetList(organicMatter);  /*  */
                }
                else if (showNothing)
                {
                    series1.pointValues.SetList(empty); //series1.pointValues.SetList(data);
                    series2.pointValues.SetList(empty);
                    series3.pointValues.SetList(empty);

                    series4.pointValues.SetList(empty);
                    series6.pointValues.SetList(empty);
                    series7.pointValues.SetList(empty);
                    series8.pointValues.SetList(empty);
                    series9.pointValues.SetList(empty);  /*  */

                }
                else
                {
                    series1.pointValues.SetList(waterMM); //series1.pointValues.SetList(data);
                    series2.pointValues.SetList(soltimer);
                    series3.pointValues.SetList(waterData);
                    series4.pointValues.SetList(air); //her settes tilfeldige verdier serie en
                 //series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
                    series6.pointValues.SetList(smallLife);
                   series7.pointValues.SetList(humusQuality);
                    series8.pointValues.SetList(nitrogen);
                    series9.pointValues.SetList(organicMatter);  /*  */
                }



                graph.Refresh();
               
            }
        }


        public void nyeVerdier()
        {
            Debug.Log("Her forandrer jeg fra et spot til et annet");

            if (spot00)
            {
                thisSpot = zpots[0, 0];
                Debug.Log("Jordflekk (0,0), dvs zpots[0,0]");
            }
            else if (spot01)
            {
                thisSpot = zpots[0, 1];
                Debug.Log("Jordflekk (0,1), dvs zpots[0,1]");
            }

        }


        public void gamleVerdierf()
        {
            //lager en temp-array
            List<Vector2> data3 = new List<Vector2>();

            data3.Add(new Vector2(3, 20));
            data3.Add(new Vector2(9, 86));
            data3.Add(new Vector2(13, 95));
            data3.Add(new Vector2(20, 80));
            data3.Add(new Vector2(21, 60));
            data3.Add(new Vector2(22, 0));
            Debug.Log("gammel graf skrives opp igjen");

            //series1.pointValues.SetList(series1Data);

            //series2.pointValues.SetList(waterMM);

            /*   if (waterMM.Count > 1)
               {
                   series2.pointValues.SetList(waterMM);
                   for (int i=0; i < 7; i++)
                   {
                       Debug.Log(waterMM[i].y);
                   }
               }
               else
               {
                   soltimer = Weather.Sun;
                   series2.pointValues.SetList(soltimer);
                   for (int i = 0; i < 7; i++)
                   {
                       Debug.Log("soltimer (x,y): "+soltimer[i].x+" ,  "+ soltimer[i].y);
                   }
               }*/

            //series2.pointValues.SetList(soltimer);
            //waterMM = Weather.Rain;
            //series3.pointValues.SetList(waterMM);
            series1.pointValues.SetList(data3);

        }



        public static Spot ChosenSpot
        {
            get
            {
                return chosenSpot;
            }
            set
            {
                chosenSpot = value;
            }
        }



        public static int ChosenDay
        {
            get
            {
                return chosenDay;
            }
            set
            {
                chosenDay = value;
            }
        }

    }//class
}//namespace