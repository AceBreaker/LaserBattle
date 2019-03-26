using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class GameManager : MonoBehaviour {

        public GameObject currentTurnPlayer;
        PlayerNumbers number;

        public void ChangeTurn()
        {
            currentTurnPlayer.GetComponent<Player>().ToggleTurn();
            foreach(Transform child in transform)
            {
                currentTurnPlayer = child.gameObject;
                currentTurnPlayer.GetComponent<Player>().ToggleTurn();
                //currentTurnPlayer.GetComponent<Player>().GetPlayerNumber();
                child.SetAsLastSibling();
                break;
            }
        }
    }
}