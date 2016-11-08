using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

using System.Text;
using UnityEngine.UI;
//using System.Threading.Tasks;



namespace HappyGardenConsoleVSU
{
    public class Field
    {
        public Spot jordbit;
        public int Fieldnr;

        public Spot[,] spots;

        public Text fieldText;
        
       
        /*from http://stackoverflow.com/questions/18865574/multidimensional-array-of-object-c-sharp#18865697   
        */

        public Field(int nr, Text myText)
        {
            fieldText = myText;
            Fieldnr = nr;
            //Debug.Log("Field>" + nr);
            spots = new Spot[10, 10]; //et 'Field' er på 10x10 punkter (egentlig ca 20x200)


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    spots[i, j] = new Spot(nr,i,j,myText);

                    ///Debug.Log("jordbit " + i + "  " + j);
                }
            }

        }



        public void Update(int iterasj)
        {
            int iterasjon = iterasj;
            //MessageBox.Show("Spot Update");
            //Debug.Log("----Field "+ Fieldnr + " Update, iterasjon "+iterasjon+"--------------------");

            for (int i = 0; i < 3; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                    
                    //Debug.Log("oppdaterer spot (f,x,y)  "+Fieldnr+" , " + i + " , " + j);

                    jordbit = spots[i, j];
                    jordbit.Update(iterasjon);
                }
            }
        }



        public void PourWater()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    spots[i, j].PourWater();

                    ///Debug.Log("jordbit " + i + "  " + j);
                }
            }
        }



        public void Plant(string namn, int fieldNr, int spotX, int spotY)
        {
            //Debug.Log("Plant. Field. Plant funksjonen");
            spots[spotX, spotY].Plant(namn, fieldNr, spotX, spotY);

            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        spots[i, j].Plant(namn, fieldNr, spotX, spotY);

            //       Debug.Log("jordbit " + i + "  " + j);
            //    }
            //}

        }

    }//class Field
}
