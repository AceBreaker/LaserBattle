using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    [CreateAssetMenu(menuName = "PlayerControllers/Human")]
    public class PlayerController : ScriptableObject
    {
        [SerializeField] PlayerNumbers playerNumber;
        [SerializeField] LayerMask raycastLayer;
        [SerializeField] LayerMask raycastLayerWithSelectedObject;
        [SerializeField] Camera mainCamera;

        [SerializeField] GameEvent turnOver;

        [SerializeField] float rotationAmountDegrees;

        GameObject selectedObject = null;

        Vector3 objectPositionWhenSelected;
        Quaternion objectRotationWhenSelected;

        public void Initialize(PlayerNumbers pNumber)
        {
            playerNumber = pNumber;
        }

        private void OnEnable()
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (mainCamera == null)
            {
                Debug.LogError("mainCamera is not set, will cause errors");
            }

            selectedObject = null;
        }

        public virtual void Update()
        {
            //TODO function this out, and get rid of magic numbers.
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (selectedObject == null && Physics.Raycast(ray, out hit, 100.0f, raycastLayer))
                {
                    switch (hit.transform.gameObject.tag)
                    {
                        case "Movable":
                            {
                                if (hit.transform.parent.gameObject.GetComponent<MoveableUnit>().GetOwner() == playerNumber)
                                {
                                    selectedObject = hit.transform.gameObject;
                                    objectPositionWhenSelected = selectedObject.transform.position;
                                    objectRotationWhenSelected = selectedObject.transform.rotation;
                                    selectedObject.transform.Find("FloorIgnorer").gameObject.SetActive(false);
                                }
                                break;
                            }
                    }
                }
                else if (selectedObject != null && Physics.Raycast(ray, out hit, 100.0f, raycastLayerWithSelectedObject))
                {
                    switch (hit.transform.gameObject.tag)
                    {
                        case "Tile":
                            {
                                if (IsNewLocation(hit) && IsValidLocation(hit))
                                {
                                    FinalizeMove();
                                }
                                else
                                {
                                    UndoMove();
                                }
                                break;
                            }
                        case "Movable":
                            {
                                if(selectedObject == hit.transform.gameObject && IsNewLocation(hit) && IsValidLocation(hit))
                                {
                                    FinalizeMove();
                                }
                                else
                                {
                                    UndoMove();
                                }
                                break;
                            }
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                UndoMove();
            }

            OnMouseDrag();
            OnMouseScroll();
        }

        /// <summary>
        /// Resets the position of the selected object to the original location
        /// </summary>
        void UndoMove()
        {
            selectedObject.transform.position = objectPositionWhenSelected;
            selectedObject.transform.rotation = objectRotationWhenSelected;
            selectedObject.transform.Find("FloorIgnorer").gameObject.SetActive(true);
            objectPositionWhenSelected = Vector3.zero;
            objectRotationWhenSelected = Quaternion.identity;
            selectedObject = null;
        }

        /// <summary>
        /// Checks if the location in the RaycastHit is a valid place to move the selected unit
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        bool IsValidLocation(RaycastHit hit)
        {
            if (Mathf.Abs(selectedObject.transform.position.x - objectPositionWhenSelected.x) < 1.5f &&
                Mathf.Abs(selectedObject.transform.position.z - objectPositionWhenSelected.z) < 1.5f &&
                selectedObject.transform.rotation == objectRotationWhenSelected)
            {
                return true;
            }
            else if (Mathf.Abs(selectedObject.transform.position.x - objectPositionWhenSelected.x) == 0.0f &&
                Mathf.Abs(selectedObject.transform.position.z - objectPositionWhenSelected.z) == 0.0f &&
                selectedObject.transform.rotation != objectRotationWhenSelected)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finishes the unit movement and fires the end of turn event
        /// </summary>
        void FinalizeMove()
        {
            selectedObject.transform.Find("FloorIgnorer").gameObject.SetActive(true);
            objectPositionWhenSelected = Vector3.zero;
            objectRotationWhenSelected = Quaternion.identity;
            selectedObject = null;
            turnOver.Raise();
        }

        bool IsNewLocation(RaycastHit hit)
        {
            if(hit.transform.position != objectPositionWhenSelected || hit.transform.rotation != objectRotationWhenSelected)
                return true;

            return false;
        }

        void OnMouseDrag()
        {
            //TODO: get rid of magic numbers
            if (selectedObject)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f, raycastLayerWithSelectedObject))
                {
                    switch(hit.transform.gameObject.tag)
                    {
                        case "Tile":
                            {
                                selectedObject.transform.position = hit.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
                                break;
                            }
                    }
                }
            }
        }

        void OnMouseScroll()
        {
            if(selectedObject == null)
            {
                Debug.LogError("No selectedObject");
                return;
            }

            selectedObject.transform.Rotate(new Vector3(0.0f, 0.0f, rotationAmountDegrees * Input.mouseScrollDelta.y));
        }
    }
}