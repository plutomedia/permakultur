using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System;
using System.Collections.Generic;

using Newtonsoft.Json; //DENNE IMPORTERTE JEG MANUELT OG LA INN I PLUGINS. BASERT PÅ .NET 4.5
using System.IO;


//using System.Threading.Tasks;



namespace HappyGardenConsoleVSU
{
    public class Flora : MonoBehaviour, IEnumerable
    {
        public Plant plant;
        public List<Plant> planter, planterHentet, planterHentet2;
        
        //??GameObject  planter;
        public GameObject planten;


        public Flora()
        {
            Debug.Log("---------------------Flora Model---------------->");

            planter = new List<Plant>();
            planterHentet = new List<Plant>();


            ///_______________________________________________________________
            ///lager planteobjekt
            ///
            /*
                        Plant dummyplante = new Plant("UmuligPlante");
                        Plant hentetplante;

                        string p_json = JsonUtility.ToJson(dummyplante,true);
                        Debug.Log("I Flora Constructor,dummyplante. Den serialiserte json:    '" + p_json + "'");

                        using (System.IO.StreamWriter File2 = new System.IO.StreamWriter(@"D:\Unity\HappyGardenUnityVS\HappyGarden_6\Assets\VS\HappyGardenConsoleVSU 2\HappyGardenConsoleVSU\HGData\floratest.txt"))
                        File2.WriteLine(p_json);


                         //////////////////////////////// å konvertere tilbake 
                        // jsontest = JsonUtility.FromJson<JsonTest>(json);
                        //Debug.Log("jsontest:      " + jsontest.playerName);

                        hentetplante = JsonUtility.FromJson<Plant>(p_json);
                        Debug.Log("hentetplante:      " + hentetplante.name_no);
            */



            /*
                        //Eventually Load the list from file, but it some times have to be created
                        //this way e.g:  planter.Add(new Plant("fantasiPlante1"));
                        CreatePlantList();

                        /// Denne fungerer lagrer listeobjektet i json-streng, lagrer strengen til fil
                        string plantlist = JsonConvert.SerializeObject(planter, Formatting.Indented);
                        Debug.Log("plantelisten: > " + plantlist + " <");

                        using (System.IO.StreamWriter File2 = new System.IO.StreamWriter(@"D:\Unity\HappyGardenUnityVS\HappyGarden_6\Assets\VS\HappyGardenConsoleVSU 2\HappyGardenConsoleVSU\HGData\plantlist.txt"))
                        File2.WriteLine(plantlist);

                        Debug.Log("ReadFromJSON: leser objektet fra JSON");    

                        for (int i=0; i<planterHentet.Count; i++)
                        {
                            Debug.Log("Hentet fra fil: " + planterHentet[i].name_no);
                        }
            */



            /*
                        Debug.Log("---------lager flora2--------------------------------------------------------------------" );
                        //Lager ny alternativ flora, flora2. Hensikt å finne plantene ved plantenavn
                        //laget i flora3 slik: public Plant[] planter = { new Plant("fantasiPlante1"),...

                        Flora2 flora2 = new Flora2(); //lager en liste over Planteobjekter.
                        Plant[] plantene = flora2.planter;

                        //serialiserer den nye flora2-listen
                        string plantlist2 = JsonConvert.SerializeObject(plantene, Formatting.Indented);
                        Debug.Log("plantelisten2: > " + plantlist2 + " <");

                        //Debug.Log("flora2['potet'].name_no:   "+               flora2["potet"].name_no);

                        using (System.IO.StreamWriter File2 = new System.IO.StreamWriter(@"D:\Unity\HappyGardenUnityVS\HappyGarden_6\Assets\VS\HappyGardenConsoleVSU 2\HappyGardenConsoleVSU\HGData\plantlist2.txt"))
                            File2.WriteLine(plantlist2);
            */




            //  LESE FRA FIL

            string readplantlist ="";


            using (StreamReader sr = new StreamReader(@"D:\Unity\HappyGardenUnityVS\HappyGarden_7\Assets\VS\HappyGardenConsoleVSU 2\HappyGardenConsoleVSU\HGData\plantlist2.txt"))
            {
                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    //Debug.Log("> "+line);
                    readplantlist += line;
                }
            }


            //Her gjøres string 'readplantlist' om til listeobjektet 'planterHentet2'.    List<Plant>()
            planterHentet2 = JsonConvert.DeserializeObject<List<Plant>>(readplantlist);

     
            Flora2 flora2 = new Flora2(); //lager en liste over Planteobjekter.
         
