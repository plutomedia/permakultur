using UnityEngine;
using UnityEngine.UI;
//using System.Threading.Tasks;



namespace HappyGardenConsoleVSU
{
    public class Game
    {
        public Farm farmen;
        public GameView gameView;
        public Flora flora;


        public Game(Text myText)
        {
           //Modellen gis adgang til sin parent:

            Debug.Log("--------------------- Game Model--------------------->");
            gameView = new GameView(this);
            farmen = new Farm( myText);
            flora = new Flora();
        }

        public void InitializeEarthType()
        {
            farmen.InitializeEarthType();
        }



        public void Update(int iterasj)
        {
            int iterasjon = iterasj;

            //Debug.Log("GameUpdate Iterasjon" + iterasjon);
            farmen.Update(iterasjon);
        }

        public void WriteEarthValues(Text myText)
        {

            farmen.WriteEarthValues(myText);
        }

        public void PourWater(int fieldn)
        {
            int fieldnr = fieldn;

            farmen.PourWater(fieldnr);
        }

        public void Plant(string namn, int fieldNr, int spotX, int spotY)
        {
            //Debug.Log("Planter.Game, Plant-funksjonen " + namn + "  " + fieldNr + "  " + spotX + "  " + spotY);

            
            farmen.Plant(namn, fieldNr, spotX, spotY);
        }

    }//class Game
}