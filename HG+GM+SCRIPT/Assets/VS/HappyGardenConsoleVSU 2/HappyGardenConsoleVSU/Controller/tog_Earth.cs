using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace HappyGardenConsoleVSU
{
    public class tog_Earth : MonoBehaviour
    {

        public Toggle toggler;

        public void toggleChanged()
        {
            Debug.Log("knapp presset opp eller ned " + toggler.isOn);
            
        }
    }
}