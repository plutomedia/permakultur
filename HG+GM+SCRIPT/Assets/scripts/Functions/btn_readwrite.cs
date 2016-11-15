using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class btn_readwrite : MonoBehaviour
    {
        public GameObject tekstfelt;    //textDisplay
        public GameObject inputfelt;

        public Text myText;
        public Text myBtnText;
        public Text myInput;

        // Use this for initialization
        void Start()
        {
            //leser tekstfelt
            myText = tekstfelt.GetComponent<Text>();
            //Debug.Log(myText.text);

            //skifter skrift på tekstfeltet
            myText.text = "Denne tekst oppdateres til det du skriver";
            //Debug.Log("ny tekst="+myText.text);

            //skifter skrift på Button til Pressed
            //myBtnText = this.transform.GetComponent<Text>();
            //Debug.Log(myBtnText);

            myInput = inputfelt.GetComponent<Text>();
            //Debug.Log(myInput.text) ;

        }

        // Update is called once per frame
        void Update()
        {
            myInput = inputfelt.GetComponent<Text>();
            if (myInput.text != "")
            {
                //Debug.Log("inputfeltet: "+myInput.text);
                myText.text = myInput.text;
            }
        }
    }
}