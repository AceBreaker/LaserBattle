using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class GameManager : MonoBehaviour {

        public GameObject currentTurnPlayer;
        public static PlayerNumbers number;

        public void ChangeTurn()
        {
            currentTurnPlayer.GetComponent<Player>().ToggleTurn();
            foreach(Transform child in transform)
            {
                currentTurnPlayer = child.gameObject;
                currentTurnPlayer.GetComponent<Player>().ToggleTurn();
                number = currentTurnPlayer.GetComponent<Player>().GetPlayerNumber();
                //currentTurnPlayer.GetComponent<Player>().GetPlayerNumber();
                child.SetAsLastSibling();
                break;
            }
        }

        public void EndTurn()
        {
            currentTurnPlayer.GetComponent<Player>().FinalizeMove();
        }
    }
}