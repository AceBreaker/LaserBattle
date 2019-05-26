using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class RestrictedSpace : MonoBehaviour
    {
        [SerializeField] PlayerNumbers playerNumber;

        [SerializeField] float dampener;

        [SerializeField] List<Color> squareColors;

        private void Start()
        {
            if(squareColors.Count > (int)playerNumber )
                gameObject.GetComponent<Renderer>().material.color = squareColors[(int)playerNumber];
        }

        public PlayerNumbers GetPlayerNumber()
        {
            return playerNumber;
        }
    }
}