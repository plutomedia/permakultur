using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{
    public class Farm 
    {
        public  Field teig;
        public static Field muldTeig;
        public static List<Field> fields;
        Text myTekst;



        public Farm(Text myText)
        {
            myTekst = myText;
            //Debug.Log("("---------------------Farm Model>");

            fields = new List<Field>();
            myTekst.text = "Happy Garden System Simulator";

            /*  for (int i = 0; i < 1; i++)
              {
                  teig = new Field(i,  myTekst);
                  if (i==0) { muldTeig = teig; };
                  fields.Add(teig);
              }  */

            //teig = new Field(0, myTekst);
            //fields.Add(teig);


            muldTeig = new Field(0, myTekst);
        }


        public void InitializeEarthType()
        {
            //foreach (Field tg in fields)
            //{
            //    tg.InitializeEarthType();
            //}

            muldTeig.InitializeEarthType();
        }


        public void Update(int iterasj)
        {
            int iterasjon = iterasj;
            //Debug.Log("Farm.Update Iterasjon" + iterasjon);

            //foreach (Field tg in fields)
            //{
            //    tg.Update(iterasjon);
            //}

            //fields[0].Update(iterasjon);

            muldTeig.Update(iterasjon);
        }


        public void WriteEarthValues(Text myText)
        {
            //foreach (Field tg in fields)
            //{
            //    Debug.Log("xxxxxxxxxxxxxxxx foreach field tg in fields" + tg.Fieldnr);
            //    tg.WriteEarthValues(myText);
            //}

            muldTeig.WriteEarthValues(myText);

        }


        public void PourWater(int fieldn)
        {
            int fieldnr = fieldn;
            //Debug.Log("Planter. Farm.PourWater-funksjonen fieldnr:  "  + fieldn );
            //fields[fieldnr-1].PourWater();

            muldTeig.PourWater();
        }


        public void Plant(string namn, int fieldn, int spotX, int spotY)
        {
            int fieldNr = fieldn;
            //Debug.Log("Planter. Farm.Plant-funksjonen " + namn + "  " + fieldNr + "  " + spotX + "  " + spotY);


            //fields[fieldNr].Plant(namn, fieldNr, spotX, spotY);

            muldTeig.Plant(namn, fieldNr, spotX, spotY);
        }



        // denne skal sende peker til parent-felt
        public static List<Field> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }

        public static Field MuldTeig
        {
            get
            {
                return muldTeig;
            }
            set
            {
                muldTeig = value;
            }
        }

    }//class Farm
}
