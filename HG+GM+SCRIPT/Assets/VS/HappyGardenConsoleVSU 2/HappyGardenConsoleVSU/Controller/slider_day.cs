using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HappyGardenConsoleVSU;


public class slider_day : MonoBehaviour {

    public Slider mainSlider;
    public int dagnr;
    public Text tekst;
    public Text showDayText;


    public void DayChange()
    {
        dagnr = (int)mainSlider.value;
        //Debug.Log(mainSlider.value);

        tekst.text = dagnr.ToString();
        showDayText.text = tekst.text;

        Graf.ChosenDay=dagnr;//ta dette bort når det under funker. prøv ikke å lagre i wmg...
        Initializer.dagValgt=dagnr;

        //Debug.Log(" dagnr.ToString()=" + dagnr.ToString());
    }
}
