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

        Vector3 moveDestination;
        float rotationDestination;
        float speed = 1.0f;
        bool moving;
        float time = 0.0f;
        float lerpTime = 0.3f;
        Vector3 startPosition;

        public GameEvent networkPieceMoved;
        public GameObject ghostObject;

        public void OnEnable()
        {
            canMove = true;
            floorIgnorer = transform.FindDeepChild("FloorIgnorer").gameObject;
        }

        private void Update()
        {
            if (moving)
            {
                time += Time.deltaTime;

                    float tween = time / lerpTime;
                if(time >= lerpTime)
                {
                    if (tween >= 1.0f)
                    {
                        moving = false;
                        tween = 1.0f;
                    }

                }
                    Debug.Log("tween: " + tween.ToString());
                    transform.position = Vector3.Lerp(startPosition, moveDestination, tween);
                    transform.eulerAngles = new Vector3(0.0f, Mathf.LerpAngle(transform.eulerAngles.y, rotationDestination, tween), 0.0f);
            }
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
            Debug.Log("Unit selected event firing here");
            selectedUnitEvent.Raise(gameObject);
        }

        public void UnitUnselected()
        {
            if(unselectedUnitEvent != null)
                unselectedUnitEvent.Raise(gameObject);
        }

        public void NetworkMoveUnit(float x, float z)
        {
            moving = true;
            moveDestination = new Vector3(x, transform.position.y, z);
            time = 0.0f;
            startPosition = transform.position;
        }

        public void NetworkRotateUnit(float rotation)
        {
            time = 0.0f;
            moving = true;
            rotationDestination = rotation;
        }

        public void SpawnGhost()
        {
            networkPieceMoved.Raise();
            Instantiate(ghostObject, transform.position, transform.rotation);
        }
    }
}