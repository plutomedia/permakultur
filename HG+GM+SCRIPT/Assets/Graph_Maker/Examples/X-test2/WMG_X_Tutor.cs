using UnityEngine;
using System.Collections.Generic;
using HappyGardenConsoleVSU;
using UnityEngine.UI;
using System;



namespace HappyGardenConsoleVSU
{
    public class WMG_X_Tutor : MonoBehaviour
    {

        public GameObject emptyGraphPrefab;
        public  WMG_Axis_Graph graph;

        public WMG_Series series1;//1-28 serien kan hentes fra ...public
        public WMG_Series series2;//1-28 serien kan hentes fra ...public
        public WMG_Series series3;//1-28 serien kan hentes fra ...public
        public WMG_Series series4;//1-28 serien kan hentes fra ...public
        public WMG_Series series5;//1-28 serien kan hentes fra ...public
        public WMG_Series series6;//1-28 serien kan hentes fra ...public
        public WMG_Series series7;//1-28 serien kan hentes fra ...public
        public WMG_Series series8;//1-28 serien kan hentes fra ...public
        public WMG_Series series9;//1-28 serien kan hentes fra ...public


        public List<Vector2> series1Data, testData, waterData, tilf1Data, tilf2Data;
        //public List<Vector2> series1DataN, testDataN, waterDataN, tilf1DataN, tilf2DataN;
        public static List<Vector2> soltimer, regnMM, soltimerN, regnMMN;
        public GameObject ScriptContainer;   //containerValues

        /// Verdier fra Spot første siffer i vektor er dagnr
        public List<Vector2> air, waterMM, smallLife, humusQuality, nitrogen, organicMatter;
        public static Spot[,] zpots;
        public Spot jordflekk;
        public List<Field> fields;

        //dette gjelder utprøvingsfasen. ett field er tilgjengelig. muld
        //public Farm farm;

        //public Field field = Farm.MuldTeig;
        public bool  useData4;
        public List<string> series1Data2; //streng variable. fx 1-28 dager
        GameObject graphGO;


         void Start()
        {
            
          /*  zpots = Field.Spots; //array som har alle jordlappene 

            testData = containerValues.MinVektorListe;// dette er måten eksterne vektorer kan bli tilgj
            //waterData = containerValues.MinVektorListe2;
            waterData = Weather.Rain;//vektorliste til graf
            tilf1Data = containerValues.MinVektorListe3;
            tilf2Data = containerValues.MinVektorListe4;
            soltimer = Weather.Sun;//vektorliste til graf

            */
            /*
            zpots = Field.Spots;
            air = zpots[0, 0].Air;
            smallLife = zpots[0, 0].SmallLife;
            humusQuality = zpots[0, 0].HumusQuality; ;
            nitrogen = zpots[0, 0].Nitrogen;
            organicMatter = zpots[0, 0].OrganicMatter;
            */
            graphGO = GameObject.Instantiate(emptyGraphPrefab);
            graphGO.transform.SetParent(this.transform, false);
            graph = graphGO.GetComponent<WMG_Axis_Graph>();
            graph.xAxis.AxisMaxValue = 28;

            initiateSecondGraph();

            /*
                        series1 = graph.addSeries();    //legger inn series1. den hentes vha graphGO.Getcomponent se linje 20
                        series2 = graph.addSeries();    // soltimer
                        series3 = graph.addSeries();    // waterData
                        series4 = graph.addSeries();
                        series5 = graph.addSeries();
                        series6 = graph.addSeries();
                        series7 = graph.addSeries();
                        series8 = graph.addSeries();
                        series9 = graph.addSeries();


                        series1.pointValues.SetList(waterMM);
                        series2.pointValues.SetList(soltimer);
                        series3.pointValues.SetList(waterData);
                        series4.pointValues.SetList(air ); //her settes tilfeldige verdier serie en
                        series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
                        series6.pointValues.SetList(smallLife);
                        series7.pointValues.SetList(humusQuality);
                        series8.pointValues.SetList(nitrogen);
                        series9.pointValues.SetList(organicMatter);


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

                       series1.UseXDistBetweenToSpace = true;

                        if (useData4)
                        {
                            // en bruk her er at vi kan velge ut grafer som skrives ut og ikke. useData2 er bool som gjør dette
                            Debug.Log("if useData er true");
                            List<string> groups = new List<string>();
                            List<Vector2> data = new List<Vector2>();

                            Debug.Log("for-løkke med series1Data2.Count=" + series1Data2.Count);
                            for (int i = 0; i < series1Data2.Count; i++)
                            {
                                Debug.Log("i=" + i);
                                Debug.Log("series1Data(" + i + "]:  " + series1Data[i]);
                            }
                            graph.groups.SetList(groups);
                            graph.useGroups = true;
                            graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;//Label'ene er definert og hentes fra WMG_Axis.labelTypes.groups
                            graph.xAxis.AxisNumTicks = groups.Count;

                            series1.UseXDistBetweenToSpace = true;
                            // series1.pointValues.SetList(data);///her setter vi pointValues til ?????ilordnes dette i det hele tatt???? T
                            Debug.Log("series1.pointValues.SetList(testData); testData[0]=" + testData[0] + "   testData[1]=" + testData[1]);
                        }
               */
        }//start()

