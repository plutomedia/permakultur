using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

namespace HappyGardenConsoleVSU
{
    public class btn_read_file : MonoBehaviour
    {
        public GameObject Poppupp;

        void Start()
        {

        }


        private bool Load()
        {
            string line;

            using (System.IO.StreamReader Line = new System.IO.StreamReader(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData3.txt"))
            {
                line = Line.ReadLine();

                if (line != null)
                {
                    //while there's lines left in the text fil, do this:
                    do
                    {
                        /*do whatever you need to do with the text line, itæs a string now. 
						 * In this example, I split it into arguments based on comma deliniators, 
						 * then send that array to do''DoStuff()*/

                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                            DoStuff(entries);

                        line = Line.ReadLine();
                    }
                    while (line != null);
                }
                //Done reading, close the reader and return true to broadcast success
                Line.Close();
                return true;

            }
        } //Load


        void DoStuff(string[] entry)
        {
            Debug.Log("Do stuff with " + entry.GetValue(1));
        }


        public void onClick()
        {
            Debug.Log("readfile");
            if (Load() == true)
            {
                Debug.Log("load=true, hva nå DET vil si");
            }
        }
    }
}