using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Time.realTimeSinceStartup;



namespace HappyGardenConsoleVSU
{
    public class Spot
    {
        //time, day, iteration related
        public bool firstSimulation;    //er false inntil det er gjort en simulering på denne spot allerede
        public int dagenIdag=0;
        public int tempDay=0; //for storing day value temporary. We have both
                            //iteration by the Simulate all prodecure and
                            //day update by only the update button.
                            //these have to work together.

        public int fraDag=0;  //fra denne dag skal det simuleres. default er null

        //Her opprettes vektorene. De skal initieres videre i 1)InitializeType   og 2)Update
        Vector2 waterMMVec;
        Vector2 airVec;
        Vector2 organicMatterVec;
        Vector2 smallLifeVec;
        Vector2 humusQualityVec;
        Vector2 nitrogenVec;

        public float[] watering;//An array containing the amount of water added, for each day

        //kompost, næring og annet
        public bool mulch = false; //lagt på jorddekke av kompost

        //for the graph display we make vector-lists from some essential values
        public  List<Vector2> waterMM;
        public  List<Vector2> air;
        public  List<Vector2> organicMatter;
        public  List<Vector2> smallLife;
        public  List<Vector2> humusQuality;
        public  List<Vector2> nitrogen;

        public List<Vector2> sun = Weather.Sun; //denne hentes fra Weather.Sun
        public List<Vector2> rain = Weather.Rain; //denne hentes fra Weather.Rain

        public List<Vector2> temp = new List<Vector2>();

        //planterelatert
        public List<Vector2> pHeight=null; //hver spot har en slik liste. den er tom inntil vi skaper en plante
                                           //når planten lages, initieres listen med 0,0 verdier inntil dayPlanted hvis nødvendig. prøv:[6,0][7,2][8,4][9,5][10,8][11,10]

        //vannrelatert water related
        float waterAdded = 0f; //contain the local value of water added on each spot at every day.
                                //has to be reset every simulation, not to adding up old values

        System.Random rnd = new System.Random();

        // new values to be considered dependent of earth type (rich, wet, forest, old field)
        string earthType="default";
        private double topLayerSize;
        private double humusLayerSize;
        private double mineralEarthSize;

        // dictionary of small life
        // springtale, spretthale, collembola (små insektlignende dyr)
        // roundworms, rundorm, nematode . Gode og onde. De gode hjelper, de onde parasitterer
        // earthworm, meitemark
        private double earthworms, microbes, myceles, bacteriaes, nematodesPredator, springtales;
        private double nematodesRootKnot;

        //View related
        //public Text headingText; //overskriften arvet fra ititializer og farm
        public Text uttekst;    //utskrift av data spot etc

        //Weather related
        public Weather weather;
        int soltimer;
        
        //Spot related
        public int v_index;
        public int h_index;
        private int fieldnr;
        public  string spotID; //skal forenkle utskrift av v og h-indekser
        private  bool marked;     //alle som har blitt valgt før eller siden er merket
                                        // disse vil bli simulert.


        //Plant related (rydd opp i dette)
        private Plant planten;
        double plantGrowth;
        private string plantName;

        private bool planted;
        private bool fantesFraTidligere; //når vi simulerer et felt om igjen som allerede har en plante

        public List<Plant> planter;
        public Plant plant;


        //Macro næringsstoffer
        public double water_H2O;
        public double water;

         public double nutr_C;
        public double nutr_O;
        public double nutr_H;
        public double nutr_N;
        public double nutr_K;
        public double nutr_Ca;
        public double nutr_Ph;
        public double nutr_S;
        public double nutr_Mg;


        float _water,  _luftverdi, _smallLife, _nitrogen, _air, _humusQuality, _organicMatter; //brukes for å holde jordlappens verdier
        Weather vaer;

        float val_air;
        float val_Nitrogen;
        float val_OrgM;
        float val_sLife;




        public Spot(int fnr, int vert, int hor, Text myTex)
        {

            if ((hor == 0) && (vert == 0)) marked = true;//denne er kanskje overflødig
            //lager en initiering i field

            vaer = Weather.ThisDay;

            firstSimulation = true; //blir satt til true i iteration1 i update. Brukt for å bruke spotobjekt.Set(new Vector2) i stedet for Add)

            //headingText = myTex; //overskrift arvet fra initialize via farm
            h_index = hor;
            v_index = vert;
            fieldnr = fnr;
            spotID = String.Format("({0},{1})",v_index,h_index);
            Debug.Log("Ereating new spot. spotID= "+spotID);

            //iteration 1-4 related parameters. Temporarily placed in this class.
            plantGrowth = 1;    //dependent on sun, water and maybe certain other minerals or conditions


            //InitializeType(earthType); //other earth typew may be implemented later
            InitializeType("muldJord");


            InitializeSpot(); //start-dummy values. If game loaded, values will be loaded
            //printVectors(1);

        } ///public Spot Constructor





        public void printVectors(int max)
        {


            if (true)
            {
                int maxverdi = waterMM.Count;

                for (int j = 0; j < maxverdi; j++)
                {
                    Debug.Log("\nwaterMM      " + waterMM[j] + "=" + (float)waterMM[j].y);
                   Debug.Log("air          " + air[j] + "=" + (float)air[j].y);
                    Debug.Log("smallLife    " + smallLife[j] + "=" + (float)smallLife[j].y);
                    Debug.Log("humusQuality " + humusQuality[j] + "=" + (float)humusQuality[j].y);
                    Debug.Log("nitrogen     " + nitrogen[j] + "=" + (float)nitrogen[j].y);
                    Debug.Log("organicMatter " + organicMatter[j] + "=" + (float)organicMatter[j].y);
                }
                Debug.Log("\n");
            }   
        }



