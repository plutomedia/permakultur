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
            int fieldnr = 0; // i denne versjon 0.1, har vi bare ett 'field' med 4x4 spot


            // 1 find day and spot chosen
            //Spot valgtSpot = WMG_X_Tutor.ChosenSpot;
            //int valgtDag = WMG_X_Tutor.ChosenDay;

            Spot valgtSpot = Initializer.SpotValgt;
            int valgtDag = Initializer.DagValgt;

            Debug.Log("Planter på dag " + valgtDag + " og på spot ( " + valgtSpot.v_index+" ,  "+valgtSpot.h_index+" )");
            Debug.Log("planting              ---------------------------------------------------");

            initializer.Plant("Reddik", fieldnr, valgtSpot.v_index, valgtSpot.h_index);
           /* initializer.Plant("Reddik",1, 2, 2);
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
            initializer.Plant("Bønner", 0, 1, 2);*/

        }
    }
}