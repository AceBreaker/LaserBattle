using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class MoveableUnit : MonoBehaviour
    {
        [SerializeField] PlayerNumbers owningPlayerNumber;
        public PlayerNumbers GetOwner()
        {
            return owningPlayerNumber;
        }
    }
}