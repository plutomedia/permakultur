using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Time.realTimeSinceStartup;


namespace HappyGardenConsoleVSU
{
    public class Initializer : MonoBehaviour
    {
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
        private List<Field> flds;
        //private Flora flr;
        public Spot[,] spotts;

        public Environments environments;
        Weather vaer;


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
            game = gameFrame.game;
            frm = game.farmen;
            flds = frm.fields;
 
            String nytekst; //hmm hva er forskjellen på String og string.
            string tempstring;
            myText.text = "";


            vaer = Weather.ThisDay;

             tempstring = String.Format("HappyGarden, Day {0}:   ",vaer.WhichDay);

            whatText.text = tempstring;

            //nytekst = String.Format("\nTeig{2}, spot({0},{1}):  ", ii, jj, i.Fieldnr);
            foreach (Field i in flds)
            {

                spotts = new Spot[10, 10];
                spotts = i.spots;
                string earthType="default";// default, to prevent error messages. Should be replaced with exception in switch.
                Debug.Log("i.fieldNr=" + i.Fieldnr);
                switch (i.Fieldnr)
                {
                    case 0:
                        earthType = "muldJord";
                        break;
                    case 1:
                        earthType = "moreneJord";
                        break;
                    case 2:
                        earthType = "myrJord";
                        break;
                }


                myText.text = myText.text + "\nTeig "+ i.Fieldnr+"  -  "+earthType; 

                for (int ii = 0; ii < 3; ii++)
                {
                    for (int jj = 0; jj < 3; jj++)
                    {
                        //spotts[ii, jj];
                        // Debug.Log("--->" + ii + " , " + jj + " :" + spotts[ii, jj].nutr_C\n);
                        nytekst = String.Format("\nSpot({0},{1}):  ",ii,jj);
                        myText.text += nytekst;

                        //Debug.Log("       --->    " + ii + " , " + jj + " :");
                        spotts[ii, jj].WriteSpot(myText);
                    }
                }

                myText.text += " \n";
            }
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
    }
}