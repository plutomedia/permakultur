using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*public GameObject mainMenu;
public GameObject optionsMenu;*/



namespace HappyGardenConsoleVSU
{
    public class MenuScript : MonoBehaviour
    {

        //menu states
        public enum Menustates { Main, StartGame, Options, Wiki, Load, ExitOS };
        public Menustates currentstate;

        public GameObject mainMenu;
        public GameObject startNewMenu;
        public GameObject optionsMenu;
        public GameObject wikiMenu;
        public GameObject loadMenu;


        void Awake()
        {
            //Always sets first menu to main menu
            currentstate = Menustates.Main;
        }


        void Update()
        {

            //Checks current menu state
            switch (currentstate)
            {
                case Menustates.Main:
                    // sets active gameobject for main menu

                    mainMenu.SetActive(true);
                    startNewMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    wikiMenu.SetActive(false);
                    loadMenu.SetActive(false);

                    break;
                case Menustates.StartGame:
                    // sets active gameobject for StartGame menu
                    mainMenu.SetActive(false);
                    startNewMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    wikiMenu.SetActive(false);
                    loadMenu.SetActive(false);

                    break;
                case Menustates.Options:
                    // sets active gameobject for Options menu
                    mainMenu.SetActive(false);
                    startNewMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    wikiMenu.SetActive(false);
                    loadMenu.SetActive(false);


                    break;
                case Menustates.Wiki:
                    // sets active gameobject for Wiki menu
                    mainMenu.SetActive(false);
                    startNewMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    wikiMenu.SetActive(true);
                    loadMenu.SetActive(false);


                    break;
                case Menustates.Load:
                    // sets active gameobject for Load menu
                    mainMenu.SetActive(false);
                    startNewMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    wikiMenu.SetActive(false);
                    loadMenu.SetActive(true);

                    break;

            }

        }

        public void OnStartGame()
        {
            Debug.Log("You pressed StartNewGame");
            if (currentstate == Menustates.StartGame)
            {
                SceneManager.LoadScene("Farmscene028");
            }
            currentstate = Menustates.StartGame;

        }

        public void OnMain()
        {
            Debug.Log("You pressed back to Main");
            currentstate = Menustates.Main;
        }


        public void OnOptions()
        {
            Debug.Log("You pressed Options");
            currentstate = Menustates.Options;
        }


        public void OnWiki()
        {
            Debug.Log("You pressed Wiki");
            currentstate = Menustates.Wiki;
        }


        public void OnLoad()
        {
            Debug.Log("You pressed Load Previous Game");
            currentstate = Menustates.Load;
        }


        public void OnQuit()
        {
            Debug.Log("You pressed Exit to OS");
            currentstate = Menustates.ExitOS;
            SceneManager.LoadScene("menutest004 mainOK");
        }

    }
}