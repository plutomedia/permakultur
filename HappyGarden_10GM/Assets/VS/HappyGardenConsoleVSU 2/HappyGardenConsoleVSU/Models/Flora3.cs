using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Newtonsoft.Json;
using System.IO;

namespace HappyGardenConsoleVSU
{
    [Serializable]
    class Flora3 : MonoBehaviour
    {
        Flora2 flora2;
        public Plant plant;

        public Flora3()
        {
            Debug.Log("flora3 constructor ||||||||||||||||||||||||||||||||");
            //initializeFlora();
            flora2 = new Flora2();

            newPlante("reddik");
        }

        public void initializeFlora()
        {
            Plante hodeSalat = new Plante("hodeSalat");
            Plante spinat = new Plante("spinat");
            Plante brokkoli = new Plante("brokkoli");
            Plante rosenkaal = new Plante("rosenkaal");
            Plante pillerter = new Plante("pillerter");
            Plante lavHageBonne = new Plante("lavHageBonne");
            Plante loekHvit2 = new Plante("loekHvit2");
            Plante gressLoek = new Plante("gressLoek");
            Plante purre = new Plante("purre");
            Plante hvitLoek = new Plante("hvitLoek");
            Plante potet = new Plante("potet");
            Plante gulrot = new Plante("gulrot");
            Plante reddik = new Plante("reddik");
            Plante kaalrot = new Plante("kaalrot");
            Plante jordskokk = new Plante("jordskokk");
            Plante nepe = new Plante("nepe");
            Plante sukkermais = new Plante("sukkermais");
            Plante agurk = new Plante("agurk");
            Plante squash = new Plante("squashe");
        }


        public void newPlante(string plantetype)
        {
            Plant nyPlante = flora2["potet"];
            Debug.Log("NewPlante():     flora2['potet'].name_no:   " + flora2["potet"].name_no);


            string p2_json = JsonUtility.ToJson(nyPlante,true);
            Debug.Log("I Flora3.NewPlante: den serialiserte json:    '" + p2_json + "'");
            //nyPlante = flora2(plantetype);
        }





    }
}
