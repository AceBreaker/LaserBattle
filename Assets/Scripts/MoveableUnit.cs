using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class MoveableUnit : Unit
    {
        [SerializeField] PlayerNumbers owningPlayerNumber;

        [SerializeField] GameObject floorIgnorer;

        public void OnEnable()
        {
            canMove = true;
            floorIgnorer = transform.FindDeepChild("FloorIgnorer").gameObject;
        }

        public PlayerNumbers GetOwner()
        {
            return owningPlayerNumber;
        }

        public override void FinalizeMove()
        {

        }

        public override void UndoMove()
        {
            Debug.Log(gameObject.name);
            floorIgnorer.gameObject.SetActive(true);
        }
    }
}