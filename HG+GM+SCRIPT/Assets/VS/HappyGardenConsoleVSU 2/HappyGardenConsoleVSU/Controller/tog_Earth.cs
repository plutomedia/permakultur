﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;


public class tog_Earth : MonoBehaviour
{

    private static bool ison;

    void Start()
    {
        ison = false;
        WMG_X_Tutor.ShowE = ison;

    }

    public void toggleChanged()
        {
        if (ison) ison = false; else ison = true;
        WMG_X_Tutor.ShowE = ison;
        Debug.Log("earth " + ison);
    }

}


