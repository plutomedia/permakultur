using UnityEngine;
using System.Collections.Generic;

public class Grafial : MonoBehaviour {

    public GameObject denneGraf;

    public WMG_Axis_Graph graph,graph2;

    public WMG_Series series1;
    public WMG_Series series2;
    public WMG_Series series3;
    public WMG_Series series4;
    public WMG_Series series5;
    public WMG_Series series6;
    public WMG_Series series7;
    public WMG_Series series8;

    public List<Vector2> series1Data;
    public List<string> series1Data2;
    public bool useData2;

    //mine egne verdier og tester. gjøre om x og y til vector2
    //hvor x er tiden, og y er vertikal avmerking
    //med mange verdier, noen fra 0 til 1, andre fra 0 til 100 eller fra 0 til 0.001
    //må hver av data-set'ene tilpasses y-verdien.
    //lager et Vector2 objekt for å prøve ut dette

    public List<Vector2> series2Data;
    public List<Vector2> series3Data;
    public List<Vector2> series4Data;
    public List<Vector2> series5Data;
    public List<Vector2> series6Data;
    public List<Vector2> series7Data;
    public List<Vector2> series8Data;


    private Vector2 punkt, punkt2, punkt3, punkt4, punkt5,punkt6,punkt7,punkt8;


    void Start ()
    {
        GameObject graphGO = GameObject.Instantiate(denneGraf); /// emptyGraphPrefab instantieres
        graphGO.transform.SetParent(this.transform, false);

        graph = graphGO.GetComponent<WMG_Axis_Graph>();
        graph2 = graphGO.GetComponent<WMG_Axis_Graph>();    //blir ingen graf hvis jeg kutter ut en av dem


        // {setter data inn i serie2, til egne tester
        series2Data = new List<Vector2>();
        series3Data = new List<Vector2>();
        series4Data = new List<Vector2>();
        series5Data = new List<Vector2>();
        series6Data = new List<Vector2>();
        series7Data = new List<Vector2>();
        series8Data = new List<Vector2>();

        for (int i = 0;i< 8; i++)
        {
            punkt = new Vector2(i, i*i);
            punkt2 = new Vector2(i, i / (i * 2f+0.2f));
            punkt3 = new Vector2(i, i + (0.5f * i * i) - (2f * i));
            punkt4 = new Vector2(i, Mathf.Abs(3f * i - 0.5f * i * i));
            punkt5 = new Vector2(i, Mathf.Abs(i / (i * 2f) - 4));
            punkt6 = new Vector2(i, Mathf.Abs(2* i - 0.8f * i * i));
            punkt7 = new Vector2(i, Mathf.Abs(i - 5f) * (i - 3f));
            punkt8 = new Vector2(i, i);

            series2Data.Add(punkt);
            series3Data.Add(punkt2);
            series4Data.Add(punkt3);
            series5Data.Add(punkt4);
            series6Data.Add(punkt5);
            series7Data.Add(punkt6);
            series8Data.Add(punkt8);
        }
        // slutt på initiering av array }


        series1 = graph.addSeries();
        graph.xAxis.AxisMaxValue = 10;

        //prøver det samme med series2
        series2 = graph2.addSeries();
        graph2.xAxis.AxisMaxValue = 25; //verdien er tatt ut av luften

        if (useData2)
        {
            List<string> groups = new List<string>();

            List<Vector2> data = new List<Vector2>();

            List<Vector2> data3 = new List<Vector2>();
            List<Vector2> data4 = new List<Vector2>();
            List<Vector2> data5 = new List<Vector2>();
            List<Vector2> data6 = new List<Vector2>();
            List<Vector2> data7 = new List<Vector2>();
            List<Vector2> data8 = new List<Vector2>();


            //prøver det same med serie2
            List<string> groups2 = new List<string>();
            List<Vector2> data2 = new List<Vector2>();


            for (int i = 0; i < series1Data2.Count; i++)
            {
                string[] row = series1Data2[i].Split(',');

                //prøver å hente ut x og y verdier fra vector2
                int row2x = (int)series2Data[i].x;
                //int row2y = (int)series2Data[i].y;

                //Debug.Log("int row2 x= " + row2x + " ,   int row2 y= "+ row2y);

                groups.Add(row[0]);
                //groups2.Add(row[2]);

                if (!string.IsNullOrEmpty(row[1]))
                {
                    float y = float.Parse(row[1]);
                    
                    int row2y = (int)series2Data[i].y;

                    Debug.Log("y=" + y + "  ");
                    Debug.Log("row2y=" + row2y + "  ");

                    data.Add(new Vector2(i + 1, y));
                    data2.Add(new Vector2(i+1, row2y));

                    data3.Add(new Vector2(punkt3.x,punkt3.y));
                    Debug.Log("data3.Add(new Vector2(punkt3.x,punkt3.y));  hvor punkt3.x=" + punkt3.x + "   og punkt 3.y=" + punkt3.y);
                }
            }



            graph.groups.SetList(groups);
            graph.useGroups = true;

            graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
            graph.xAxis.AxisNumTicks = groups.Count*2-1;

            series1.seriesName = "Fruit Data";

            series1.UseXDistBetweenToSpace = true;
            series2.UseXDistBetweenToSpace = true;

            /*
            series1.pointValues.SetList(data);
            series2.pointValues.SetList(data2);
            series3.pointValues.SetList(data3);
            series4.pointValues.SetList(data4);
            series5.pointValues.SetList(data5);
            series6.pointValues.SetList(data6);
            */
        }
        else
        {
            series1.pointValues.SetList(series1Data);
        }
        

        
	}

}
