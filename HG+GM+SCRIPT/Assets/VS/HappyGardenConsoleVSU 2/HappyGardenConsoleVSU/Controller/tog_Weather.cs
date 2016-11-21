using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;

public class tog_Weather : MonoBehaviour
{


    private static bool ison;

    void Start()
    {
        ison = false;
        WMG_X_Tutor.ShowW = ison;

    }

    public void toggleChanged()
    {
        if (ison) ison = false; else ison = true;
        WMG_X_Tutor.ShowW = ison;
        Debug.Log("weather " + ison);
    }

}
