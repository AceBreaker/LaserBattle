using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class StartGame : MonoBehaviour
    {

        public GameObject ui;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HideUI_StartGame()
        {
            ui.SetActive(false);
            GameManager.gameStarted = true;
        }
    }
}