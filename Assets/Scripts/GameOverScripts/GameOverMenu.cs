using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class GameOverMenu : MonoBehaviour
    {

        public void Rematch()
        {
            GameManager.quittingOrChangingScene = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void ExitToMainMenu()
        {
            GameManager.quittingOrChangingScene = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}