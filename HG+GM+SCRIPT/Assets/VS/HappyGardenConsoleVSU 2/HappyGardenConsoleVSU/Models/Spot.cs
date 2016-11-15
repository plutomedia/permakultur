using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Time.realTimeSinceStartup;

namespace HappyGardenConsoleVSU
{
    public class Spot
    {
        public int tempDay=0; //for storing day value temporary. We have both
                            //iteration by the Simulate all prodecure and
                            //day update by only the update button.
                            //these have to work together.

        public int fraDag=0;  //fra denne dag skal det simuleres. default er null

        public List<Plant> planter;
        //Flora2 flora2;
        public Plant plant;

        public bool mulch = false; //lagt på jorddekke av kompost

        //for the graph display we make vector-lists from some essential values
        public  List<Vector2> waterMM = new List<Vector2>();
        public  List<Vector2> air = new List<Vector2>();
        public static List<Vector2> organicMatter = new List<Vector2>();
        public static List<Vector2> smallLife = new List<Vector2>();
        public static List<Vector2> humusQuality = new List<Vector2>();
        public static List<Vector2> nitrogen = new List<Vector2>();

        public List<Vector2> temp = new List<Vector2>();



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

        //Plant related
        Plant planten;
        double plantGrowth;
        private string plantName;
        private bool planted;
        public bool plantet;

        //Macro næringsstoffer
        public double water_H2O;
        public double nutr_C;
        public double nutr_O;
        public double nutr_H;
        public double nutr_N;
        public double nutr_K;
        public double nutr_Ca;
        public double nutr_Ph;
        public double nutr_S;
        public double nutr_Mg;

        //Micro næringsstoffer. I will not use this in this simulation.
        public double min_Fe;
        public double min_Mn;
        public double min_Zn; //this list will be updated later.
            public decimal min_B;
        public double min_Cu;
        public double min_Mo;
        


        //Other Chemicals;
        //public double min_Hg;
        public double nutr_Pot;
        public double nutr_Sulfur;

        //To avoid labour at a later stage I create these 'superfluous' variables.
        //Why? These variables will be a result of calculations and mathematics.
        //this will be done in other classes at a later stage.
        public double water;
        public double valC; // 
        public double valO; // 
        public double valH;  // 
        public double valN; // 
        public double valK;  // 
        public double valCa; // 
        public double valPh;  // 
        public double valS;  // 
        public double valMg;  // 

        public double valFe;    //
        public double valMn;    //
        public double valZn;    //
         public decimal valB;    // kanskje jeg kan bruke decimal i stedet for double. fordeler?
        public double valCu;    //
        public double valMo;    //

        private int sunHours;   //These values are gathered from Environments via get set
        private int Nedboer;
        private int LightHours;
        private int MeanTemp;

        float _water,  _luftverdi, _smallLife, _nitrogen, _air, _humusQuality, _organicMatter; //brukes for å holde jordlappens verdier

        Weather vaer;
        //the abundance of these minerals will decide the content of the same in the plants.
        // good? explanation of effects on plants:
        /* http://www.holganix.com/blog/bid/59536/
         * The-Science-Behind-Holganix-The-6-Essential-Nutrients-for-Healthy-Plants
        */



        public Spot(int fnr, int vert, int hor, Text myTex)
        {
           
            vaer = Weather.ThisDay;

            int tempDay;
            tempDay = vaer.WhichDay;
            
           

            //headingText = myTex; //overskrift arvet fra initialize via farm
            h_index = hor;
            v_index = vert;
            fieldnr = fnr;

            //iteration 1-4 related parameters. Temporarily placed in this class.
            plantGrowth = 1;    //dependent on sun, water and maybe certain other minerals or conditions

            Debug.Log("..............................................................................Spot Initialiseres> (" + vert + " , " + hor + " )");


            switch (fieldnr)
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



            InitializeType(earthType); //other earth typew may be implemented later

            //printVectors();
           
            InitializeSpot(); //start-dummy values. If game loaded, values will be loaded
            printVectors();
        } ///public Spot Constructor



