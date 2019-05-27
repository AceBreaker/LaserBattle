using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.quittingOrChangingScene = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }

        public void ExitGame()
        {
            GameManager.quittingOrChangingScene = true;
            Application.Quit();
        }
    }
}