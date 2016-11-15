using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;



namespace HappyGardenConsoleVSU
{
    public class Initializer : MonoBehaviour
    {
        private static bool graphUpdated=false;
        public Text myText;
        public Text whatText;
        //public MainController mainController;
        public GameFrame gameFrame;
        HappyGardenConsole console;

        public float lastFrameTime = 0.0f;
        public float thisInterval=0.0f;
        public float lastFixed = 0.0f;
  

        private Game game;
        private Farm frm;
        private  List<Field> flds;
        //private Flora flr;
        public Spot[,] spotts;

        public Environments environments;
        Weather vaer;

        public static int dagenIdag;  //dagen som det skal simuleres fra. dvs input som fx vanning og kalking og planting

        //public WMG_Axis_Graph grafen;
        public WMG_X_Tutor wmgTut;


        void Start()
        {


            Debug.Log("HAPPY GARDEN MAIN");


            //the below line create an object of singleton type
            //Singleton objFirstObject = Singleton.ObjSingle;
            ///objFirstObject.Name = "XXXXXXXXXXXXXX";

            //if we access the objSingle property from another variable we will get the same object
            //Singleton objSecondObject = Singleton.ObjSingle;
            //We have assigned the name in first object,we will get the same value in second object since both points
            //to the same instance

            ///String ut = String.Format("objFirstObject.Name={0} ,objSecondObject.Name={1}", objFirstObject.Name, objSecondObject.Name);


           /// Debug.Log(ut);
            //Console.WriteLine("objFirstObject.Name={0} ,objSecondObject.Name={1}", objFirstObject.Name, objSecondObject.Name);
            //Console.ReadLine();




            //Debug.Log("HAPPY GARDEN");
            environments = new Environments();
            gameFrame = new GameFrame(whatText);  //This is not good mvc. here we hand the view to the model, and intend to change the view from the model. No listeners.
                                                //but: the final view is not implemented that way. This is a development and debug view.
   
        }

        //static void Main(string[] args)
        //{
        //    Debug.Log("HAPPY GARDEN MAIN");
        //    //the below line create an object of singleton type
        //    Singleton objFirstObject = Singleton.ObjSingle;
        //    objFirstObject.Name = "ashish";
        //    //if we access the objSingle property from another variable we will get the same object
        //    Singleton objSecondObject = Singleton.ObjSingle;
        //    //We have assigned the name in first object,we will get the same value in second object since both points
        //    //to the same instance
        //    Console.WriteLine("objFirstObject.Name={0} ,objSecondObject.Name={1}", objFirstObject.Name, objSecondObject.Name);
        //    Console.ReadLine();
        //}

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
            //Debug.Log("Planter.Initializer, Plant-funksjonen");
            
            gameFrame.Plant(plantname, fieldNr, spotX, spotY);

            ///alternative måte. finn planteobjekt og sett dette lik det i floraen
            ///
             /*
             1 skriv ut floraen
             2 faste verdier initieres under opprettelse av floraen
             3 floraen lagres i json og hentes inn en gang
             3 noen av egenskapene er statiske andre ikke. Singleton???

            */
        }


        public void UpdateMonth(int dagenidag)
        {
            dagenIdag = dagenidag;
            //dette skal renovere alle arrayene.
            //første iterasjon: været forblir det samme
            //dette gjøres etterhvert automatisk
            //hvis det skal skje forandringer, må det foreligge noen
            //nye input
            //Nye input eller ikke. Det enkleste å programmere først er å 
            //kjøre en standard update, hvor nye utregninger med sine tilfeldigheter skjer
            //prøv først å kjøre en loop med 28 updates.
            //når denne funker er mye gjort.
            Weather vair = Weather.ThisDay;





            for (int day = 0; day < 7; day ++)
            {
                Weather.ThisDay.WhichDay = day;
                dagenIdag = day;//brukes fra Spot for å holde rede på dag-index

                Debug.Log(day+"x dag     ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||>");

                //Debug.Log("         water vektoren FØR vanntilordning:\n");
                //printVector(Spot.WaterMM, 100);

                gameFrame.Oppdater(1);
                //Debug.Log("         water vektoren etter vanntilordning:\n");
                //printVector(Spot.WaterMM, 100);

                gameFrame.Oppdater(2);
                gameFrame.Oppdater(3);
                gameFrame.Oppdater(4);

                Debug.Log(day + "x dag slutt     >|||||||||||||||||||");
                //Debug.Log("         water vektoren etter alle iterasjoner:\n");
                
                //Debug.Log("nitrogenvektoren:\n");
                //printVector(Spot.Nitrogen, 100);


            }
            Debug.Log("END OF UPDATE. NOW ITS TIME TO UPDATE GRAPH. MAKE A NEW GRAF   WMG_X_Tutor nygraf = new WMG_X_Tutor();");
            WMG_X_Tutor nygraf = new WMG_X_Tutor();
            //dette er ikke god MVC programmering. controlleren skal ikke direkte påvirke viewet

            //wmgTut.Starte();
            /* //jeg må prøve å opprette grafen etter initieringa.
            nygraf.updateGraphs();
          
            graphUpdated = true;
            */
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


    }//class Initializer
}//namespace
