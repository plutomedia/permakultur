using UnityEngine;
using System.Collections;


namespace HappyGardenConsoleVSU
{
    public class btn_PlantXX : MonoBehaviour
    {
        public Initializer initializer;

        //this button Plant a plant on a fixed Spot let¨s say at 2,2 Field 1



        public void Plant()
        {
            //fieldnr = 1;
            //spotx = 2;
            //spoty = 2;

            Debug.Log("planting-----------------------------------------------------------------");
            initializer.Plant("Reddik",1, 2, 2);
            initializer.Plant("Reddik",1, 2, 1);
            initializer.Plant("Reddik",1, 2, 0);
            initializer.Plant("Reddik",1, 1, 2);

            initializer.Plant("Salvie", 1, 0, 2);
            initializer.Plant("Salvie", 1, 0, 1);
            initializer.Plant("Salvie", 1, 0, 0);
            initializer.Plant("Salvie", 1, 1, 1);

            initializer.Plant("Bønner", 1, 0, 2);
            initializer.Plant("Bønner", 0, 2, 1);
            initializer.Plant("Bønner", 0, 2, 0);
            initializer.Plant("Bønner", 0, 1, 2);

        }
    }
}