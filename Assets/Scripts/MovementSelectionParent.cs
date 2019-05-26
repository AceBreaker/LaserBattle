using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class MovementSelectionParent : MonoBehaviour
    {
        CalculateMoveTargets moveCalc;
        GameObject selectedUnit;

        // Use this for initialization
        void Start()
        {
            moveCalc = GetComponent<CalculateMoveTargets>();
            moveCalc.Initialize();
        }

        public void UnitSelected(GameObject pSelectedUnit)
        {
            selectedUnit = pSelectedUnit;
            transform.position = selectedUnit.GetComponentInParent<MoveableUnit>().gameObject.transform.position;

            CalculateMovement();
        }

        public void UnitUnselected(GameObject pSelectedUnit)
        {
            moveCalc.PieceUnselected(selectedUnit);
        }

        void CalculateMovement()
        {
            moveCalc.MyParent = selectedUnit;
            moveCalc.PieceSelected(selectedUnit);
        }
    }
}