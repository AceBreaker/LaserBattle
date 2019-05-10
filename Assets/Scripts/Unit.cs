using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    protected bool canMove;

    public bool CanMove()
    {
        return canMove;
    }

    public virtual void FinalizeMove()
    {
        Debug.Log("ASDFKJASDFKJHASDLFKJHASUnitObject");
    }

    public virtual void UndoMove()
    {

    }
}
