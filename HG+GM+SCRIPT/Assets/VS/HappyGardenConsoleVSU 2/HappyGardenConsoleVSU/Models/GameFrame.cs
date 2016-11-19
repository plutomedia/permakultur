using UnityEngine;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{
    public class GameFrame 
    {
        public Game game;
       //// public GameFrameView gameFrameView;
        //public HappyGardenConsole happyGardenConsole;



        public GameFrame(Text myText)
        {

            Debug.Log("----------------------------GameFrame Model>");

            // gameFrameView = new GameFrameView(this);

            //oppretter et console-view objekt
            //happyGardenConsole = new HappyGardenConsole(this);
            
            game = new Game(myText);


        }// public GameFrame Constructor

        public void InitializeEarthType()
        {
            game.InitializeEarthType();
        }

        public void Oppdater(int iterasj)
        {
            int iterasjon = iterasj;

            //Debug.Log("GameFrame.Update Iterasjon"+iterasjon);

            game.Update(iterasjon);
        }



        public void WriteEarthValues(Text myText)
        {

            game.WriteEarthValues(myText);
        }

        public void PourWater(int fieldn)
        {
            int fieldnr = fieldn;

            game.PourWater(fieldnr);

        }

        public void Plant(string namn, int fieldNr, int spotX, int spotY)
        {
            //Debug.Log("Planter.GameFrame, Plant-funksjonen "+namn+"  "+fieldNr+"  "+spotX+"  "+spotY);

            game.Plant(namn, fieldNr, spotX, spotY);
        }
    }
}