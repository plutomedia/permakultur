using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;


namespace HappyGardenConsoleVSU
{
    public class btn_readwrite_file3 : MonoBehaviour
    {



        void Start()
        {
            /*
            using(System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData3.txt",true))

                File.WriteLine("tullball line");
    */
            //using(System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData2.txt",true))



        }



        public void onClick()
        {
            Debug.Log("mouse presseeddddd");

            using (System.IO.StreamWriter File = new System.IO.StreamWriter(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\PlantData3.txt", true))

                File.WriteLine("......................onmousedown");
        }

    }
}
