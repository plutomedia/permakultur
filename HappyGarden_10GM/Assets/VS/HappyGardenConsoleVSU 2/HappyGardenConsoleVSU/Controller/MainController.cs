using UnityEngine;
using System.Collections;
using System;


namespace HappyGardenConsoleVSU
{
    public class MainController
    {
       // HappyGardenConsole consoleWindow;
       /// public GameFrame gameFrame;




        public  MainController()
        {
            // happyGardenConsole = new HappyGardenConsole();

           Debug.Log("----------------------------------------------MainController> ");
        }



        public void Oppdater()
        {
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>    MainController.Update !");
            // update on environments
           // gameFrame.Oppdater(1);
            //gameFrame.Update(2);
            //gameFrame.Update(3);
            //gameFrame.Update(4);

        }//Update




        //public HappyGardenConsole ConsoleWindow
        //{
        //    //this function makes tall1 public to the controller
        //    get { return consoleWindow; }
        //    set { consoleWindow = value; }
        //}

    }
}