        public void InitializeType(string jordType)
        {
            ///Different soil types, different qualities
            ///
            ///  Muldjord Myrjord Morenejord   Sandjord Leirjord
            ///  Silty?   Peaty ?     Sandy Clay
            /// In our little farm, volume 1, we have three spots, each with different earth type. 
            /// 

            // InitializeSpot vil bare legge til noen forskjeller som avhenger av andre faktorer
            // som ...
            watering = new float[30];

            waterMMVec = new Vector2(0, 0);
            airVec = new Vector2(0, 0);
            organicMatterVec = new Vector2(0, 0);
            smallLifeVec = new Vector2(0, 0);
            humusQualityVec = new Vector2(0, 0);
            nitrogenVec = new Vector2(0, 0);


            waterMM = new List<Vector2>();
            air = new List<Vector2>();
            organicMatter = new List<Vector2>();
            smallLife = new List<Vector2>();
            humusQuality = new List<Vector2>();
            nitrogen = new List<Vector2>();

  /*
            switch (jordType)
            {
              

                case "muldJord":

                    //This will be the main vector-lists with earth data

                    val_air = 10;
                    val_Nitrogen = 1;
                    val_OrgM = 3;
                    val_sLife = 3;



                    _air            = val_air/20f;
                    _nitrogen       = val_Nitrogen/5f;
                    _organicMatter  = val_OrgM/25f;
                    _smallLife      = val_sLife/15f;


                    _water = 0.25f;
                    _luftverdi = 0.23f;
                    water = _water;


                    _humusQuality = (_air * 1 + _nitrogen * 2 + _organicMatter * 3 + _smallLife * 4) / (1 + 2 + 3 + 4);

                    //Debug.Log("beregnetNyvannverdi:" + water);

                    //small life
                    bacteriaes = 1500; //this means 1500 millions= 1 500 000 000
                    nematodesPredator = 50000;
                    springtales = 220;
                    nematodesRootKnot = 0; //these parasitic may be implemented ater a later stage;
                    earthworms = 10;

                    //other life
                    myceles = 0.5; //how to measure this is maybe in 0 to 1 (%)

                    //initialization for the graph-values

                    //waterMM.Add(new Vector2(0, _water*100));  //ganger med hundre pga grafen
                    //air.Add(new Vector2(0, (float)_air*100));
                    //smallLife.Add(new Vector2(0, _smallLife*100));
                    //humusQuality.Add(new Vector2(0, _humusQuality*100));
                    //nitrogen.Add(new Vector2(0, _nitrogen*100));
                    //organicMatter.Add(new Vector2(0,_organicMatter*100));


                    //vektorene skal tilpasses. her initieres de med 'nye verdier'
                    waterMMVec.Set      (0, _water * 100);
                    airVec.Set          (0, (float)_air * 100);
                    organicMatterVec.Set(0, _organicMatter * 100);
                    smallLifeVec.Set    (0, _smallLife * 100);
                    humusQualityVec.Set (0, _humusQuality * 100);
                    nitrogenVec.Set     (0, _nitrogen * 100);


                    waterMM.Add(waterMMVec);  //ganger med hundre pga grafen
                    air.Add(airVec);
                    smallLife.Add(smallLifeVec);
                    humusQuality.Add(humusQualityVec);
                    nitrogen.Add(nitrogenVec);
                    organicMatter.Add(organicMatterVec);

                    break;
                case "myrJord":
                    Debug.Log("Initialisering av jordverdier. Jordtype 'myrJord'");

                    break;
                case "moreneJord":

                    Debug.Log("Initialisering av jordverdier. Jordtype 'moreneJord'");
                  
                    break;
                case "default":
                    Debug.Log("Initialisering av jordverdier. Jordtype 'default', som ikke finnes.");

                    break;
            }//switch

            
            water_H2O = _water;
            water = _water;
            Debug.Log("EarthTypeInitializing._water=" + _water + " Water i spot ( " 
                + fieldnr + " , " + v_index + " , " + h_index + ") initialiseres med..........." + water);

            //Debug.Log("waterMM      " + waterMM[0] + "=" + (float)waterMM[0].y);

            int c = waterMM.Count;
            for (int i = 0; i < c; i++)
            {
                //Debug.Log(i+": init : waterMM      " + waterMM[i] + " = " + (float)waterMM[i].y);
            }

            */


            val_air = 10;
            val_Nitrogen = 1;
            val_OrgM = 3;
            val_sLife = 3;



            _air = val_air / 20f;
            _nitrogen = val_Nitrogen / 5f;
            _organicMatter = val_OrgM / 25f;
            _smallLife = val_sLife / 15f;


            _water = 0.25f;
            _humusQuality = (_air * 1 + _nitrogen * 2 + _organicMatter * 3 + _smallLife * 4) / (1 + 2 + 3 + 4);


            //vektorene skal tilpasses. her initieres de med 'nye verdier'
            waterMMVec.Set(0, _water * 100);
            airVec.Set(0, (float)_air * 100);
            organicMatterVec.Set(0, _organicMatter * 100);
            smallLifeVec.Set(0, _smallLife * 100);
            humusQualityVec.Set(0, _humusQuality * 100);
            nitrogenVec.Set(0, _nitrogen * 100);


            waterMM.Add(waterMMVec);  //ganger med hundre pga grafen
            air.Add(airVec);
            smallLife.Add(smallLifeVec);
            humusQuality.Add(humusQualityVec);
            nitrogen.Add(nitrogenVec);
            organicMatter.Add(organicMatterVec);


            Debug.Log("\nHER ER VERDIENE I JORDA sist i InitalizeType.");
            Debug.Log("Air " + _air + ", N " + _nitrogen + ", Org.matter " + _organicMatter + ". Small life " + _smallLife);
            Debug.Log("val_air " + val_air + ", val_Nitrogen " + val_Nitrogen + ", val_OrgM " + val_OrgM + ". val_sLife " + val_sLife);
            Debug.Log("HumusQuality: " + _humusQuality);
        }





        public void InitializeSpot()
        {
            //Default is not planted. Plant Name is 'ubeplantet'
            plantName = "Ubeplantet";
            planted = false;
            

            firstSimulation = true; //blir satt til true i iteration1 i update. Brukt for å bruke spotobjekt.Set(new Vector2) i stedet for Add)
        }



        public void WriteSpot(Text myText)
        {

            uttekst = myText;
            double plantHeight=0;
            int day;
            String nytekst;
            vaer = Weather.ThisDay;


            day = vaer.WhichDay;
            if (planted)
            {
                plantHeight = planten.Height;
                nytekst = String.Format("Water {0}, N {1}, C {2}, Ph {3}, S {4}, Plant: {5}, {6} cm", water_H2O, nutr_N, nutr_C, nutr_Ph, nutr_S, plantName, plantHeight);
            }
            else
            {
                nytekst = String.Format("Water {0}, N {1}, C {2}, Ph {3}, S {4},  Plant: ***", water_H2O, nutr_N, nutr_C, nutr_Ph, nutr_S);
            }

            uttekst.text += nytekst;
        }



