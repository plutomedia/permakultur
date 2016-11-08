using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


//using System.Web.Script.Serialisation;





namespace HappyGardenConsoleVSU
{

    [Serializable]
    public class Flora2 	//DayCollection
    {

            public Plant[] planter = {

                new Plant("hodeeeeeeSalat"),
                new Plant("spinat"),
                new Plant("brokkoli"),
                new Plant("rosenkaal"),
                new Plant("pillErter"),
                new Plant("lavHagebønne"),
                new Plant("loekHvit"),
                new Plant("gressLoek"),
                new Plant("purrre"),
                new Plant("hvitLoek"),
                new Plant("potet"),
                new Plant("gulrot"),
                new Plant("reddik"),
                new Plant("kaalrot"),
                new Plant("jordskokk"),
                new Plant("nepe"),
                new Plant("sukkermais"),
                new Plant("agurk"),
                new Plant("squash")
            };

            public Flora2()
        {// utfordring vi har to separate flora
            /// denne som lages her, og den som leses inn fra json
            /// i 'Flora' leser vi inn og tilordner plantene[i]

        }





        // This method finds the day or returns -1
        private Plant GetPlant(string plantenavn)
        {
            Debug.Log("GetPlant, navn er " + plantenavn);

            for (int j = 0; j < planter.Length; j++)
            {
                Debug.Log(planter[j].name_no);

                if (planter[j].name_no == plantenavn)
                {
                    return (planter[j]);
                }
            }

            throw new System.ArgumentOutOfRangeException(plantenavn, "testPlante must be in the form \"Sun\", \"Mon\", etc");
        }

        // The get accessor returns an integer for a given string
        public Plant this[string tstDay]
        {
            get
            {
                return (GetPlant(tstDay));
            }
        }
    }
}