        public void printVectors()
        {
            Debug.Log("PRINTVECTORS, v_index= "+v_index+", h_index= "+h_index+ ",  day "+tempDay);

            Debug.Log("");

            Debug.Log("waterMM      " +       waterMM[tempDay] +   "=" + (float)waterMM[tempDay].y);
            Debug.Log("air          " +           air[tempDay] +   "=" + (float)air[tempDay].y);
            Debug.Log("smallLife    " +     smallLife[tempDay] +   "=" + (float)smallLife[tempDay].y);
            Debug.Log("humusQuality " +  humusQuality[tempDay] +   "=" + (float)humusQuality[tempDay].y);
            Debug.Log("nitrogen     " +      nitrogen[tempDay] +   "=" + (float)nitrogen[tempDay].y);
            Debug.Log("organicMatter " + organicMatter[tempDay] +   "=" + (float)organicMatter[tempDay].y);
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



            switch (jordType)
            {
                //Earth Life has to be dramatically simplified

                case "muldJord":


                   Debug.Log("Initialisering av jordverdier. Jordtype 'muldJord': "+jordType);
               /*     topLayerSize = 5;
                    humusLayerSize = 30;
                    mineralEarthSize = 30;
                    water = 0.3f;
                    water = Nyverdi(water, 1);
*/
                    //This will be the main vector-lists with earth data
                    _water = 0.25f;
                    _air = 0.2f;
                    _luftverdi = 0.23f;
                    _smallLife = 0.09f;
                    _nitrogen = 0.15f;
                    _humusQuality = 0.5f;
                    _organicMatter = 0.2f;

                    water = _water;

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

                waterMM.Add(new Vector2(0, _water*100));  //ganger med hundre pga grafen
                air.Add(new Vector2(0, (float)_air*100));
                    smallLife.Add(new Vector2(0, _smallLife));
                    humusQuality.Add(new Vector2(0, _humusQuality));
                    nitrogen.Add(new Vector2(0, _nitrogen));
                    organicMatter.Add(new Vector2(0,_organicMatter));

                    break;
                case "myrJord":
                    Debug.Log("Initialisering av jordverdier. Jordtype 'myrJord'");

                    break;
                case "moreneJord":

                    Debug.Log("Initialisering av jordverdier. Jordtype 'moreneJord'");
                    /*
                    //
                    topLayerSize = 5;
                    humusLayerSize = 25;
                    mineralEarthSize = 40;
                    water = 0.3;
                    water = Nyverdi(water, 1);

                    //small life
                    bacteriaes = 1000; //this means 1500 millions= 1 500 000 000
                    nematodesPredator = 20000;
                    springtales = 220;
                    nematodesRootKnot = 0; //these parasitic may be implemented ater a later stage;
                    earthworms = 4;

                    //other life
                    myceles = 0.5; //how to measure this is maybe in 0 to 1 (%)


                    //initialization for the graph-values
                    waterMM.Add(new Vector2(0, 0.37f));
                    air.Add(new Vector2(0, 0.2f));
                    smallLife.Add(new Vector2(0, 0.08f));
                    humusQuality.Add(new Vector2(0, 0.35f));
                    nitrogen.Add(new Vector2(0, 0.12f));
                    organicMatter.Add(new Vector2(0, 0.2f));
                    */
                    break;
                case "default":
                    Debug.Log("Initialisering av jordverdier. Jordtype 'default', som ikke finnes.");

                    break;
            }//switch

            
            water_H2O = _water;
            water = _water;
            Debug.Log("EarthTypeInitializing._water=" + _water + " Water i spot ( " + fieldnr + " , " + v_index + " , " + h_index + ") initialiseres med..........." + water);

            //Debug.Log("waterMM      " + waterMM[0] + "=" + (float)waterMM[0].y);

            int c = waterMM.Count;
            for (int i = 0; i < c; i++)
            {
                Debug.Log(i+": init : waterMM      " + waterMM[i] + " = " + (float)waterMM[i].y);
            }


        }


