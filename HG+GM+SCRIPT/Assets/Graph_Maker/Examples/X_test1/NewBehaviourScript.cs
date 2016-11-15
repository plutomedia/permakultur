using UnityEngine;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{

   //public GameObject emptyGraphPrefab;
    public GameObject existingGraph;

    public WMG_Axis_Graph graph, graph2;

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

    private Vector2 punkt;
    public List<Vector2> series2Data;





    void Start()
    {
        GameObject graphGO = GameObject.Instantiate(existingGraph); /// emptyGraphPrefab instantieres
 
        graphGO.transform.SetParent(this.transform, false);
        graph = graphGO.GetComponent<WMG_Axis_Graph>();
        //graph2 = graphGO.GetComponent<WMG_Axis_Graph>();


        // {setter data inn i serie2, til egne tester
        series2Data = new List<Vector2>();
        for (int i = 0; i < 8; i++)
        {
            punkt = new Vector2(i, i * i);
            series2Data.Add(punkt);
        }
        // slutt på initiering av array }


        //series1 = graph.addSeries();
        graph.xAxis.AxisMaxValue = 5;

        //prøver det samme med series2
        //series2 = graph2.addSeries();
        //graph2.xAxis.AxisMaxValue = 25; //verdien er tatt ut av luften

        if (useData2)
        {
            List<string> groups = new List<string>();
            List<Vector2> data = new List<Vector2>();


            //prøver det same med serie2
           // List<string> groups2 = new List<string>();
           // List<Vector2> data2 = new List<Vector2>();


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

                    //Debug.Log("y=" + y + "  ");
                    //Debug.Log("row2y=" + row2y + "  ");

                    data.Add(new Vector2(i + 1, y));
                    //data2.Add(new Vector2(i+1, row2y));
                }
            }

            /*        //prøver det samme
                    for (int i = 0; i < series1Data2.Count; i++)
                    {
                        string[] row = series1Data2[i].Split(',');
                        groups.Add(row[0]);

                        if (!string.IsNullOrEmpty(row[1]))
                        {
                            float y = float.Parse(row[1]);
                            data.Add(new Vector2(i + 1, y));
                        }
                    }
        */
            graph.groups.SetList(groups);
            graph.useGroups = true;

            graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
            graph.xAxis.AxisNumTicks = groups.Count * 2 - 1;

            series1.seriesName = "FryktData";

            series1.UseXDistBetweenToSpace = true;
            series2.UseXDistBetweenToSpace = true;

            series1.pointValues.SetList(data);
            series2.pointValues.SetList(data);
        }
        else
        {
            series1.pointValues.SetList(series1Data);
        }



    }

}