        //void Start()
        //{

        //}


        void Update()
        {
            if (Initializer.GraphUpdated)
            {
                Initializer.GraphUpdated = false;
                //updateGraphs();
                Debug.Log("");
            }
        }



        public void updateGraphs()
        {
            Debug.Log("updateGraphs i WMG_X_Tutor       %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            soltimer = Weather.Sun;

            initiateSecondGraph();

            series2.pointValues.SetList(soltimer);
            series3.pointValues.SetList(waterData); //      
                                                    //series4.pointValues.SetList(tilf1Data); //her settes tilfeldige verdier serie en
                                                    //series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
        }

        public void initiateSecondGraph()
        {

   
        zpots           = Field.Spots;//array som har alle jordlappen

            Debug.Log("");

            air             = zpots[0, 0].Air;
            smallLife       = zpots[0, 0].SmallLife;
            humusQuality    = zpots[0, 0].HumusQuality; ;
            nitrogen        = zpots[0, 0].Nitrogen;
            organicMatter   = zpots[0, 0].OrganicMatter;
            testData        = containerValues.MinVektorListe;// dette er kanskje måten eksterne vektorer kan bli tilgjengelige
            waterData       = Weather.Rain;//vannmengde i jordbiten


            tilf1Data = containerValues.MinVektorListe3;
            
            soltimer = Weather.Sun;//vektorliste til graf




            graphGO = GameObject.Instantiate(emptyGraphPrefab);
            graphGO.transform.SetParent(this.transform, false);
            graph = graphGO.GetComponent<WMG_Axis_Graph>();
            graph.xAxis.AxisMaxValue = 28;





            /* int zpotLengde = zpots.GetLength(1);
            Debug.Log("int zpotLengde = zpots.GetLength(1);    zpotLengde=  " + zpots.GetLength(1));
            ///disse må tilordnes
            ///eksempel air= zpots[i,j].Air;*/
            Debug.Log("????????????????????????????????????????????????????????????????\ntest for import av array fra spot. via farm. air:(float)air[0].y     ->" + (float)air[0].y);
            //Debug.Log("test for import av array fra spot. via farm. air:(float)air[1].y     ->" + (float)air[1].y);
            //Debug.Log("test for import av array fra spot. via farm. air:(float)air[2].y     ->" + (float)air[2].y);
            //graphGO = GameObject.Instantiate(emptyGraphPrefab);
            //graphGO.transform.SetParent(this.transform, false);
            //graph = graphGO.GetComponent<WMG_Axis_Graph>();
           


            //Destroy(series2);
            //Destroy(series3);
            //Destroy(series4);
            //Destroy(series5);
            //Destroy(series6);
            //Destroy(series7);
            //Destroy(series8);
            //Destroy(series9);

            //PROBLEM. SER UT TIL Å SUMMERE SEG OPP
            //kan vi renske dataene først eller opprette en helt ny graf
            series1 = graph.addSeries();    //legger inn series1. den hentes vha graphGO.Getcomponent se linje 20
            series2 = graph.addSeries();    // soltimer
            series3 = graph.addSeries();    // waterData
            series4 = graph.addSeries();
         series5 = graph.addSeries();
            series6 = graph.addSeries();
            series7 = graph.addSeries();
            series8 = graph.addSeries();
            series9 = graph.addSeries(); 
/*   
*/

            series1.pointValues.SetList(waterMM);
            series2.pointValues.SetList(soltimer);
            series3.pointValues.SetList(waterData);
            series4.pointValues.SetList(air); //her settes tilfeldige verdier serie en
            series5.pointValues.SetList(tilf2Data); //her settes tilfeldige verdier serie to
            series6.pointValues.SetList(smallLife);
            series7.pointValues.SetList(humusQuality);
            series8.pointValues.SetList(nitrogen);
            series9.pointValues.SetList(organicMatter);

            Debug.Log("\n******************************************SKRIVER UT VEKTORER WATER SOL WATERD AIR HUMUSQ NITRO");
            printVector(waterMM,100);
            printVector(soltimer, 100);
            printVector(waterData, 100);
            printVector(air, 100);
            printVector(humusQuality, 100);
            printVector(nitrogen, 100);

            /**/
        }

        public void printVector(List<Vector2> flekk, float max)
        {
            for (int i = 0; i < 20; i++)
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



        //public static WMG_Axis_Graph Graph
        //{
        //    get
        //    {
        //        return graph;
        //    }
        //    set
        //    {
        //        graph = value;
        //    }
        //}





    }
}