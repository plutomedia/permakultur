using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;
//using System;
using System.Text;
using System;
using System.Collections.Generic;

public class trig_boxes : MonoBehaviour {

    public static  Spot choseSpot, valgtSpot;
    public static Spot[,] zpots;
    private static string spotstring;
    public static int h, v;
    //public Transform updateTxt;
    //public txt_DayAndSpot oppdaterSpotTxt; //OBSOLETE
    //public Text myTextS;


    public GameObject tekstfelt;
    public Text myText;
    //public Text myInput;

    void  Start()
    {
        myText = tekstfelt.GetComponent<Text>();
        choseSpot = WMG_X_Tutor.ChosenSpot;
        WMG_X_Tutor.chosenSpot = choseSpot;

        spotstring = "0,0";
        myText.text = spotstring;


        //myText.text="0,0";
        //myInput = String.Format("dummy {0}",myText);

    }

    public void OnMouseDown()
    {
        zpots = Field.Spots;
        Debug.Log("klikket på denne box: "+this.name);
        //Debug.Log("  " + zpots[0, 0].v_index + "  " + zpots[0, 0].h_index + "  ");
        String nytekst;

        switch (this.name)
        {
            case "spot00":
                choseSpot = zpots[0,0];
                break;
            case "spot01":
                choseSpot = zpots[0, 1];
                break;
            case "spot02":
                choseSpot = zpots[0, 2];
                break;
            case "spot03":
                choseSpot = zpots[0, 3];
                break;
            case "spot10":
                choseSpot = zpots[1, 0];
                break;
            case "spot11":
                choseSpot = zpots[1, 1];
                break;
            case "spot12":
                choseSpot = zpots[1, 2];
                break;
            case "spot13":
                choseSpot = zpots[1, 3];
                break;
            case "spot20":
                choseSpot = zpots[2, 0];
                break;
            case "spot21":
                choseSpot = zpots[2, 1];
                break;
            case "spot22":
                choseSpot = zpots[2, 2];
                break;
            case "spot23":
                choseSpot = zpots[2, 3];
                break;
            case "spot30":
                choseSpot = zpots[3, 0];
                break;
            case "spot31":
                choseSpot = zpots[3, 1];
                break;
            case "spot32":
                choseSpot = zpots[3, 2];
                break;
            case "spot33":
                choseSpot = zpots[3, 3];
                break;
        }

        //spotChosenTXT2.text = spotChosen2; //



        Debug.Log(">  "+choseSpot.v_index + "  " + choseSpot.h_index+"  ");
        WMG_X_Tutor.chosenSpot = choseSpot;
        Initializer.SpotValgt = Field.Spots[v,h];

       
        spotstring = System.String.Format("({0},{1})", choseSpot.v_index, choseSpot.h_index);

        myText.text = spotstring;

        Debug.Log(">>>>>>spotstring>>>>>>>>>  "+spotstring);

        v = choseSpot.v_index;
        h = choseSpot.h_index;

        nytekst = String.Format("({0},{1})", v, h);


        //Rosinen i pølsa. Bruker Initializer for å lagre valgt dag og spot
        Initializer.SpotValgt = Field.Spots[v, h];

    }



    public static string Spotstring
    {
        get
        {
            return spotstring;
        }
        set
        {
            spotstring = value;
        }
    }

    public static int Vertikal
    {
        get
        {
            return v;
        }
        set
        {
            v = value;
        }
    }

    public static int Horisontal
    {
        get
        {
            return h;
        }
        set
        {
            h = value;
        }
    }

    public  static Spot ValgtSpot
    {
        get
        {
            return valgtSpot;
        }
        set
        {
            valgtSpot = value;
        }
    }


}
