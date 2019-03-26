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
                                selectedObject = null;
                                turnOver.Raise();
                                break;
                            }
                    }
                }
            }

            OnMouseDrag();
            OnMouseScroll();
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