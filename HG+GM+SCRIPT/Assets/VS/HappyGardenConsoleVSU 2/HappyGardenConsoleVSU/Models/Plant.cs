using UnityEngine;
using System.Collections;

using System;

using Newtonsoft.Json;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;



namespace HappyGardenConsoleVSU
{

    [Serializable]
    public class Plant
    {
        public string name_eng;
        public string name_no;
        public string name_lat;

        public int fieldNr, spotX, spotY;  //koordinater og plassering
        public int dayPlanted; // relative to day 0. QUESTION: maybe fill out day 0 to dayPlanted in List<Vector2> plant
                               // Every Spot has a list List<Vector2> plant. Can be empty or not. First we don't implement these empty ones
                               // but creating new in Spot.Plant. To make it simple, we initialize these Vectors with 00 values 
                               //from day 0 to day 'dayPlanted'
        public double distance;
        public double maxHeight;
        public double maxDepth;

        public double growthSpeed;

        public double optimumPh;
        public bool bindsNitrogen;

        public Spot mySpot;   //skal peke på spot(f,x,y)

        private double height;
        int health;

        bool dead = false;


        /// 

        int aromatic; int timeGrow;       //Spiringstid. Default er 1 inntil videre. Tid før frøet spirer.
        int timePlant;      //Tid spiren trenger for å være klar til å plantes. Minimumstid. Varierer med årstid og temp.
        int timePlantMax;  //Tidsperiode planten må plantes etter spiring, før det blir for sent. Kanskje det ikke blir for sent
        int timeBlomst;    //Tid relativt til spiring. Normal blomstringstid. 
        int timeSeed;      //Time relative to sprouting. Normal time seeds are ready

        

        double consumeNitrogen;// Hvis det er lite nitrogen, blir planten lidende. Les om virkningene
        double consumeWater;//testverdier. ikke reelt. dessuten er vannopptak avhengig av størrelse
                            // andre variable consume eller produce

        double produceNitrogen;


        //OVERLOADED
        public Plant(int dChosen,string namn, int fnr, int sx, int sy)
        {
            dayPlanted = dChosen;
            name_no = namn;
            fieldNr = fnr;
            spotX = sx;
            spotY = sy;

            //Debug.Log("Plant lages, MED parametre");
            //Debug.Log("Plant lages, MED parametre. Plant Model> " + name_no);

            initializeMe( namn);

            //Relevant features
            // aromatic and repellent treats. 
            // shading effect. Gives shade to other plant and soil
            // this shade is controlled by growth, direction and other shades
            //Ground Cover
            // Give and take from the soil
            // this is controlled of growth spesific parameters like hight, depth of root, age
            // another factor: oversize or undersize of leaves according to watering and sun and shade


            /*
             * HUSK Å BRUKE FLORA2[] MED NAVN. DETTE MÅ KANSKJE GJØRES MANUELT
             * VED BRUK AV SWITCH. NÅ DET ER GJORT, VIL HVERT NYE PLANTEOBJEKT VÆRE LIK
             * ET AV OBJEKTENE I FLORA2[]
             * flora2 må derfor være oppdatert på forhånd med innleste data fra fil
             * pass på å rydd opp i planter plantene hentede planter ol i Flora
             * */
        }


        public Plant(string namn)
        {
            name_no = namn;
         
            //Debug.Log("Plant lages, uten parametre");
           //// Debug.Log("Plant Model> " + name_no);




            initializeMe(name_no);
        }

        public Plant()
        {
            name_no = "noname";
        }

        private void initializeMe(string namn)
        {
            //Debug.Log("initalisering av " + namn);

            name_eng = "Dummy-name";
            name_no = namn;
            name_lat = "Domio Nomino";
            dead = false;

            aromatic = 0; int timeGrow = 1;       //Spiringstid. Default er 1 inntil videre. Tid før frøet spirer.
            timePlant = 7;      //Tid spiren trenger for å være klar til å plantes. Minimumstid. Varierer med årstid og temp.
            timePlantMax = 21;  //Tidsperiode planten må plantes etter spiring, før det blir for sent. Kanskje det ikke blir for sent
            timeBlomst = 28;    //Tid relativt til spiring. Normal blomstringstid. 
            timeSeed = 35;      //Time relative to sprouting. Normal time seeds are ready       
            height = 0;
            consumeNitrogen = 0.01;// Hvis det er lite nitrogen, blir planten lidende. Les om virkningene
            consumeWater = 0.05;//testverdier. ikke reelt. dessuten er vannopptak avhengig av størrelse
            // andre variable consume eller produce
            produceNitrogen = 0.0;

            growthSpeed = 2;    //default average speed. It is modified whith the height. Close to max is slow, beginning is relatively slow.
            maxHeight = 50;     //THIS IS FOR DEBUGGING REASONS ONLY. SHOULD BE INITIALIZED FROM JSON OR THE LIKE

            health = 10;

            if (produceNitrogen > 0) bindsNitrogen = true;

            string  planteutskrift;

            planteutskrift = string.Format("Plante {0}: ConsumeN {1}, ProduceN {2}, ConsumeWater {3}, Aromatic {4} ", name_no, consumeNitrogen, produceNitrogen, consumeWater, aromatic);
           /// Debug.Log(planteutskrift);
            //mySpot();
        }

        public void Oppdater(Spot myspot)
        {
            //Debug.Log("plant update(). Nye verdier er plant.height: "+height);
            //aktuelle private variable hentes fra spot

            //mySpot = myspot;



            // i iterasjon 3 under spot, skal planten oppdatere's
            //Oppdateringen er at planten får noen nye verdier for vekst og utvikling
            //eventuelt noen 'flagg' som sier noe om underernæring eller annet

            //dummyverdiene tar ikke hensyn til realitetene.
            //første test under update er at i alle fall planten skal vokse
            //og suge ut ting og tang fra jorden - eventuelt gi tilbake

            // height += 2; //2 cm hver oppdatering


            Debug.Log("plant.update: plantehelse:" + health);

            if ((health < 3)&&(dead!=true))
            {
                Debug.Log("The plant dies soon. Give water and prey.");

                if (health < 1)
                {
                    Debug.Log("Planten døde.");
                    dead = true;
                } 

            }
        
           
            if (dead)
            {
                height = 5; //når planten dør, blir den fem centimeter. For å unngå forvirring...
            }
            else if (height < maxHeight)
            {

                if (height < maxHeight * 0.6)
                {
                    height += growthSpeed;
                }
                else
                {
                    double diff = maxHeight - height;
                    double relation = diff / maxHeight;
                    double newGrowthSpeed = growthSpeed * relation;
                    height += newGrowthSpeed;
                    //Debug.Log("newGrowthSpeed=" + newGrowthSpeed + "  Plant height="+height);
                }
            }
        }


        public double Height
        {
            get { return height; }
            set { height = value;  }
        }


        public int Health
        {
            get { return health; }
            set { health = value; }
        }


        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
    }
}
