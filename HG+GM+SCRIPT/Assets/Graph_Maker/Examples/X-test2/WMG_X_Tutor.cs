using UnityEngine;
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


        //public Field field = Farm.MuldTeig;
        //public bool  useData4;
        public List<string> series1Data2; //streng variable. fx dager eller uker

        public Boolean notfirsttime;

        public bool showAll, showOnlyWeather, showOnlyEarth, showNothing;


         void Start()
        {
            notfirsttime = false;

            GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
            graphGO.transform.SetParent(this.transform, false);
            graph = graphGO.GetComponent<WMG_Axis_Graph>();

            zpots = Field.Spots;

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
            //graphGO.transform.SetParent(this.transform, false);

            //graph = graphGO.GetComponent<WMG_Axis_Graph>();
            //graph.xAxis.AxisMaxValue = 28;

            //Debug.Log("zpots:::::::::::::::::"+zpots);
            //Debug.Log("zpots[0,0]:::::::::::::::::" + zpots[0,0]);
            //Debug.Log("zpots[0, 0].Air:::::::::::::::::" + zpots[0, 0].Air);
            //Debug.Log("zpots[0, 0].Air[0]:::::::::::::::::" + zpots[0, 0].Air[0]);
            //Debug.Log("zpots[0, 0].Air[0].y:::::::::::::::::" + zpots[0, 0].Air[0].y);

            waterMM         = zpots[0, 0].WaterMM;
            air             = zpots[0, 0].Air;
            smallLife       = zpots[0, 0].SmallLife;
            humusQuality    = zpots[0, 0].HumusQuality; ;
            nitrogen        = zpots[0, 0].Nitrogen;
            organicMatter   = zpots[0, 0].OrganicMatter;
            testData        = containerValues.MinVektorListe;// dette er kanskje måten eksterne vektorer kan bli tilgjengelige
            waterData       = Weather.Rain;//vannmengde i jordbiten


            tilf1Data = containerValues.MinVektorListe3;
            soltimer = Weather.Sun;//vektorliste til graf


 
  
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

            //series1.pointValues.SetList(waterMM);
            //series2.pointValues.SetList(soltimer);
            //series3.pointValues.SetList(waterData);
            //series4.pointValues.SetList(air); //her settes tilfeldige verdier serie en
         /*   series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
            series6.pointValues.SetList(smallLife);
            series7.pointValues.SetList(humusQuality);
            series8.pointValues.SetList(nitrogen);
            series9.pointValues.SetList(organicMatter);*/


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
            /*
            Debug.Log("\n******************************************SKRIVER UT VEKTORER WATER SOL WATERD AIR HUMUSQ NITRO");
            Debug.Log("waterMM"); printVector(waterMM,100);
            Debug.Log("soltimer"); printVector(soltimer, 100);
            Debug.Log("waterData"); printVector(waterData, 100);
            Debug.Log("air"); printVector(air, 100);
            Debug.Log("humusQuality"); printVector(humusQuality, 100);
            Debug.Log("nitrogen"); printVector(nitrogen, 100);
            Debug.Log("\n");*/
            /**/
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
            Debug.Log("nye verdier importeres eller oppdateres og så skriver jeg grafen");
            
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


    }//class
}//namespace