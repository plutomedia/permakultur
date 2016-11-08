using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;




namespace HappyGardenConsoleVSU
{
    public class ExitScreen : MonoBehaviour
    {


        void Update()
        {
            if (Input.GetKey("escape"))
            {
                SceneManager.LoadScene("menutest004 mainOK");
            }
        }

        void OnMouseDown()
        {
            Debug.Log("musen klikket");
            Application.Quit();
        }
    }
}