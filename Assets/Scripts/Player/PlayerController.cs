using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    [CreateAssetMenu(menuName = "PlayerControllers/Human")]
    public class PlayerController : ScriptableObject
    {
        public PlayerNumbers playerNumber;

        public GameEvent turnOver;

        public float rotationAmountDegrees;
        protected bool moved = false;

        public GameEvent moveMade;

        public virtual void Initialize(PlayerNumbers pNumber)
        {
        }

        public virtual void Update()
        {
        }

        /// <summary>
        /// Finishes the unit movement and fires the end of turn event
        /// </summary>
        public virtual void FinalizeMove()
        {
        }
    }
}