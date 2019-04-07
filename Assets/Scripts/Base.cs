using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class Base : MonoBehaviour {
        public PlayerNumbers playerNumber;

        public GameEvent baseDestroyedEvent;

        public void ObjectDestroyed()
        {
            Destroy(gameObject);
        }
    }
}