using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Player : MonoBehaviour
    {

        [SerializeField] PlayerNumbers playerNumber;
        [SerializeField] bool myTurn;
        [SerializeField] PlayerController controller;

        private void Start()
        {
            controller.Initialize(playerNumber);
        }

        // Update is called once per frame
        void Update()
        {
            if (myTurn)
            {
                controller.Update();
            }
        }

        public void ToggleTurn()
        {
            Debug.Log(gameObject.ToString());
            myTurn = !myTurn;
        }

        public PlayerNumbers GetPlayerNumber()
        {
            return playerNumber;
        }

        public void FinalizeMove()
        {
            controller.FinalizeMove();
        }
    }
}