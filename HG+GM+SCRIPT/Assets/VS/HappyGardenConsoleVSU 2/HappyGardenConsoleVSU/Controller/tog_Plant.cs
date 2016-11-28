using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;

public class tog_Plant : MonoBehaviour
{

    private static bool ison;

    void Start()
    {
        ison = false;
        Graf.ShowP = ison;
    }

    public void toggleChanged()
    {
        if (ison) ison = false; else ison = true;
        Graf.ShowP = ison;
        Debug.Log("plant "+ison);
    }

}