            Plant[] plantene = flora2.planter; //dette funker. henter verdier fra lokal tilordning

            //Debug.Log("etter  Plant[] plantene = flora2.planter; før utskrift av flora2['potet']");

            //Debug.Log("flora2['potet'] = " + flora2["potet"].name_no); //denne gav feilmelding fordi verdiene var feil f.x '.potet' i stedet for 'potet'
           


            //Debug.Log("Plant[] plantene = flora2.planter; Nå blir plantene initiert? plantene[8].name_no = " + plantene[8].name_no);
            //kan vi gå andre veien flora2.planter = planterHentet2

            //Flora2 flora2 = new Flora2(); //lager en liste over Planteobjekter.
 //flora2.Plant = planterHentet2; 
 //Plant[] plantene = flora2.planter;






            for (int i=0; i< planterHentet2.Count; i++)
            {
                //Debug.Log("Lokal-tilordning: " +plantene[i]+ ",  "+ plantene[i].name_no);
                //Debug.Log("Hentet fra fil: " + planterHentet2[i]+",  "+ planterHentet2[i].name_no);

                //HER legger vi inn verdiene fra fil til et tilgjengelig planteobjekt (Flora2[])
                plantene[i] = planterHentet2[i];
                //flora2


            }


            ///Debug.Log("flora2['potet'] = " + flora2["potet"].name_no);

            //foreach (string pnavn in flora2[])
            //    {

            //    }

            for (int i = 0; i < planterHentet2.Count; i++)
            {
                Debug.Log("Plante"+i+": " + plantene[i].name_no);

                //Plant testplante = Flora2.Plant["kgh"];



            }

        }





        public void CreatePlant(string noname)
        {
            //Plant planteData = planter["xx"];

        }

        public void CreatePlantList()
        { 
            //Lager dummyplanter, en liste på vanlige plantenavn
            //Disse kan lages også i Liste

            /*  //Grønnsaker,salat
              Plant hodeSalat = new Plant("hodeSalat  "); 
              Plant spinat = new Plant("spinat  ");
              //kål
              Plant brokkoli = new Plant("brokkoli  ");
              Plant rosenkaal = new Plant("rosenkaal  ");
              //erter og bønner
              Plant pillErter = new Plant("pillErter  ");
              Plant lavHagebønne = new Plant("lavHagebønne  ");
              //Løk og purre
              Plant loekHvit = new Plant("loekHvit  ");
              Plant gressLoek = new Plant("gressLoek  ");
              Plant purre = new Plant("purre  ");
              Plant hvitLoek = new Plant("hvitLoek  ");

              //Rotfrukter og knoller
              Plant potetX = new Plant("potetX  ");
              Plant gulrotX = new Plant("gulrotX  ");
              Plant reddik = new Plant("reddik  ");
              Plant kaalrot = new Plant("kaalrot  ");
              Plant jordskokk = new Plant("jordskokk  ");
              Plant nepe = new Plant("nepe  ");

              //sukkermais, agurk og squash
              Plant sukkermais = new Plant("sukkermais  ");
              Plant agurk = new Plant("agurk  ");
              Plant squash = new Plant("squash  ");
              */

            //mal for å lage nye Planter:
            // planter.Add(new Plant("fantasiPlante1"));
            //


            //Lager et array også (for å prøve hvordan dette fungerer også
            planter.Add(new Plant("fantasiPlante1"));
            planter.Add(new Plant("fantasiPlante2"));
            planter.Add(new Plant("fantasiPlante3"));

            planter.Add(new Plant("hodeSalat  "));
            planter.Add(new Plant("spinat  "));
            planter.Add(new Plant("brokkoli  "));
            planter.Add(new Plant("rosenkaal  "));
            planter.Add(new Plant("pillErter  "));
            planter.Add(new Plant("lavHagebønne  "));
            planter.Add(new Plant("loekHvit2  "));
            planter.Add(new Plant("gressLoek  "));
            planter.Add(new Plant("purre  "));
            planter.Add(new Plant("hvitLoek  "));

            planter.Add(new Plant("potet  "));
            planter.Add(new Plant("gulrot  "));
            planter.Add(new Plant("reddik  "));
            planter.Add(new Plant("kaalrot  "));
            planter.Add(new Plant("jordskokk  "));
            planter.Add(new Plant("nepe  "));
            planter.Add(new Plant("sukkermais  "));
            planter.Add(new Plant("agurk  "));
            planter.Add(new Plant("squash  "));

            Debug.Log("plant flora creation completed. -------------------------------------");

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

