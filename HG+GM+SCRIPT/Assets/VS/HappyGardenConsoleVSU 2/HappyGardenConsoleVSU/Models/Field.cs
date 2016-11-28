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
        public static Spot[,] spots;
        public Text fieldText;
        public static int rows, columns;

        //public List<Vector2> jordflekker;
        Weather vaer;
        public Spot[,] spotts;
        public Text myText;
        //public Text whatText;
        public String whatText;
        //private List<Field> flds;



        public Field(int nr, Text myText)
        {
            rows = 4;
            columns = 4;

            fieldText = myText;
            Fieldnr = nr;
            //Debug.Log("Field>" + nr);
            spots = new Spot[rows, columns]; //et 'Field' er på 10x10 punkter (egentlig ca 20x200)


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    spots[i, j] = new Spot(nr,i,j,myText);
                    if ((i == 0) && (j == 0))
                    {
                        spots[i, j].Marked = true;
                    }
                }
            }

            //default valgt verdi som kan oppdateres til wievet
            Graf.ChosenSpot = spots[0, 0];

        }


        //public void InitializeEarthType()
        //{

        //    //jordbit = spots[0, 0];
        //    //jordbit.Marked = true;

        //    for (int i = 0; i < rows; i++)
        //    {

        //        for (int j = 0; j < columns; j++)
        //        {

        //            Debug.Log("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQqinitierer spot (f,x,y)  " + Fieldnr + " , " + i + " , " + j);

        //            jordbit = spots[i, j];
        //            jordbit.InitializeEarthType();

        //        }
        //    }

        //    //spots[0, 0].Marked = true; //default avmerket felt, som alltid skal simuleres
        //}


        public void Update(int iterasj)
        {
            int iterasjon = iterasj;

            //dagenIdag = Initializer.dagenIdag;

            Debug.Log("############  Field "+ Fieldnr + " Update, iterasjon "+iterasjon+
                "  ###########################################################");


            for (int i = 0; i < rows; i++)
            {
                
                for (int j = 0; j < columns; j++)
                {

                    jordbit = spots[i, j];

                    if (jordbit.Marked)
                    {
                        Debug.Log("oppdaterer spot (f,x,y)  "+Fieldnr+" , " + i + " , " + j);
                        jordbit.Update(iterasjon);
                    } //alle som er trykket på
                }//for j
            }//for i
      
        }



        public void PourWater()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    spots[i, j].PourWater();

                    ///Debug.Log("jordbit " + i + "  " + j);
                }
            }
        }



        public void Plant(string namn, int fieldNr, int spotX, int spotY)
        {
            //Debug.Log("Plant. Field. Plant funksjonen");

            //spots[spotX,spotY] er aktuell 'Spot'. Kaller på dennes Plant-funksjon

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

        public void WriteEarthValues(Text myText)
        {

            //flds = Farm.Fields;
            String nytekst; //hmm hva er forskjellen på String og string.
            String tempstring;
            
            myText.text = "";
            
            //whatText.text = "";

            vaer = Weather.ThisDay;

            tempstring = String.Format("HappyGarden, Day {0}:   ", vaer.WhichDay);

            whatText = tempstring;

            //nytekst = String.Format("\nTeig{2}, spot({0},{1}):  ", ii, jj, i.Fieldnr);

            
            //this version we have only one field, with one earth type
            for (int fnr=0; fnr<1;fnr++)
            {

                string earthType = "Our garden bed";// default, to prevent error messages. Should be replaced with exception in switch.


                myText.text = myText.text + "\nTeig " + Fieldnr + "  -  " + earthType;

                for (int ii = 0; ii < Field.Rows; ii++)
                {
                    for (int jj = 0; jj < Field.columns; jj++)
                    {
                        //spotts[ii, jj];
                        // Debug.Log("--->" + ii + " , " + jj + " :" + spotts[ii, jj].nutr_C\n);
                        nytekst = String.Format("\nSpot({0},{1}):  ", ii, jj);
                        myText.text += 
                            nytekst;

                        //Debug.Log("       --->    " + ii + " , " + jj + " :");
                        spots[ii, jj].WriteSpot(myText);
                    }
                }

                //myText.text += " \n";
            }

        }



        public static int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
            }
        }

        public static int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        public static Spot[,] Spots
        {
            get
            {
                return spots;
            }
            set
            {
                spots = value;
            }
        }


    }//class Field
}
