using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class containerValues : MonoBehaviour {

    private static List<Vector2> minVektorListe = new List<Vector2>();
    private static List<Vector2> minVektorListe2 = new List<Vector2>();
    private static List<Vector2> minVektorListe3 = new List<Vector2>();
    private static List<Vector2> minVektorListe4 = new List<Vector2>();

    System.Random rng = new System.Random();


    void Start () {
        Debug.Log(">>>>>>>>>>> CONTAINERVALUES START");
  /*      //random(0.7f, 1f);
        System.Random rng = new System.Random();

        minVektorListe.Add(new Vector2(0, 1));
        minVektorListe.Add(new Vector2(1, 2));
        minVektorListe.Add(new Vector2(2, 4));
        minVektorListe.Add(new Vector2(3, 8));
        minVektorListe.Add(new Vector2(4, 16));
        minVektorListe.Add(new Vector2(5, 32));
        minVektorListe.Add(new Vector2(6, 1));
        minVektorListe.Add(new Vector2(7, 2));
        minVektorListe.Add(new Vector2(8, 4));
        minVektorListe.Add(new Vector2(9, 8));
        minVektorListe.Add(new Vector2(10, 16));
        minVektorListe.Add(new Vector2(11, 32));


        minVektorListe2.Add(new Vector2(0, 3));
        minVektorListe2.Add(new Vector2(1, 4));
        minVektorListe2.Add(new Vector2(2, 6));
        minVektorListe2.Add(new Vector2(3, 7));
        minVektorListe2.Add(new Vector2(4, 6));
        minVektorListe2.Add(new Vector2(5, 3));
        minVektorListe2.Add(new Vector2(6, 5));
        minVektorListe2.Add(new Vector2(7, 5));
        minVektorListe2.Add(new Vector2(8, 4));
        minVektorListe2.Add(new Vector2(9, 4));
        minVektorListe2.Add(new Vector2(10, 5));
        minVektorListe2.Add(new Vector2(11, 6));

        for (int i=0; i<5; i++)
        {
            //Debug.Log("minVektorListe[i]" + minVektorListe[i]);
        }



        for (int i =0; i < 28;i++)
        {
            float tilftall = tilfTall(0.5f, 2f);
            ///Debug.Log("tilftall=" + tilftall);

            minVektorListe3.Add(new Vector2(i, tilftall*100));
        }

        for (int i = 0; i < 28; i++)
        {
            float tilftall = tilfTall(0.5f, 2f);
            ///Debug.Log("tilftall=" + tilftall);

            minVektorListe4.Add(new Vector2(i, tilftall * 60));
        }
*/

    }


    public static List<Vector2> MinVektorListe
    {
        get
        {
            return minVektorListe;          
        }
        set
        {
            minVektorListe = value;
        }
    }

    public static List<Vector2> MinVektorListe2
    {
        get
        {
            return minVektorListe2;
        }
        set
        {
            minVektorListe2 = value;
        }
    }

    public static List<Vector2> MinVektorListe3
    {
        get
        {
            return minVektorListe3;
        }
        set
        {
            minVektorListe3 = value;
        }
    }

    public static List<Vector2> MinVektorListe4
    {
        get
        {
            return minVektorListe4;
        }
        set
        {
            minVektorListe4 = value;
        }
    }


    /*
    ///-------------------------------------------random funksjoner
    ///
    public float random(float midtp, float avvik)
    {
        float Pi = 3.14159265f;
        System.Random rng = new System.Random();
        float xsum = 0;
        float ysum = 0;
        int ant = 1000;
        float minx, miny, maxx, maxy;
        float midtpunkt = 0.5f;
        float Avvik = 0.01f;

        midtpunkt = midtp;
        Avvik = avvik / 100f;

        minx = midtpunkt; miny = midtpunkt;//skal ende opp sim min-verdier for x hhv y
        maxx = midtpunkt; maxy = midtpunkt;

        for (int i = 0; i < ant; i++)
        {
            float temp = Mathf.Abs(-2 * Mathf.Log(rng.Next()));
            float r = Mathf.Sqrt(temp);
            float th = 2 * Pi * rng.Next();
            float x = r * Mathf.Cos(th);
            float y = r * Mathf.Sin(th);

            //midtpunkt og avvik. tweake til vårt scope
            x *= Avvik;
            x += midtpunkt;

            //beregner gjennomsnitt og max-avvik
            xsum = xsum + x;
            ysum = ysum + y;

            if (x < minx) minx = x;
            if (y < miny) miny = y;

            if (x > maxx) maxx = x;
            if (y > maxy) maxy = y;

            string tempstr = String.Format("x={0}, y={1}  ", x, y);
            //Debug.Log(tempstr);
        }

        float xmid = xsum / ant;
        float ymid = ysum / ant;
        Debug.Log("\nMidtpunkt=" + midtpunkt + "  Avvik=" + Avvik);

        string tempstring = String.Format("Gjennomsn x={0}, maxavvikx={1}  minx {2} maxx {4}  [miny {3} maxy {5}]", xmid, Mathf.Abs(midtpunkt) - Mathf.Abs(minx), minx, miny, maxx, maxy);
        Debug.Log(tempstring);

        return xmid;
    }*/

    public float tilfTall(float midtp, float avvik)
    {
        float Pi = 3.14159265f;
        
        float midtpunkt = 0.5f;
        float Avvik = 0.01f;
        float minx=0,maxx=1;

        midtpunkt = midtp;
        Avvik = avvik / 100f;
        float tilf = rng.Next();

        float temp = Mathf.Abs(-2f * Mathf.Log(tilf));
        float r = Mathf.Sqrt(temp);
        float th = 2 * Pi * rng.Next();
        float x = r * Mathf.Cos(th);
        float y = r * Mathf.Sin(th);

        x *= Avvik;
        x += midtpunkt;

        return x;
    }

}



