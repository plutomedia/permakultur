using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{
    public class Farm 
    {
        public Field teig;
        public List<Field> fields;

        Text myTekst;

        //public GameObject teig_;

        public Farm(Text myText)
        {
            myTekst = myText;
            //Debug.Log("("---------------------Farm Model>");

            fields = new List<Field>();

            myTekst.text = "Happy Garden System Simulator";
     
            for (int i = 0; i < 3; i++)
            {
                teig = new Field(i,  myTekst);
                fields.Add(teig);
            }
        }





        public void Update(int iterasj)
        {
            int iterasjon = iterasj;
            //Debug.Log("Farm.Update Iterasjon" + iterasjon);

            foreach (Field tg in fields)
            {
                tg.Update(iterasjon);
            }
        }

        public void PourWater(int fieldn)
        {
            int fieldnr = fieldn;
            Debug.Log("Planter. Farm.PourWater-funksjonen fieldnr:  "  + fieldn );
            fields[fieldnr-1].PourWater();
        }

        public void Plant(string namn, int fieldn, int spotX, int spotY)
        {
            int fieldNr = fieldn;
            Debug.Log("Planter. Farm.Plant-funksjonen " + namn + "  " + fieldNr + "  " + spotX + "  " + spotY);


            fields[fieldNr].Plant(namn, fieldNr, spotX, spotY);
        }
    }//class Farm
}
