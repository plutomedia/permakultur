using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.IO;


namespace HappyGardenConsoleVSU
{
    public class btn_readfile1 : MonoBehaviour
    {
        //public Text textTilFil;
        //string[] lines = {"1.linje", "2.linje", "3.linje"};



        void Start()
        {
            //textTilFil="asdfghjkl";


            //System.IO.File.WriteAllLines(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\functions\PlantData.txt", lines);

            System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData4.txt");

            File.WriteLine("Fourth line");

            StreamWriter File2 = new StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData3.txt");

            File2.WriteLine("Fifth line");

        }



    }
}
