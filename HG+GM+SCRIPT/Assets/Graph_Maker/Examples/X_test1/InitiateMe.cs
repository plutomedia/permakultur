using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitiateMe : MonoBehaviour
{
    ///Lager lister som dummylagrer double verdier tilsvarende nutrients, temperature etc)
    public List<string> nameslist = new List<string> { "N1", "N2", "N3", "N4", "N5", "N6", "N7" };

    public double[] N1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public double[] N2 = { 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5 };
    public double[] N3 = { 3, 2, 1, 0.5, 0, 0.5, 1, 3, 9 };
    public double[] N4 = { 0, 9, 8, 4, 6, 4, 2, 6, 3 };
    public double[] N5 = { 8, 5, 3, 2, 3, 4, 4, 2, 5 };
    public double[] N6 = { 3, 2, 9, 9, 8, 9, 7, 9.9, 9 };
    public double[] N7 = { 4, 4, 4, 4, 3, 4, 2, 0, 0 };


    public 	void Start ()
    {
	    
	}

    public void initiate()
    {



    }

    public List<T> getData<T>()
    {

        return new List<T>();
    }

}
