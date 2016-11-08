using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Text;

public class LesFraFil: MonoBehaviour
{
	public GameObject UrtePop;
	public GameObject textPlace;
	public Text textField;

	void start(){
		textField = textPlace.GetComponent<Text>();
	}

	public void onClick()
    {
		Debug.Log("lesfrafil onclick");
		//sender canvas i forgrunnen
		UrtePop.SetActive( true );

		//leser tekst fra fil og skriver til 'UrtePop'
		textField = textPlace.GetComponent<Text>();

        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(@"D:\Unity\HappyGarden\HappyGarden\Assets\Scripts\Functions\Flora000.txt"))
            {
	        	// Read the stream to a string, and write the string to the console.
                string line = sr.ReadToEnd();

                Debug.Log(line);

				//tilordner line text til textField i Canvas (popWindow)
				textField.text = line;
            }
        }
        finally
        {
			//Debug.Log("Final:");
        }
    }//onClick


	void OnMouseDown() 
	{
		Debug.Log("musen klikket");
		UrtePop.SetActive( false );
	}
}