        public void InitializeSpot()
        {
            //dette gjøres i initiateEarthTypes
            //waterMM.Add(new Vector2(0, 0.6f));
            //air.Add(new Vector2(0, 0.05f));
            //smallLife.Add(new Vector2(0, 0.08f));
            //humusQuality.Add(new Vector2(0, 0.2f));
            //nitrogen.Add(new Vector2(0, 0.02f));
            //organicMatter.Add(new Vector2(0, 0.25f));

            //Default is not planted. Plant Name is 'ubeplantet'
            plantName = "Ubeplantet";
            planted = false;
            plantet = planted;
            
            //setting the amount of nutrients and minerals.
/*

            valFe = 0.0004;
            valMn = 0.00005;
            valZn = 0.00002;
            valB = 0.000015M;
            valCu = 0.000006;
            valMo = 0.000001;

            valFe = 400E-006;  // Fe
            valMn = 50E-006;   // Mn
            valZn = 20E-006;  // Zn
            valB = 1E-006M;   // B
            valCu = 6E-006;    // Cu
            valMo = 1E-006;    // Mo
*/
           /// water variable is added under InitiateEarthType,
           /// water = 0.3; //tørt: 0-0.2, vått 0.3-0.6 f.x

            //Approximated weight procent of soil, based on various sources
            //The amount also are presented by kg/ha (depth of soil ?)
 /*           valC = 0.9; // 
            valO = 0.01; // 
            valH = 0.01;  // 
            valN = 0.22; // 
            valK = 0.01;  // 
            valCa = 0.01; // 
            valPh = 0.01;  // 
            valS = 0.01;  // 
            valMg = 0.01;  // 
*/
            //Dummy values from normal soil analysis
            //Macro nutitients greatly differs from soiltype to soiltype
            //and by the treatment of the eart. These values are dummy-values,
            //and will be updated by which soiltype is chosen/calculated
            double waterold = water;
            //water = Nyverdi(water,1);
            //Debug.Log("waterOld="+waterold+ " Water i spot ( "+fieldnr+" , " + v_index + " , " + h_index + ") initialiseres med..................." + water);
            //water_H2O = water;

/*
            nutr_C = valC;  // 
            nutr_O = valO;  // 
            nutr_H = valH;  // 
            nutr_N = valN;  // 
            nutr_K = valK;  // 
            nutr_Ca = valCa;  // 
            nutr_Ph = valPh;  // 
            nutr_S = valS;  // 
            nutr_Mg = valMg;  // 
 */       

            //Debug.Log("water_H2O: " + water_H2O);
/*
            nutr_N = Nyverdi(valN , 2);
            nutr_C = Nyverdi(valC, 2);
            nutr_K  = Nyverdi(valK , 3);
            nutr_Ca = Nyverdi(valCa , 3);
            nutr_Ph = Nyverdi(valPh , 2);  
            nutr_O = Nyverdi(valO , 2);
            nutr_H  = Nyverdi(valH , 2);
            nutr_S  = Nyverdi(valS , 1);
            nutr_Mg = Nyverdi(valMg , 1);
*/
        }



        public void WriteSpot(Text myText)
        {
            //String plantName;
            //plantName = "u-beplantet";
            uttekst = myText;
            double plantHeight=0;
            int day;
            String nytekst;
            vaer = Weather.ThisDay;


            day = vaer.WhichDay;
            if (planted)
            {
                plantHeight = planten.Height;
                //nytekst = String.Format("Day {10}   Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: {9}, {11} cm", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName, day, plantHeight);
                //nytekst = String.Format("Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: {9}, {10} cm", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName, plantHeight);
                nytekst = String.Format("Water {0}, N {1}, C {2}, Ph {3}, S {4}, Plant: {5}, {6} cm", water_H2O, nutr_N, nutr_C, nutr_Ph, nutr_S, plantName, plantHeight);

            }
            else
            {
                //nytekst = String.Format("Day {10}   Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: {9}", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName, day);
                //nytekst = String.Format("Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: ***", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName);
                nytekst = String.Format("Water {0}, N {1}, C {2}, Ph {3}, S {4},  Plant: ***", water_H2O, nutr_N, nutr_C, nutr_Ph, nutr_S);

            }



            //String nytekst = String.Format("Day {10}   Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: {9} {11}", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName, day, day);
            //String nytekst = String.Format("Day {10}   Water {0}, N {1}, C {2}, K {3}, Ca {4}, Ph {5}, X {6}, Y {7} Z {8}, Plant: {9} Height: {11}", water_H2O, nutr_N, nutr_C, nutr_K, nutr_Ca, nutr_Ph, nutr_S, nutr_Mg, min_Fe, plantName, day, plantHeight);

            //oldtekst = textOfDay+uttekst.text;

            uttekst.text += nytekst;
            


        }



