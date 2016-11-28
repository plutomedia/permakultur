using UnityEngine;
using System.Collections.Generic;
using HappyGardenConsoleVSU;

public class Grafial_1 : MonoBehaviour {

	public GameObject emptyGraphPrefab;
	public WMG_Axis_Graph graph;
	public WMG_Series series1;

	public List<Vector2> series1Data;
	public bool useData2;
	public List<string> series1Data2;

    public bool buttonPressed;




    //her kommer tillegg
    public static Spot[,] zpots;
    public List<Vector2> air, waterMM;



    // Use this for initialization
    void Start () {


        //her kommer tillegg
        zpots = Field.Spots;


        //waterMM = zpots[0, 0].WaterMM;


        //air = zpots[0, 0].Air;



        GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
		graphGO.transform.SetParent(this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph>();
        graph.Init();

		series1 = graph.addSeries();
       
		graph.xAxis.AxisMaxValue = 28;

		if (useData2) {
			List<string> groups = new List<string>();
			List<Vector2> data = new List<Vector2>();
			for (int i = 0; i < series1Data2.Count; i++) {
				string[] row = series1Data2[i].Split(',');
				groups.Add(row[0]);
				if (!string.IsNullOrEmpty(row[1])) {
					float y = float.Parse(row[1]);
					data.Add(new Vector2(i+1, y));
				}
			}

			graph.groups.SetList(groups);
			graph.useGroups = true;

			graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
			graph.xAxis.AxisNumTicks = groups.Count;

			series1.seriesName = "Fruit Data";

			series1.UseXDistBetweenToSpace = true;

			series1.pointValues.SetList(data);

            graph.Refresh();
		}
		else {
			series1.pointValues.SetList(series1Data);
		}
	}//Start()


     public    void Oppdater()
    {
        if (true)
        {
            Debug.Log("valg gjort. eventuelt oppdatering");

            List<string> groups = new List<string>();
            List<Vector2> data = new List<Vector2>();
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

            graph.groups.SetList(groups);
            graph.useGroups = true;

            graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
            graph.xAxis.AxisNumTicks = groups.Count;

            series1.seriesName = "Fruit Data";

            series1.UseXDistBetweenToSpace = true;

            series1.pointValues.SetList(data);

            graph.Refresh();
        }
    }


    public void nyeVerdier()
    {
        Debug.Log("nye verdier importeres eller oppdateres og så skriver jeg grafen");
        
    }


    public void gammelGraf()
    {
        Debug.Log("gammel graf skrives opp igjen");
        series1.pointValues.SetList(series1Data);
    }

}//class WMG_Tutorial
