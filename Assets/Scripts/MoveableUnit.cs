using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class MoveableUnit : Unit
    {
        [SerializeField] PlayerNumbers owningPlayerNumber;

        [SerializeField] GameObject floorIgnorer;

        [SerializeField] GameEventGameObject1x selectedUnitEvent;
        [SerializeField] GameEventGameObject1x unselectedUnitEvent;

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
            Vector3 additive = transform.GetChild(0).transform.localPosition;
            transform.position -= additive;
            transform.GetChild(0).transform.localPosition = Vector3.zero;
            UnitUnselected();
        }

        public override void UndoMove()
        {
            Debug.Log(gameObject.name);
            UnitUnselected();
            floorIgnorer.gameObject.SetActive(true);
        }

        public void UnitSelected()
        {
            selectedUnitEvent.Raise(gameObject);
        }

        public void UnitUnselected()
        {
            Debug.Log("asdfasdfasdfasdf");
            if(unselectedUnitEvent != null)
                unselectedUnitEvent.Raise(gameObject);
        }
    }
}