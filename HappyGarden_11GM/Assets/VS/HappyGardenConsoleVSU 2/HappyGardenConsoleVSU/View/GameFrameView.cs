using UnityEngine;
//using System.Threading.Tasks;

namespace HappyGardenConsoleVSU
{
    public class GameFrameView : MonoBehaviour
    {
        //public GameFrame gameFrame;

        public GameFrameView(GameFrame _gameFrame)
        {
            //Viewet gis tilgang til Modellen(GameFrame)
            //gameFrame = _gameFrame;
           Debug.Log("GameFrameView> Splash screens, intro-video, main menu, options, load, save og slutt-video/outro.");
        }





    }//class GameFrameView
}
