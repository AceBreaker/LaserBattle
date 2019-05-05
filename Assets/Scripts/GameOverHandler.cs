using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class GameOverHandler : MonoBehaviour
    {

        [SerializeField] string redWinsText;
        [SerializeField] string blueWinsText;

        [SerializeField] Color redWinsColor;
        [SerializeField] Color blueWinsColor;

        [SerializeField] UnityEngine.UI.Text winnerText;
        [SerializeField] GameObject gameOverUI;

        private bool CheckForWinnerTextGameObject()
        {
            if (gameOverUI == null)
            {
                Debug.LogError("gameOverUI is null. Make sure it is set in the inspector");
                return false;
            }
            if (winnerText == null)
            {
                Debug.LogError("winnerText is null. Make sure it is set in the inspector");
                return false;
            }
            return true;
        }

        public void ShowWinner(PlayerNumbers player)
        {
            if (!CheckForWinnerTextGameObject())
            {
                return;
            }
            gameOverUI.SetActive(true);
            switch (player)
            {
                case PlayerNumbers.ONE:
                    {
                        winnerText.text = redWinsText;
                        winnerText.color = redWinsColor;
                        break;
                    }
                case PlayerNumbers.TWO:
                    {
                        winnerText.text = blueWinsText;
                        winnerText.color = blueWinsColor;
                        break;
                    }
                case PlayerNumbers.THREE:
                    {
                        Debug.LogError("Currently does not support three players");
                        break;
                    }
                case PlayerNumbers.FOUR:
                    {
                        Debug.LogError("Currently does not support four players");
                        break;
                    }
                default:
                    Debug.LogError("PlayerNumbers is not valid");
                    break;
            }
        }

        public void ShowWinnerRed()
        {
            ShowWinner(PlayerNumbers.ONE);
        }

        public void ShowWinnerBlue()
        {
            ShowWinner(PlayerNumbers.TWO);
        }
    }
}