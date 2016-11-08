using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class InGameMenu : MonoBehaviour
    {
        //public enum Menustates {Save, Options, Wiki, MainSave, MainNoSave };
        //public Menustates thisstate;
        public bool GamePaused;
        public GameObject PauseMenu;


        void Start()
        {
            GamePaused = false;
        }


        void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                if (GamePaused == true)
                {
                    PauseMenu.SetActive(false);
                    Debug.Log("pausemeny fjernes");
                    GamePaused = false;
                }
                else
                if (GamePaused == false)
                {
                    PauseMenu.SetActive(true);
                    Debug.Log("pausemeny bringes frem");
                    GamePaused = true;
                }
            }
        }


        public void OnSave()
        {
            Debug.Log("You pressed Save");
            //thisstate = Menustates.Save;	
        }


        public void OnOptions()
        {
            Debug.Log("You pressed Options");
            //thisstate = Menustates.Options;
        }


        public void OnWiki()
        {
            Debug.Log("You pressed Wiki");
            //thisstate = Menustates.Wiki;
        }


        public void OnLoad()
        {
            Debug.Log("You pressed Quit to Main and Save");
            //thisstate = Menustates.MainSave;
            SceneManager.LoadScene("menutest004 mainOK");
        }


        public void OnQuit()
        {
            Debug.Log("You pressed Quit to Main without saving");
            //thisstate = Menustates.MainNoSave;
            SceneManager.LoadScene("menutest004 mainOK");
        }

    }
}