        public void Update(int iterasjon)
        {
            //Debug.Log("      -->  spot, update-iterasjon"+iterasjon+":   " + v_index + "  " + h_index);
            double rainmm;

            //This is the same for every iteration. 4 iterations each day/period
            plantet = this.Planted;
            Weather vaer = Weather.ThisDay;
            soltimer = vaer.SunHours;
            fraDag = Initializer.DagenIdag;
            tempDay = Initializer.DagenIdag; //holde rede på index

            Debug.Log("FØRST i iterasjon. iterasjon "+iterasjon);

            printVectors();

            switch (iterasjon)
            {
                case 1:
                    ////Debug.Log("Environmental variables gathered. Calculate new values of Spot");



                    String todayIs = vaer.Name;  //debugging purpose
                    double absorbtion = 0;
                    double absorbed = 0;
                    rainmm = vaer.Nedboer;
                    double oldWatervalue = water_H2O;
                    double waterValueBeforeSun;

                    soltimer = vaer.SunHours;
                    //Debug.Log("soltimer " + soltimer);

                    //Debug.Log("---------------------------------------------------environment update");
                    //Debug.Log("Water before this environment update:    " + water_H2O);

                    String nytekst = String.Format("Environment. Simulation day {0}, with weather.:   mm Nedbør {1},  Soltimer {2},  Dagslystimer {3},  Gj.Sn temp {4}", vaer.WhichDay, vaer.Nedboer, vaer.SunHours, vaer.LightHours, vaer.MeanTemp);
                    if ((h_index == 0) && (v_index == 0)) Debug.Log(nytekst); //begrensning til bare en gang per Field



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
                        Ny = Nyverdi(0.6, 1); Debug.Log("regn > 70mm. vanntillegg: " + Ny);
                        water_H2O = Ny;//this is maximum
                    }
                    else if (rainmm > 50)
                    {
                        Ny = Nyverdi(0.5, 1); Debug.Log("regn > 50mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 40)
                    {
                        Ny = Nyverdi(0.3, 1); Debug.Log("regn > 40mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 30)
                    {
                        Ny = Nyverdi(0.20, 1); Debug.Log("regn > 30mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 20)
                    {
                        Ny = Nyverdi(0.15, 1); Debug.Log("regn > 20mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 10)
                    {
                        Ny = Nyverdi(0.1, 1); Debug.Log("regn > 10mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 5)
                    {
                        Ny = Nyverdi(0.05, 1); Debug.Log("regn > 5 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm > 2)
                    {
                        Ny = Nyverdi(0.02, 1); Debug.Log("regn > 2 mm. vanntillegg: " + Ny);
                        water_H2O = water_H2O + Ny;
                    }
                    else if (rainmm == 0)
                    {

                        //water_H2O = water_H2O;
                    }

                    if (water_H2O > 0.75) water_H2O = 0.75; //never more than 0.75, that's maximum soakable amount of water...

                    waterValueBeforeSun = water_H2O;


                    //Debug.Log("waterValueBeforeSun "+waterValueBeforeSun);
                    //Debug.Log("soltimer " + soltimer);

                    //Debug.Log("Water_H2O FØR:  " +oldWatervalue+    " ETTER regnvannberegning: "+water_H2O+ "  rainmm: "+rainmm) ;
                    double tempWater = water_H2O;







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



                    Debug.Log("...............................Spot (" + h_index + "," + v_index + ").Water_H2O først:  " + oldWatervalue + " ETTER rainwater: " + tempWater + ", ETTER soltimeuttørking(new walue): " + water_H2O + "  (rainmm: " + rainmm + ")");


                    Debug.Log("Water_H2O   " + (float)water_H2O);


                    ///¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤





                    break;
                case 2:
                    //Debug.Log("---------------------------------------------------earth update");
                    //Debug.Log("Iteration 2: Calculating new equilibrum between spots, according to env variables. Not implemented");
                    //Debug.Log("  Calculating humus factors. Especially water, oxygen, amount of small life and other");
                    //Debug.Log("waterlevel:" + water_H2O + " temperature: ?" + " acidity: ?"+"" );



                    //_luftverdi = (float)air[0];
                    //_smallLife = (float)smallLife[tempDay].y;
                    //_nitrogen = (float)nitrogen[tempDay].y;
                    //_air = (float)air[0].y;
                    //_humusQuality = (float)nitrogen[tempDay].y;
                    //_organicMatter = (float)organicMatter[tempDay].y;



                    //bool mulch er true hvis det er lagt på kompost-dekke
                    if(mulch)
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
                    if (planted)
                    {
                       // nitrogen fratrekk og tillegg vedrørende planten. gjøres under case 4 (iterasjon4)
                    }
                    bool earthTeaPut = false; //denne brukes for å flagge om det er vannet med jord-te. 
                    if (earthTeaPut)
                    { 
                        //calculate new values. e.g mengde jordte er '100' dette fordeles og tilføyes jorda etterhvert
                    }



                    Debug.Log("_____Spot oppdateres dag " + tempDay + " > (" + v_index + " , " + h_index + " )");
                    Debug.Log("air  tempday  _air=" + _air);

                    waterMM.Add(new Vector2(tempDay, (float)water_H2O)*100);
                    air.Add(new Vector2(tempDay, (float)_air)*100);
                    smallLife.Add(new Vector2(tempDay, (float)_smallLife)*100);
                    humusQuality.Add(new Vector2(tempDay, _humusQuality)*100);
                    nitrogen.Add(new Vector2(tempDay, (float)_nitrogen*100));
                    organicMatter.Add(new Vector2(tempDay, (float)_organicMatter)*100);

                    Debug.Log("air  tempday  _air=" + _air);



                    //Debug.Log("iterasjon. jordverdier. dag=" + tempDay + ", Vector2utskrifter");

                    Debug.Log(".....  waterMM="+ waterMM[tempDay] + "   waterMM["+tempDay+"].y= " + waterMM[tempDay].y);
                   Debug.Log(".....  air    " + air[tempDay].y);
                 /*    Debug.Log("smallLife    " + smallLife[tempDay].y);
                    Debug.Log("humusQuality    " + humusQuality[tempDay].y);
                    Debug.Log("nitrogen    " + nitrogen[tempDay].y);
                    Debug.Log("organicMatter    " + organicMatter[tempDay].y);
*/



                   
                 


                    break;
                case 3:     //planten
                    //Debug.Log("---------------------------------------------------plant update");
                    //Debug.Log("Iterationi 3: Calculating influence on each spot from Plants. Not implemented");
                    //Debug.Log("oppdaterer spot (x,y)  "+ v_index + " , " + h_index);
                    //Debug.Log("p l a n t e d   er   " + planted);
                    soltimer = vaer.SunHours;

                    // if (plantet == true) Debug.Log("ja, planted er true");
                    if (plantet == false) break;
                    //Debug.Log("platnet er true, regner ut spot og plante-influens");
                    ////Debug.Log("oppdaterer spot (x,y)  " + v_index + " , " + h_index);

                    planten.Oppdater(this);

                    if (planten.Dead)
                    {
                        String ut2 = String.Format("spot ({0},{1}) {2}. is  no a late {2}. It died by heat and lack of water.", h_index, v_index, planten.name_no);
                        
                        Debug.Log(ut2);
                        planten = null;

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


                    //if there is much sun, the plant needs more water. 
                    double waterNeedSun = 1 + (double)soltimer / 10;   //e.g 30cm and 4 hours: 3*4/1000=0.12
                    double waterNeedEveryDay = 1;


                    //Water need according to plant height
                    double waterNeedPlant = (plantHeight / 2) * (plantHeight / 2) / 1000;


                    //factor, waterNeedSun will be multiplied by the 'sun' factor. If much sun, the greater the factor. Influence the thirst of the Plant
                    double waterNeedPlantOld= waterNeedPlant;
                    waterNeedPlant *= waterNeedSun;//waterNeedSun is a factor decided by the dryness and sunhours



                    String ut0 = String.Format("spot ({4},{5}) {6}. Soltimer={0} gives waterNeedSun-factor={1}. PlantHeight={2} waterNeedPlant=PlantHeight**2/1000={3}", soltimer, waterNeedSun, planten.Height, waterNeedPlant,h_index,v_index,planten.name_no);
                    Debug.Log(ut0);

                    String ut1 = String.Format("waterNeed {0} * ", sunHours, waterNeedSun, planten.Height);
                  //  Debug.Log(ut0);


                    String ut = String.Format("spot ({4},{5}) {6}. New earth water Level:   OldWaterlevel( {0} ) - waterNeedPlant({1}) * waterNeedSun-factor({2}) = newwaterdrink {3}", water_H2O, waterNeedPlantOld, waterNeedSun, water_H2O - waterNeedPlant, h_index, v_index, planten.name_no);
                    Debug.Log(ut);

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



                    break;
                case 4:
                    //Debug.Log("Iteration 4: Calculating new equilibrum. Not implemented");
                    //problem How to put the plant into the simulation?
                    //Update plant from where - from spot??




                   // The last thing we do is to update the day
                    break;
                default:
                    break;




            }//switch

            //dette utføres etter hver iterasjon 
            //
            Debug.Log("\n");
        }//Update


        public void PourWater()
        {
            // This Spot is watered 
            // Dummy variable is watered until 0.4
            // More water is excessive

            if (water_H2O < 0.4) water_H2O = 0.4;
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
            //Debug.Log("Spot, Plant-funksjonen fieldnr/spotx/spoty  "+fieldNr + spotX + spotY);

            // enten lages planten og tilordnes herfra, eller så gjøres det før iterasjonen fra kontrolleren
            // lager en peker til objektet

            planted = true; //ikke så robust, må huske å forandre verdien hvis planten fjernes eller dør.

            planten = new Plant(plantName, fieldNr,spotX, spotY);

  ////        Plant[] plantane = flora2.planter;

  ///          Debug.Log("lager plante: " + plantane[6].name_no);

            //Debug.Log("etter at planten er opprettet. planted=" + planted);
            //alternative: når planten lages, så får den informasjon om hvor den er plantet.
            //
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


        public  List<Vector2> WaterMM
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




    }//class Spot
}
