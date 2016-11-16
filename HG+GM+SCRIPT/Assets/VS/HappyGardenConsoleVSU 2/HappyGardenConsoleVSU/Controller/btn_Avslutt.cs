using UnityEngine;
using System.Collections;
using System;

namespace HappyGardenConsoleVSU
{
    public class btn_Avslutt : MonoBehaviour
    {
        public Initializer initializer;
        int fieldNr;

        public btn_Avslutt()
        {

            //result();
        }

        public void random()
        {
            float Pi = 3.14159265f;
            System.Random rng = new System.Random();
            float xsum = 0;
            double ysum = 0;

            Debug.Log("xsum:  " + xsum);

            int ant=10000;
            float minx, miny, maxx, maxy;


            float midtpunkt = 0.5f;
            float avvik = 0.01f;


            minx = midtpunkt; miny = midtpunkt;//skal ende opp sim min-verdier for x hhv y
            maxx = midtpunkt; maxy = midtpunkt;

            for (int i=0; i < ant; i++)
            {
                float temp = Mathf.Abs(-2 * Mathf.Log(rng.Next())); 

                float r = Mathf.Sqrt(temp);

                

                float th = 2 * Pi * rng.Next();

                float x = r * Mathf.Cos(th);
                float y = r * Mathf.Sin(th);

                //midtpunkt og avvik. tweake til vårt scope
                
                x *= avvik;
                x += midtpunkt;

                xsum = xsum + x;
                ysum = ysum + y;

                if (x < minx) minx = x;
                if (y < miny) miny = y;

                if (x > maxx) maxx = x;
                if (y > maxy) maxy = y;




                string tempstr = String.Format("x={0}, y={1}  ", x, y);
                //Debug.Log(tempstr);

            }
            double xmid = xsum / ant;
            double ymid = ysum / ant;
            Debug.Log("Midtpunkt="+midtpunkt);

            string tempstring = String.Format("Gjennomsn x={0}, maxavvikx={1}  minx {2} maxx {4}  [miny {3} maxy {5}]", xmid,Mathf.Abs(midtpunkt)-Mathf.Abs(minx), minx, miny, maxx, maxy);
            Debug.Log(tempstring);
        }

        public float random(float midtp, float avvik)
        {
            float Pi = 3.14159265f;
            System.Random rng = new System.Random();
            float xsum = 0;
            float ysum = 0;

            Debug.Log("xsum:  " + xsum);

            int ant = 10000;
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
        }


        public void result()
        {
            /// x=0 når toppen av normalfordelingen er rundt x=0
            /// my=forventning, der hvor midten av kurven skal dreie seg rundt
            /// sigma=varians. I praksis hvor bred kurven er
            /// (my,sigma**1)=(0,1) gir en enklere formel:
            /// 

            float pi, sigma, my, x;
            float fx, nevner, epotens, epotens2,expfaktor;

            pi = 3.1415826535f; sigma = 1f; my = 1f; x=1f; 

            //Debug.Log("Normalfordeling");
            //Debug.Log("Forventning my="+my);
            //Debug.Log("Standardavvik sigma="+sigma);
            //Debug.Log("Normalfordeling");




            nevner = Mathf.Sqrt(2 * pi) * sigma;
            Debug.Log("nevner:    Mathf.Sqrt(2 * pi) * sigma=" + nevner);

            epotens = -(x - my) / (2 * sigma*sigma);
            //Debug.Log("e skal opphøyes i epotens**2 =" + epotens+"**2");

            epotens2 = epotens * epotens;
            //Debug.Log("e skal opphøyes i epotens2=" + epotens2);

            expfaktor = Mathf.Exp(epotens2);
            //Debug.Log("e oppøyd i " + epotens2 + " gir Mathf.Exp(epotens2)=" + expfaktor);

            fx = (1 / nevner) * Mathf.Exp(((x-my)/2*sigma)*((x - my) / 2 * sigma));
            //Debug.Log(fx);
            fx = (1 / nevner) * expfaktor;
            //Debug.Log("f(x) = " + fx);


            random(0.7f,1f);





        }
    }
}