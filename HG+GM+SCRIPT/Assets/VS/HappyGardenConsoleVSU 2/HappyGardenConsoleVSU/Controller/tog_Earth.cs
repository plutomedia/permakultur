using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;


public class tog_Earth : MonoBehaviour
{

    private static bool ison;

    void Start()
    {
        ison = false;
        Graf.ShowE = ison;

    }

    public void toggleChanged()
        {
        if (ison) ison = false; else ison = true;
        Graf.ShowE = ison;
        Debug.Log("earth " + ison);
    }

}