        public void Update(int iterasjon)
        {
            // tilfelle 1: første gang. Arrayene er initiert i [0,0]
            // tilfelle 2: vi har kjørt en simulering fra før
            // hvis det er tilfelle 2, må vi
            // istedetfor å si thisspot.Add(new Vecor2) må vi si
            // tømme hele fx waterMM og lage nye vektorer
            // et tillegg: input så som plante, vanning etc lagres separat
            // så verdier som er puttet på fra før blir bevart. Slik kan vi legge til flere og flere ting etterhvert
            // bruker kanskje rainMM[0].Add rainMM.Set

            dagenIdag = Initializer.DagenIdag; //current day-index to be updated

            Debug.Log("\nDay "+dagenIdag+".  Iteration "+iterasjon+".  Update Spot "+spotID);
            double rainmm;
            int dagslystimer;

            //This is the same for every iteration. 4 iterations each day/period
           
           
            soltimer        = (int)(Weather.Sun[dagenIdag-1].y/10); //soltimene er lagret x10. Og timene er heltall..
            rainmm          = (int)(Weather.Rain[dagenIdag - 1].y / 10);
            dagslystimer    = (int)(Weather.Light[dagenIdag - 1].y / 10); //


            
            Debug.Log("Nedbør. (int)Weather.Rain[dagenIdag-1].y:   " +rainmm);
            Debug.Log("Soltimer. (int)Weather.Sun[dagenIdag-1].y:   "+ soltimer);


            //POSSIBLE SOLUTION TO MAKE THIS STRAIT
            // IF CASE: FIRST-TIME SIMULATION
            // IF CASE: LATER TIME && EXISTING PLANT: RENEW AND RE-SIMULATE. FORGET THE OLD ONE
            // THAT IS DONE IN ITERATION 1, AND ONLY ONCE FOR EACH OF THE NON-ENRITONMENTAL VECTORS

            

            switch (iterasjon)
            {
                case 1:
                    //First thign first. If spot is freshly initialized and never updated. then go on and add. 
                    // if not :
                    //checking first whether it is freshly made
                    //this has to be checked for all vectors? No, by checking one, you checking all ..(?)

                    Debug.Log("D E T T E   E R   S I M U L E R I N G   N R   " + Initializer.SimNumber);

                    Debug.Log("Simulation nr " + Initializer.SimNumber + "Vector waterMM[dagenIdag="
                        +dagenIdag+ "] . waterMM.Count= " + waterMM.Count+ " firstSimulation= "+ firstSimulation);


                    if (Initializer.SimNumber > 1)
                    {
                        firstSimulation = false;
                    }

                    if (Initializer.SimNumber==1)
                    {
                        Debug.Log("ALLER FØRSTE SIMULERING");
                        firstSimulation = true;             
                    }
                    else if ((dagenIdag == 1) && (Initializer.SimNumber > 1))
                    {
                        Debug.Log("IKKE FØRSTE SIMULERING. initializerer jorda på ny. muldjord");               

                        

                        // THIS IS THE PLACE TO RE-INITIALIZE ALL THE VECTORS.
                        // WHAT IS TO MAINTAIN THOUGH IS ENVIRONMENTAL VECTORS AND PLANTING DATA
                        // THE SAME PLANT HAS TO BE PLANTED ON THE SAME SPOT AND DATE
                        // 1 FIRST WE SEE THAT ALL EARTH VARIABLES ARE RENEWED
                        // 2 WE HAVE TO EMPTY THE VECTOR, CREATE  THE VECTOR AS A NEW VECTOR / VECTOR-LIST
                        // 3 THIRD WE SEE THAT WE CAN STORE AND GET THE PLANT, SO IT WILL BE SIMULATED AGAIN

                        // 1 INITIALIZING av jordverdier 
                        InitializeType("muldjord"); //har denne tilsiktet virkning. poenget er ved gjentatte simuleringer også
                        // 2 RECREATE THE VECTOR LISTS happens in InitializeType()
                       


                        //2 
                        if (planted)
                        {
                            Debug.Log("Her er allerede plantet. Planten må hentes igjen og simuleres på ny");
                            fantesFraTidligere = true; //brukes i Spot.Plant(); for å resette plante
                        }


                        //printVectors(1);
                        //skriv ut kontrollverdier for å se om alt virkelig er reinitiert
                    }



                    //String todayIs = vaer.Name;  //debugging purpose
                    double absorbtion = 0;
                    double absorbed = 0;
                    //rainmm = vaer.Nedboer;
                    double oldWatervalue = water_H2O;
                    double waterValueBeforeSun;

                    //soltimer = vaer.SunHours;
 /*      
                    Debug.Log("[iterasjon 1] soltimer " + soltimer+ ".  Water before rain update (water_H2O):    " + water_H2O);

                    //String nytekst = String.Format("\nEnvironment. Simulation day {0}, with weather.:   mm Nedbør {1},  Soltimer {2},  Dagslystimer {3},  Gj.Sn temp {4}", vaer.WhichDay, vaer.Nedboer, vaer.SunHours, vaer.LightHours, vaer.MeanTemp);
                    String nytekst = String.Format("\nBefore Rain Simulation day {0}, Weather.:   mm Nedbør {1},  Soltimer {2},  Dagslystimer {3}", dagenIdag, rainmm, soltimer, dagslystimer);

                    //if ((h_index == 0) && (v_index == 0))
                        Debug.Log(nytekst); //begrensning til bare en gang per Field
*/


                    //Calculates the weather influence on the earth with the values
                    //Only the water will influence the earth until later iterations
                    //With no earth covery and heavy rain: some minerals are washed away (over 45mm)
                    //With noe earth covery, more (or less) water will soak into the ground. (+- 20 %)
                    //there will be a random part of the wetness of the spot: e.g: +- 20 %
                    //When there is much water, less percentage of water is dissolved in the earth
                    //These principles will be interpolated and tweaked to simulate these effect. Not scientifically correct of course

                    // float overflow = 
                    //float absorption = vaer.Nedboer-((1-(vaer.Nedboer/100))* vaer.Nedboer); //eg 10mm rain gives 9mm absorbtion, 50mm gives 25, 100 mm gives
                    //float absorbtion = vaer.Nedboer * (1 - (1 / (1 + vaer.Nedboer)));

                    //All rain more than 70mm will flow away
                    if (rainmm > 70) rainmm = 70;

                    //THIS IS IMPORTANT: UNTIL 99 % OF THE WATER THE PLANT DRINKS MAY BE EMITTED AS SWEAT BY THE LEAVES. DRINKING AND SWEATING.
                    //THIS DRIES THE EARTH OUT MORE THAN EXPECTED. SEE 'TRANSPIRATION'

                    //Calculation of the degree of absorbtion. 1mm gives
                    //asymptotic to 0.5. Not more than half of the rainMM will absorb
                    //zero at zero and 

                    if (false)
                    {
                        for (int i = 1; i < 70; i += 5)
                        {
                            absorbtion = (-1) * (1 / (i + 0.5)) + 0.5; //
                            absorbed = i * (1 - absorbtion);
                            // Debug.Log("absorbtion=" + absorbtion + ",  absorbed=" + absorbed);
                        }
                        absorbtion = (-1) * (1 / (rainmm + 0.5)) + 0.5; //
                        absorbed = rainmm * (1 - absorbtion);
                    }

                    //I need a better formula for the absorbtion. First I use a dummy stair-level absorbtion model:
                    double Ny;
                    if (rainmm > 70)
                    {
                        Ny = Nyverdi(0.6, 1); // Debug.Log("regn > 70mm. vanntillegg: " + Ny);
                        water_H2O = Ny;//this is maximum
                    }
                    else if (rainmm > 50)
                    {
                        Ny = Nyverdi(0.5, 1); // Debug.Log("regn > 50mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 40)
                    {
                        Ny = Nyverdi(0.3, 1); // Debug.Log("regn > 40mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 30)
                    {
                        Ny = Nyverdi(0.20, 1); // Debug.Log("regn > 30mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 20)
                    {
                        Ny = Nyverdi(0.15, 1); // Debug.Log("regn > 20mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 10)
                    {
                        Ny = Nyverdi(0.1, 1); // Debug.Log("regn > 10mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 5)
                    {
                        Ny = Nyverdi(0.05, 1); // Debug.Log("regn > 5 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 2)
                    {
                        Ny = Nyverdi(0.02, 1); // Debug.Log("regn > 2 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 1)
                    {
                        Ny = Nyverdi(0.01, 1); // Debug.Log("regn > 2 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 0)
                    {
                        Ny = Nyverdi(0.05, 1); // Debug.Log("regn > 2 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm == 0)
                    {

                        //water_H2O = water_H2O;
                    }

                    if (water_H2O > 0.75) water_H2O = 0.75; //never more than 0.75, that's maximum soakable amount of water...

                    waterValueBeforeSun = water_H2O;

/*
                    Debug.Log("waterValueBeforeSun "+waterValueBeforeSun);
                    Debug.Log("soltimer " + soltimer);
                    Debug.Log("Water_H2O før regnvannsberegning:  " +oldWatervalue+    " ETTER regnvannberegning: "+water_H2O+ "  rainmm: "+rainmm) ;
*/                    double tempWater = water_H2O;


                    //Sun will dry the earth.
                    if (soltimer > 10)
                    {
                        water_H2O *= 0.8;

                    }
                    else if (soltimer > 9)
                    {
                        Ny = Nyverdi(0.83, 1); Debug.Log("sol > 9t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi (water_H2O*Ny)=" + water_H2O * Ny);
                        water_H2O *= Ny; //hvis avviket blir for stort så halveres det fx slik (tempWater+Ny)/2 eller noe lign.
                    }
                    else if (soltimer > 8)
                    {
                        Ny = Nyverdi(0.86, 1); Debug.Log("sol > 8t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 7)
                    {
                        Ny = Nyverdi(0.89, 1); Debug.Log("sol > 7t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 6)
                    {
                        Ny = Nyverdi(0.91, 1); Debug.Log("sol > 6t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 5)
                    {
                        Ny = Nyverdi(0.93, 1); Debug.Log("sol > 5t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 4)
                    {
                        Ny = Nyverdi(0.95, 1); Debug.Log("sol > 4t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 3)
                    {
                        Ny = Nyverdi(0.97, 1); Debug.Log("sol > 3t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }
                    else if (soltimer > 2)
                    {
                        Ny = Nyverdi(0.83, 1); Debug.Log("sol > 2t. Water før=" + water_H2O + "  Solfaktor: " + Ny + " Ny waterverdi =" + water_H2O * Ny);
                        water_H2O *= Ny;
                    }



                    Debug.Log("...................Spot (" + h_index + "," + v_index + ").Water_H2O først:  " + oldWatervalue + 
                        " ETTER rainwater: " + tempWater + ", ETTER soltimeuttørking(new walue): " + water_H2O + "  (rainmm: " + rainmm + ")");
                    Debug.Log("[ETTER ITERASJON 1]:Water_H2O   " + (float)water_H2O);


                    //disse tilordnes i iterasjon 4 og kan derfor ikke lenger skrives ut her slik det var i en tidlignere versjon av dette prod.
                    //waterMM.Add(new Vector2(dagenIdag, (float)water_H2O * 100));
                    //Debug.Log("waterMM[dagenIdag].y     " + waterMM[dagenIdag].y + ",    waterMM[dagenIdag-1]  " + waterMM[dagenIdag-1].y);


                    //Watering
                    //Water added this day watering[dagenIdag]
                    Debug.Log("WATERING, ("+h_index+","+ v_index+")="+ watering[dagenIdag]);

                    if (watering[dagenIdag] > 0)
                    {
                        water_H2O += watering[dagenIdag];
                        Debug.Log("EXTRA WATER ADDED. NEW WATER LEVEL: " + water_H2O);
                    }

















                    break;






                case 2:

                    float val_airOld;//bevarer de gamle jordverdier for sammenligningsformål
                    float val_NitrogenOld;
                    float val_OrgMOld;
                    float val_sLifeOld;
                    float _humusQualityOld;

                    Debug.Log("C A S E   2.     planted er " + planted);

                    Debug.Log("---------------------------------------------------earth update");
                    //Debug.Log("Iteration 2: Calculating new equilibrum between spots, according to env variables. Not implemented");
                    //Debug.Log("  Calculating humus factors. Especially water, oxygen, amount of small life and other");
                    //Debug.Log("waterlevel:" + water_H2O + " temperature: ?" + " acidity: ?"+"" );

                    Debug.Log("[iterasjon 2] soltimer " + soltimer + ".  Water before sun update (water_H2O):    " + water_H2O);

                    //_luftverdi = (float)air[0];
                    //_smallLife = (float)smallLife[tempDay].y;
                    //_nitrogen = (float)nitrogen[tempDay].y;
                    //_air = (float)air[0].y;
                    //_humusQuality = (float)nitrogen[tempDay].y;
                    //_organicMatter = (float)organicMatter[tempDay].y;

                    //VARIABLES
                    /* 
                     * QUALITY CONSIST OF 4 FACTORS
                     * 1 Air
                     * 2 Nitrogen
                     * 3 Organic stuff
                     * 4 small life
                     * 
                     * with weight-importance for quality
                     * Air 1, Nitrogen 2, Org matter 3, Animal life 4
                     * 
                     * Normal values in This earth (earttype "muldTeig". Initialized in 1st iteration
                     * Each matter is measured on a scale with different max. e.g Air[0,20]
                     *   here we have amount(air)/max(air)
                     *   
                     * FRA JORDINITIERINGEN HAR VI                      
                    val_air = 10;
                    val_Nitrogen = 1;
                    val_OrgM = 3;
                    val_sLife = 3;

                     * 1 Air            10/20;  //Start value for Air, amount 10, max 20
                     * 2 Nitrogen       1/5;
                     * 3 Organic stuff  4/25;
                     * 4 small Life     3/15;
                     * 
                     * Calculating quality of earth:
                     * starting values:
                     * veight(Air)          * value(Air)                = 1*10/20   = 0.5
                     * veight(Nitrogen)     * value(Nitrogen)           = 2*1/5     = 0.4
                     * veight(Organic matter) * value(Organic matter)   = 3*4/25    = 0.48
                     * veight(small Life)   * value(small Life)         = 4*3/15    = 0.8
                     * 
                     * max values for the materials:
                     * Air 1, Nitrogen 2, organic matter 3, small life 4
                     * 
                     * Calculations of the soil quality goes like this 
                     * (Sum of all amounts)/(max values) = (0.5+0.2+0.16+0.2)/(1+2+3+4)
                     *  = 2,18/10 = 0.22
                     *  On the soil quality scale from 1 to 10 [1:10] this is fairly poor soil
                     *  
                     *  This will be calculated every iteration 2.
                     *  Th plant will grow according to these three things:
                     *  Water, soil quality, temperature
                     *  
                     *  the result for the plant is
                     *  lack of water, dryes out and die but slowing the grow first
                     *  the health of the plant: will not be better than the soil permits
                     *  colder days slows the growth
                     *  */


                    //and now we calculate the new values of the soil
                    //float _water,  _luftverdi, _nitrogen, _air, _humusQuality, _organicMatter, _smallLife;

                    Debug.Log("\nHER ER VERDIENE I JORDA FØR NY BEREGNING.");
                    Debug.Log("Air "+_air+ ", N " + _nitrogen+ ", Org.matter " + _organicMatter+ ". Small life " + _smallLife);
                    Debug.Log("val_air " + val_air + ", val_Nitrogen " + val_Nitrogen + ", val_OrgM " + val_OrgM + ". val_sLife " + val_sLife);
                    Debug.Log("HumusQuality: " + _humusQuality);

                    //gamle verdier tas vare på for sammenligning
                    val_airOld = val_air;     
                    val_NitrogenOld = val_Nitrogen;
                    val_OrgMOld = val_OrgM;
                    val_sLifeOld = val_sLife;
                    _humusQualityOld = _humusQuality;

                    //beregning av jordverdiene for plotting
                    //_air = val_air / 20f;
                    //_nitrogen = val_Nitrogen / 5f;
                    //_organicMatter = val_OrgM / 25f;
                    //_smallLife = val_sLife / 15f;

                   // Debug.Log("NY BEREGNING:   Air " + _air + ", N " + _nitrogen + ", Org.matter " + _organicMatter + ". Small life " + _smallLife);

                    //SKAL FORANDRE BEREGNINGEN AV JORDKVALITET
                    // VAL-VERDIENE FÅR NYE VERDIER
                    // JORDKVALITETEN BEREGNES UT I FRA VERDIENE
                    // FAKTORER SOM FORANDRER VAL-VERDIENE

                    // air  mer organicMatter og smallLife øker air
                    // nitrogen proporsjonalt med organicMatter, øker hvis planten er N-produsent
                    // organicMatter øker langsomt hvis smallLife øker, øker mer hvis Mulch
                    // smallLife øker hvis humuskvaliteten er god

                    //beregner air.
                    //
                    if (val_air<20)
                    {
                        val_air= val_air*(1+(_organicMatter* _organicMatter )/ 20); //multipliserer med en variabel faktor.
                        val_air= val_air * (1 + (_smallLife) / 20);
                       // Debug.Log("val_air="+ val_air);
                    }

                    //beregner nitrogen
                    if (planted)
                    {
                        if ((planten.bindsNitrogen)&&(planten.Dead==false))
                        {
                            val_Nitrogen = val_Nitrogen * (1+((float)planten.Height)/10);

                          //  Debug.Log((1+((float)planten.Height)/10)+"val_Nitrogen=" + val_Nitrogen);

                        }
                        else if ((planten.bindsNitrogen==false) && (planten.Dead == false))
                        {
                            val_Nitrogen = val_Nitrogen * (1 + ((float)planten.Height) / 10000);

                        }
                        else if(planten.Dead)
                        {
                            val_Nitrogen = val_Nitrogen * 1;  //hva har plantedød å si for nitrogeninnholdet?
                            float rnd= random(val_OrgM / 50, val_OrgM / 100);
                            Debug.Log("random value nitrogen økning hvis planten er død. rnd tillegg: " + rnd);
                            val_OrgM += rnd;
                        }

                        // Debug.Log("val_Nitrogen=" + val_Nitrogen);

                    }

                    //beregner organicMatter
                    //mulch tilsetning
                    if (mulch)
                    {
                        val_OrgM *= (1 + (0.05f *(val_sLife/100)));
                    }
                    else
                    {
                        val_OrgM *= 0.99f;
                    }



                    //bedret humuskvalitet gir bonus for organisk matter og small life
                    if (_humusQualityOld > _humusQuality)
                    {
                        val_OrgM *= 1.01f;
                        val_sLife *= 1.01f;
                        
                    }
                    else
                    {
                        val_OrgM *= 1.01f;
                        val_sLife *= 1.01f;
                    }



                        //Debug.Log("val_OrgM=" + val_OrgM);
                        //Debug.Log("val_sLife=" + val_sLife);




                    //bool mulch er true hvis det er lagt på kompost-dekke
                    /*  if (mulch)
                      {
                          _luftverdi = _luftverdi * 1.01f;
                          _smallLife *= 1.01f;
                          _nitrogen *= 1.01f;
                          _air *= 1.01f;
                          _humusQuality *= 1.01f;
                          _organicMatter *= 1.01f;
                      }
                      else
                      {
                          _luftverdi = _luftverdi * 0.99f;
                          _smallLife *= 0.99f;
                          _nitrogen *= 0.99f;
                          _air *= 0.99f;
                          _humusQuality *= 0.99f;
                          _organicMatter *= 1f;
                      }
                      */
                    Debug.Log("planted er " + planted);
                    if (planted)
                    {
                       // nitrogen fratrekk og tillegg vedrørende planten. gjøres under case 4 (iterasjon4)
                    }
                    bool earthTeaPut = false; //denne brukes for å flagge om det er vannet med jord-te. 
                    if (earthTeaPut)
                    { 
                        //calculate new values. e.g mengde jordte er '100' dette fordeles og tilføyes jorda etterhvert
                    }

                    //HER BEREGNES PLOTTEVERDIENE FOR HUMUS
                    _air = val_air / 20f;
                    _nitrogen = val_Nitrogen / 5f;
                    _organicMatter = val_OrgM / 25f;
                    _smallLife = val_sLife / 15f;

                    Debug.Log("HER ER VERDIENE I JORDA ETT BER EREGNING. FØR PLOTTING");
                    Debug.Log("Air " + _air + ", N " + _nitrogen + ", Org.matter " + _organicMatter + ". Small life " + _smallLife);
                    Debug.Log("val_air " + val_air + ", val_Nitrogen " + val_Nitrogen + ", val_OrgM " + val_OrgM + ". val_sLife " + val_sLife);
                    Debug.Log("HumusQuality: " + _humusQuality);



                    //Debug.Log("\n___________________________________________________________Spot oppdateres dag " + dagenIdag + " > (" + v_index + " , " + h_index + " )");
                    //d Debug.Log("air  dagenIdag  _air=" + _air);

                    //disse push-uppene skal legges i case 4 = iterasjon 4, fordi flere faktorrer gjennom iterasj påvirker dem, så som planten
                    air.Add             (new Vector2(dagenIdag, (float)_air*100));
                    smallLife.Add       (new Vector2(dagenIdag, (float)_smallLife*100));
                    humusQuality.Add    (new Vector2(dagenIdag, (float)_humusQuality*100));
                    nitrogen.Add        (new Vector2(dagenIdag, (float)_nitrogen*100));
                    organicMatter.Add   (new Vector2(dagenIdag, (float)_organicMatter*100));


                    //Debug.Log(".....  waterMM="+ waterMM[dagenIdag] + "   waterMM["+ dagenIdag + "].y= " + waterMM[dagenIdag].y+ ".....  air    " + air[tempDay].y);

                    //Debug.Log("waterMM[dagenIdag].y     " + waterMM[dagenIdag].y + ",    waterMM[dagenIdag-1]  " + waterMM[dagenIdag - 1].y);

                    Debug.Log("[ETTER ITERASJON 2]:Water_H2O   " + (float)water_H2O);

                    break;
                case 3:

                    //planten
                    //Debug.Log("---------------------------------------------------plant update");
                    //Debug.Log("Iterationi 3: Calculating influence on each spot from Plants. Not implemented");
                    //Debug.Log("oppdaterer spot (x,y)  "+ v_index + " , " + h_index);
                    //Debug.Log("p l a n t e d   er   " + planted);
                    //soltimer = vaer.SunHours;

                    // if (plantet == true) Debug.Log("ja, planted er true");
                    Debug.Log("[iterasjon 3] Plante. soltimer " + soltimer + ".  Planted=" + planted);



                    if (planted == false) break;
                    Debug.Log("plantet er true, regner ut spot og plante-influens");

                    Debug.Log("[iterasjon 3] Plante. soltimer " + soltimer + ".  Planted=" + planted + "   Plantehøyde    " + planten.Height);


                    int plantePlantet = planten.dayPlanted;
                    if (dagenIdag > plantePlantet)
                    {
                        Debug.Log("oppdaterer planten. dagenIdag=" + dagenIdag + ",  fraDag(plantet dag)=" + fraDag);
                        planten.Oppdater(this);

                        pHeight.Add(new Vector2(dagenIdag, (float)planten.Height)); 

                    }

                 


                    if (planten.Dead)
                    {
                        String ut2 = String.Format("spot ({0},{1}) {2}. is  no a late {2}. It died by heat and lack of water.", h_index, v_index, planten.name_no);
                        
                        Debug.Log(ut2);
                        //planten = null;

                        break;
                    }

                    ///beregne vann
                    ///
                    //faktorer som bestemmer vannoppsuging: størrelse og 'consumeWater'
                    //consumeWater er egentlig en funksjon hvor mengden avgjøres av
                    //faktorer som vekstperiode, tørrhet/sol/temp
                    //Dette må forenkles. Bruker to av parametrene
                    //vannoppsuging = soluttørring (soltimer) + størrelsesfaktor (height)
                    // if sol then faktor = soltimer*Height(cm)/100

                    double plantHeight = planten.Height;

               

                    //the 'dryout-factor' depends on Plant height and sunHours. 
                    //the purpose is to see in which amount the water will be sucked from the ground (the newwaterdry formula)
                    //double dryout = (plantHeight * (double)soltimer) / 1000; 
                    //THIS 'newwaterdry' IS ACTUALLY NOT FOR IMPLEMENTING HERE, BUT UNDER ITERATION 1 (EARTH UPDATE, CASE 1). 'newwaterdry' may be used in case 1. Better??
                    //Weather conditions. sun will influence the water need of the plant. The need of water is of course also dependent on the size/height of the plant
                    //double newwaterdry=(water_H2O - water_H2O * dryout);//temporary functional variable. Replace will be with the calculation in iteration 1 (weather factors)


                    //if there is much sun, the plant needs more water. Make a Faktor
                    double waterNeedSun = 1 + (double)soltimer / 10;   //e.g 30cm and 4 hours: 3*4/1000=0.12
                   //?   double waterNeedEveryDay = 1; //trenges denne? hvisikkeslett
                    Debug.Log("planteiterasjonen. waterNeedSun=" + waterNeedSun); //utgangspunkt-index 

   
                    //Water need according to plant height. Make a Faktor
                    double waterNeedPlant = (plantHeight / 2) * (plantHeight / 2) / 1000;
                    waterNeedPlant *= 0.5; //just to tweak it before I make another formula 
                    Debug.Log("planteiterasjonen. waterNeedPlant=" + waterNeedPlant);
                 

                    
                    //factor, waterNeedSun will be multiplied by the 'sun' factor. If much sun, the greater the factor. Influence the thirst of the Plant
                    double waterNeedPlantOld= waterNeedPlant;
                    waterNeedPlant *= waterNeedSun;//waterNeedSun is a factor decided by the dryness and sunhours



                    String ut0 = String.Format("spot ({4},{5}) {6}. Soltimer={0} gives waterNeedSun-factor={1}. PlantHeight={2} waterNeedPlant=PlantHeight**2/1000={3}", soltimer, waterNeedSun, planten.Height, waterNeedPlant,h_index,v_index,planten.name_no);
                    Debug.Log(ut0);

                   // String ut1 = String.Format("waterNeed {0} * ", sunHours, waterNeedSun, planten.Height);
                  //  Debug.Log(ut0);


                    String ut = String.Format("spot ({4},{5}) {6}. New earth water Level:   OldWaterlevel( {0} ) - waterNeedPlant({1}) * waterNeedSun-factor({2}) = newwaterdrink {3}", water_H2O, waterNeedPlantOld, waterNeedSun, water_H2O - waterNeedPlant, h_index, v_index, planten.name_no);
                    Debug.Log(ut);

                    //   U P D A T E.  Maybe ok, but check if tempDay is right
                    // // // // pHeight.Add(new Vector2(tempDay, (float)plantHeight));

                    //if it is too dry (water_H2O < 0.15) two things will happen:
                    //the plant will suffer, getting dryer. A dryout-index will control the health state. The health state will consist of different faktors (water, minerals)
                    //dryout effect will also reduce the growth
                    //here is an algorithm/condition that will deal with this case
 
                    if (water_H2O- waterNeedPlant < 0.1)
                    {
                        Debug.Log("Too dry. Plant will grow slower and start to detoriate/suffer.");
                        planten.Health -= 1;
                    }
                    else if (water_H2O - waterNeedPlant < 0)
                    {
                        Debug.Log("No Water. Plant will not grow and are detoriating fast.");
                        water_H2O = 0;
                        planten.Health -= 3;
                    }
                    else
                    {
                        water_H2O = water_H2O - waterNeedPlant;
                    }


                    Debug.Log("[ETTER ITERASJON 3]:Water_H2O   " + (float)water_H2O);


                    break;

                case 4:

               
                   // dagenIdag = Initializer.DagenIdag; //current day-index to be updated

                    Debug.Log("\n[iterasjon "+iterasjon+"] Day " + dagenIdag + ".  Update Spot " + spotID);

/*
                    if (waterMM[dagenIdag] == null)
                    {
                        Debug.Log("Vector waterMM[dagenIdag] peker på null "+ waterMM[dagenIdag]);
                    }
                    else
                    {
                        Debug.Log("dagenIDag="+dagenIdag+" (første index er 1, finner data fra (dagenIDag-1)");

                        Debug.Log("Vector  waterMM[" + 0 + "] har verdi allerede=" + waterMM[0]);
                        Debug.Log("Vector  waterMM[" + 1 + "] har verdi allerede=" + waterMM[1]);
                        Debug.Log("Vector  waterMM[" + 2 + "] har verdi allerede=" + waterMM[2]);

                    }
*/

                    //OG TIL SIST, I SISTE ITERASJON. VI PUSHER OPP NY VECTOR2, DETTE SKAL IKKE GJØRES UNDERVEIS. 
                    //UNDERVEIS OPPDATERES BARE DE MIDLERTIDIGE, LOKALE VARIABLE

                    //if (firstSimulation)
                    //{
                    //    //Vector2 lokalvektor = waterMMVec;   // add og modifiser eventuelt denne her
                    //    waterMMVec.Set(dagenIdag, _water * 100);
                    //    Debug.Log("waterMMVec  "+waterMMVec);

                    //    waterMM.Add(waterMMVec);
                    //}
                    //else
                    {
                        //    Debug.Log("bytter verdier. dagenIdag=" + dagenIdag + " waterMMVec " + waterMMVec+"waterMM.Count =" +waterMM.Count);
                        //    waterMMVec.Set(dagenIdag, _water * 100);
                        //    waterMM.RemoveAt(dagenIdag);
                        //    waterMM.Add(waterMMVec);


                    }

                    //Debug.Log(",    waterMM[dagenIdag-1]  " + waterMM[dagenIdag - 1].y);

                    //Debug.Log("waterMM[dagenIdag].y     " + waterMM[dagenIdag].y );

                    waterMMVec.Set(dagenIdag, (float)water_H2O*100);
                    waterMM.Add(waterMMVec);

                    //Debug.Log("waterMM"); printVector(waterMM, 100);





                    break;
                default:
                    break;





            }//switch
            int dagg = Weather.ThisDay.WhichDay;
            //Debug.Log("\n Weather.ThisDay.WhichDay  " + Weather.ThisDay.WhichDay);
/*
            if (iterasjon == 4)
            {
                Debug.Log("\n Weather.ThisDay.WhichDay  " + Weather.ThisDay.WhichDay);
                Debug.Log("\n Alle iterasjoner gjort. siste iterasjon  "+iterasjon+"*********************************SKRIVER UT VEKTORER WATER SOL WATERD AIR HUMUSQ NITRO");
                    Debug.Log("waterMM"); printVector(waterMM, 100);
                    Debug.Log("soltimer"); printVector(sun, 100);
                    Debug.Log("waterData"); printVector(rain, 100);
                    Debug.Log("air"); printVector(air, 100);
                    Debug.Log("humusQuality"); printVector(humusQuality, 100);
                    Debug.Log("nitrogen"); printVector(nitrogen, 100);
                Debug.Log("Plantehøyde"); printVector(pHeight, 100);

                Debug.Log("\n");
            }
            */

            //dette utføres etter hver iterasjon 
            //
            Debug.Log("\n");
        }//Update




        public void printVector(List<Vector2> flekk, float max)
        {
            int range = flekk.Count;
            Debug.Log("lengde: " + range);
            for (int i = 0; i < range; i++)
            {
                Debug.Log("printVector  " + flekk[i].x+","+flekk[i].y);
            }
        }



        public void PourWater()
        {
            // This Spot is watered 
            // Dummy variable is watered until 0.4
            // More water is excessive

            Debug.Log("planten vannes. vann-tilsatt før:" + watering[Initializer.DagValgt]);
            
            watering[Initializer.DagValgt] += 0.15f; //amount of water add this day
            Debug.Log("planten vannes. Samlet tilsetning:" + watering[Initializer.DagValgt]);
        }


        private double tilf_Tall(double verdi, int avvike)
        {
            double resultat=0;
            double tilf;
            int tilf2;
            int faktor = 0;
            double nyverdi=666;
            Debug.Log("Tilftall funksjon. Skal ikke brukes");

            Debug.Log("\nverdi=" + verdi + "  avvike="+avvike);
            System.Random rnd = new System.Random();

            tilf = rnd.Next(0, 10*avvike);
            tilf2 = rnd.Next(0, 1);
            if (tilf2 == 0) faktor = -1; else faktor = 1;

                //Debug.Log("tilf:" + tilf+"  faktor="+faktor);

            tilf /= 10;
                //Debug.Log("tilf:" + tilf + "  faktor=" + faktor);
            tilf *= faktor;
                //Debug.Log("tilf:" + tilf + "  faktor=" + faktor);
            nyverdi = verdi+verdi * tilf;
            //Debug.Log("resultat/random:" + tilf + "  faktor=" + faktor+",  nyverdi="+verdi);
            return tilf; 
        }


        private double Nyverdi(double verdi, int avvike)
        {
           
            double tilf;
            double tilf2;
            double faktor = 0;
            double nyverdi = 666;

            int maxverdi, minverdi, mediumtall;
            //System.Random rnd = new System.Random();

             mediumtall = (int)(verdi * 1000); //hvis fx verdi == 0.676, mediumtall vill være  67
            double avvik = (double)(mediumtall / 3);
            if ((mediumtall + avvik) > 999)
            {
                 maxverdi = 999;
            }
            else
            {
                maxverdi = mediumtall + (int)avvik;
            }

            if ((mediumtall - avvik) < 1)
            {
                minverdi = 1;
            }
            else
            {
                minverdi = mediumtall - (int)avvik;
            }

            //int maxavvik = (int)((1 - verdi) * 100); //da vil fx (1-0.675)*100 være  32. tilfeldig tall skal da hentes mellom 67-32 og 67+32

//NY MÅTE Å BEREGNE
            double tilfTallNy = rnd.Next(minverdi, maxverdi);
            
            String tilftekst = String.Format("verdi={0}, mediumtall={1}, avvik={2}, minverdi={3}, maxverdi={4}, tilfTallNy={5}, tilfTallNy/1000={7}, nyverdi:={6}", verdi, mediumtall, avvik, minverdi, maxverdi, tilfTallNy, nyverdi,(tilfTallNy/1000));
            tilfTallNy = (tilfTallNy / 1000);
           // Debug.Log(tilftekst);

            nyverdi = tilfTallNy;
           


            //GAMMEL MÅTE:  tilf = rnd.Next(0, (2+2 * avvike)*10);//Denne må tweakes mer. Bruk statistisk formel
 /*           tilf2 = rnd.Next(0, 2);
            if (tilf2 == 0) faktor = -1; else faktor = 1;
            tilf /= 100;
            tilf *= faktor;
            //Debug.Log("tilf=" + tilf);

            nyverdi = verdi+verdi * tilf;
            if (verdi != 0)
            {
                String nytekst = String.Format("Genererer spot({3},{4}). nyverdi {0} = verdi {1} + verdi {1} * tilf {2} = nyverdi {0}\n",nyverdi, verdi, tilf,h_index,v_index);
                //Debug.Log(nytekst);
            }
*/

            //Debug.Log("  Verdi="+verdi+"  Nyverdi=" + nyverdi);
            return nyverdi;
        }//Nyverdi


        private decimal NyverdiDecimal(decimal verdi, int avvike)
        {

            decimal tilf;
            decimal tilf2;
            decimal faktor = 0;
            decimal nyverdi = 666;

            Debug.Log("\nverdi=" + verdi + "  avvike=" + avvike+"DETTE ER NYVERDI-FUNKSJONEN------------------------------------");
            

            tilf = rnd.Next(0, 5 + 2 * avvike);//Denne må tweakes mer. Bruk statistisk formel){

            tilf2 = rnd.Next(0, 2);

            if (tilf2 == 0) faktor = -1; else faktor = 1;

            tilf /= 10;
            tilf *= faktor;
            //Debug.Log("tilf:" + tilf + "  faktor=" + faktor);

            nyverdi = verdi + verdi * tilf;
            //Debug.Log("resultat/random:" + tilf + "  faktor=" + faktor + ",  nyverdi=" + nyverdi);


            String nytekst = String.Format("NyverdiDecimal()  nyverdi {0} = verdi {1} + verdi {1} * tilf {2} = nyverdi {0}\n", nyverdi, verdi, tilf);
            //Debug.Log(nytekst);
            return nyverdi;
        }


        private double RandomNr(int min, int max)
        {

            double tilfTall = 0;
            
            tilfTall = rnd.Next(min, max + 1);
                 Debug.Log("RandomNr(="+tilfTall);
            String nytekst = String.Format("RandomNr ( {0},{1} ) = {2}",min,max,tilfTall);
            Debug.Log(nytekst);
            return tilfTall;
        }

        public float random(float midtp, float avvik)
        {
            float Pi = 3.14159265f;
            System.Random rng = new System.Random();
            float xsum = 0;
            float ysum = 0;

            Debug.Log("xsum:  " + xsum);

            int ant = 10000;
            float minx, miny, maxx, maxy;


            float midtpunkt = 0.5f;
            float Avvik = 0.01f;

            midtpunkt = midtp;
            Avvik = avvik / 10;

            minx = midtpunkt; miny = midtpunkt;//skal ende opp sim min-verdier for x hhv y
            maxx = midtpunkt; maxy = midtpunkt;

            for (int i = 0; i < ant; i++)
            {
                float temp = Mathf.Abs(-2 * Mathf.Log(rng.Next()));
                float r = Mathf.Sqrt(temp);
                float th = 2 * Pi * rng.Next();
                float x = r * Mathf.Cos(th);
                float y = r * Mathf.Sin(th);

                //midtpunkt og avvik. tweake til vårt scope
                x *= avvik;
                x += midtpunkt;

                //beregner gjennomsnitt og max-avvik
                xsum = xsum + x;
                ysum = ysum + y;

                if (x < minx) minx = x;
                if (y < miny) miny = y;

                if (x > maxx) maxx = x;
                if (y > maxy) maxy = y;

                string tempstr = String.Format("x={0}, y={1}  ", x, y);
                //Debug.Log(tempstr);
            }

            float xmid = xsum / ant;
            float ymid = ysum / ant;
            Debug.Log("\nMidtpunkt=" + midtpunkt+"  Avvik="+Avvik);

            string tempstring = String.Format("Gjennomsn x={0}, maxavvikx={1}  minx {2} maxx {4}  [miny {3} maxy {5}]", xmid, Mathf.Abs(midtpunkt) - Mathf.Abs(minx), minx, miny, maxx, maxy);
            Debug.Log(tempstring);

            return xmid;
        }




        public void Plant(string namn, int fieldNr, int spotX, int spotY)
        {
            plantName = namn;
            


            if (planted==false)
            {
                //ingen plante plantet, i denne simuleringen eller i foregående
                //Da resetter vi vektor-lista og lager en ny plante 
                Debug.Log(" Spot: HER PLANTES PLANTEN: "+spotX+","+spotY+"  " + namn);

                pHeight = null;
                pHeight = new List<Vector2>();
                planted = true; //ikke så robust, må huske å forandre verdien hvis planten fjernes eller dør.
                planten = new Plant(Initializer.DagValgt, plantName, fieldNr,spotX, spotY);


                pHeight.Add(new Vector2(Initializer.DagValgt, 0));
            }
            else if ((planted==true)&&(fantesFraTidligere == true))
            {
                // her finnes planten fra før
                Debug.Log(" Spot: PLANTEN ER PLANTET I TIDLIGERE SIMULERING. FARE FOR DOBLING: " + spotX + "," + spotY + "  " + namn);

                pHeight = null;
                pHeight = new List<Vector2>();
                planted = true; //ikke så robust, må huske å forandre verdien hvis planten fjernes eller dør.
                planten = new Plant(Initializer.DagValgt, plantName, fieldNr, spotX, spotY);

                pHeight.Add(new Vector2(Initializer.DagValgt, 0));
            }
        }


        public  bool Planted
        {
            get { return planted; }
            set { planted = value; }
        }



        // denne skal sende peker til parent-felt
        public static Field felt
        {
            get
            {
                return felt;
            }
            set
            {
                felt = value;
            }
        }


        public List<Vector2> WaterMM
        {
            get
            {
                return waterMM;
            }
            set
            {
                waterMM = value;
            }
        }


        public  List<Vector2> Air
        {
            get
            {
                return air;
            }
            set
            {
                air = value;
            }
        }

        public List<Vector2> PHeight
        {
            get
            {
                return pHeight;
            }
            set
            {
                pHeight = value;
            }
        }

        public  List<Vector2> SmallLife
        {
            get
            {
                return smallLife;
            }
            set
            {
                smallLife = value;
            }
        }


        public  List<Vector2> HumusQuality
        {
            get
            {
                return humusQuality;
            }
            set
            {
                humusQuality = value;
            }
        }


        public  List<Vector2> Nitrogen
        {
            get
            {
                return nitrogen;
            }
            set
            {
                nitrogen = value;
            }
        }


        public  List<Vector2> OrganicMatter
        {
            get
            {
                return organicMatter;
            }
            set
            {
                organicMatter = value;
            }
        }

        public  String SpotID
        {
            get
            {
                return spotID;
            }
            set
            {
                spotID = value;
            }
        }


        public Plant Planten
        {
            get
            {
                return planten;
            }
            set
            {
                planten = value;
            }
        }


        public bool Marked
        {
            get
            {
                return marked;
            }
            set
            {
                marked = value;
            }
        }
    }//class Spot
}
