using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;
using System;

public class txt_DayAndSpot : MonoBehaviour {

    public String spotChosen;
    //public Text dayChosen;
    //public Spot chosenSpot= WMG_X_Tutor.chosenSpot;
   // public Text spotChosenTXT;
    private Spot previousSpot, thisSpot;

    public GameObject tekstfeltet;


	void Start ()
    {
        spotChosen = "I,J";
        Debug.Log("tekstfeltet" + tekstfeltet.GetComponent<GUIText>());
        //GetComponent<GUIText>();

        //Debug.Log("spotChosenTXT:  " + spotChosenTXT);
        //spotChosenTXT.text = spotChosen;
        //dayChosen.text = "0";
	}
/*
    void Update()
    {
        previousSpot = thisSpot;
        thisSpot = WMG_X_Tutor.ChosenSpot;

        //Text temp = tekstfeltet.GetComponent<Text>();
        //Debug.Log("tekstfeltet  " + temp);
        //spotChosen = "@@@";

        if (previousSpot != thisSpot)
        {
            string spottxt = trig_boxes.Spotstring;
            int v = trig_boxes.Vertikal;
            int h = trig_boxes.Horisontal;
            Debug.Log("ny verdi. Spot er valgt, og tall oppdateres");
            Debug.Log("oppdater " + v + "," + h);
           // Debug.Log("spotChosenTXT:  " + spotChosenTXT);
            Debug.Log("spottxt:  " + spottxt);

            spotChosen = spottxt;
            //spotChosen = "##";
            //tekstfeltet.guiText = "";
        }
    }
*/
}
