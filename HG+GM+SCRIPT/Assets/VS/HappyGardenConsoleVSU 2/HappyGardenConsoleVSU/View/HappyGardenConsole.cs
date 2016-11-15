using UnityEngine;
using System.Collections.Generic;



namespace HappyGardenConsoleVSU
{
    public class HappyGardenConsole
    {
        //private fields

        public int earthQuality;
        public double earthHumidity;
        public double earthAcidity;
        public int month;
        private string command;

        //private MainController Kontroller;
        public GameFrame gameFrame;

        private Game gama;
        private Farm frm;
        private List<Field> flds;

        //private Flora flr;
        public Spot[,] spotts;




        public HappyGardenConsole(GameFrame gFrame)
        {
            this.gameFrame = gFrame;
            Debug.Log("HappyGardenConsole opprettet.");
        }


        public int EarthQuality
        {
            //this function makes tall1 public to the controller
            get { return earthQuality; }
            set { earthQuality = value; }
        }


        public double EarthHumidity
        {
            //this function makes age public to the controller
            get { return earthHumidity; }
            set { earthHumidity = value; }
        }


        public double EarthAcidity
        {
            //this function makes age public to the controller
            get { return earthAcidity; }
            set { earthAcidity = value; }
        }


        public int Month
        {
            //this function makes age public to the controller
            get { return month; }
            set { month = value; }
        }


        public string Command
        {

            get { return command; }
            set { command = value; }
        }


        /// ------------------------------------------------------------------------

        public void GetCommand()
        {
            Debug.Log("@ ");
            //Command = Console.ReadLine();



            // n e w p s r
            /* m menu, e earth parameters, w water, p plant, s simulate, r run simulation*/


         
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("Menu");
                    Command = "M";

                }
                else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Earth Parameters ");
                Command = "E";

            }
                else if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Water Parameters");
                Command = "W";

            }
                else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Plant Parameters");
                Command = "P";

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Simulate");
                Command = "S";

            }     
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("Simulate");
                Command = "S";

            }         
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Run New Simulation");
                Command = "R";

            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Quit");
                Command = "Q";

            }
            else
                {
                Debug.Log("Not defined input");
                Command = "?";
                }
        }


        public void GetValues()
        {

            Debug.Log("Enter earth quality (1-5): ");
            //Tall1 = double.Parse(Console.ReadLine()); Debug.Log(tall1);
            //earthQuality = int.Parse(Console.ReadLine()); Debug.Log(earthQuality);

            Event e = Event.current;
            if (e.isKey)
                Debug.Log("Detected key code: " + e.keyCode);


            Debug.Log("Enter earth wetness (1-5): ");
           // earthHumidity = int.Parse(Console.ReadLine()); Debug.Log(earthHumidity);


            Debug.Log("Enter Acidity (4-10): ");
            //earthAcidity = double.Parse(Console.ReadLine()); Debug.Log(earthAcidity);


            Debug.Log("Enter Month (1-12): ");
           // month = int.Parse(Console.ReadLine()); Debug.Log(month);

            UpdateView();
        }



        public void UpdateView()
        {

            if (earthQuality == 0)
            {
                //initiering av Earth dummy variable
                earthQuality = 3;
                earthHumidity = 2;
                earthAcidity = 7;
                month = 4;
            }

            Debug.Log("earthQuality:"); Debug.Log(earthQuality);
            Debug.Log("earthHumidity:"); Debug.Log(earthHumidity);
            Debug.Log("earthAcidity:"); Debug.Log(earthAcidity);
            Debug.Log("month:"); Debug.Log(month);
        }


        public void WriteMenu()
        {
            Debug.Log("");
            Debug.Log("Earth Parameters: E");
            Debug.Log("Water Parameters: W");
            Debug.Log("Plant Parameters: P");
            Debug.Log("Run Simulation: S");
            Debug.Log("Display earth state: O");
            Debug.Log("Quit Program: Q");
            //  Debug.Log("Start Game: S");
            //           Debug.Log("This Menu: M");
            Debug.Log("");
        }


        public void RunSimulation()
        {
            Debug.Log("\n\nS I M U L A T I O N\n");
            Debug.Log("This are the earth values:\n");
            UpdateView();
            /* default-verdier
                earthQuality = 3;
                earthHumidity = 2;
                earthAcidity = 7;
                month = 4;       */

            Debug.Log("\nThis is the given weather conditions:\n");
            Debug.Log("period 14 days:  ssss r cc sss cccc\nsun4d-rain1day-cloudy2days-sun3days-cloudy4days");

            Debug.Log("\nThis is the given plant:\n");
            Debug.Log("1 fast growing phantasy plant. Grows according to earth and weather");

            Debug.Log("\n  ?? disse verdier blir gitt til modellen.");
            Debug.Log("  det er viewet (HappyGardenConsole) som gjør dette.");
            Debug.Log("  Viewet skal så oppdage dette, og oppdatere hvis det er gjort forandringer");
            Debug.Log("  Men dette skal ikke gjøres før i Application.run-form-vinduene");
        }



        public void RunOutput()
        {
            gama = gameFrame.game;
            //frm = gama.farmen;
            flds = Farm.Fields;

            //spotts = frm.fields.spots;

            Debug.Log("\n\nW R I T E   F I E L D S\n");
            Debug.Log("This are the earth values:\n");
            Debug.Log("grame." + gama);
            /*
            foreach (Field i in flds)
            {
                // System.Debug.Log(i);

                spotts = new Spot[10, 10];

                spotts = i.spots;
                //public Spot[,] jordbit;

                for (int ii = 0; ii < 2; ii++)
                {
                    for (int jj = 0; jj < 3; jj++)
                    {
                        //spotts[ii, jj];
                        Debug.Log(">" + ii + " , " + jj + " :" + spotts[ii, jj].nutr_C);
                       // spotts[ii, jj].WriteSpot();
                    }
                }

            }
            */

        }

    } //public class View1
}
