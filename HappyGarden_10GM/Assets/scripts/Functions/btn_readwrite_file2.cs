using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;
using System.Collections;
using System.IO;


namespace HappyGardenConsoleVSU
{
    public class btn_readwrite_file2 : MonoBehaviour
    {
        //public Text textTilFil;
        //string[] lines = {"1.linje", "2.linje", "3.linje"};



        void Start()
        {
            //textTilFil="asdfghjkl";


            //System.IO.File.WriteAllLines(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\functions\PlantData.txt", lines);

            using (System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData2.txt"))

                File.WriteLine("Fourth line");

            using (System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData2.txt"))

                File.WriteLine("Fifth line");

        }



    }
}
