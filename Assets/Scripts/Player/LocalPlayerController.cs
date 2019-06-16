using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;

namespace LaserBattle
{
    [CreateAssetMenu(menuName = "PlayerControllers/LocalHuman")]
    public class LocalPlayerController : PlayerController
    {
        [SerializeField] LayerMask raycastLayer;
        [SerializeField] LayerMask raycastLayerWithSelectedObject;
        [SerializeField] Camera mainCamera;

        GameObject selectedObject = null;
        GameObject movedObject = null;

        Vector3 objectPositionWhenSelected;
        Quaternion objectRotationWhenSelected;
        public GameEvent moveUndone;

        const byte MOVEMENT_TAG = 1;

        public UnityClient client { get; set; }

        public override void Initialize(PlayerNumbers pNumber)
        {
            playerNumber = pNumber;

            client = GameObject.Find("Network").GetComponent<UnityClient>();

            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (mainCamera == null)
            {
                Debug.LogError("mainCamera is not set, will cause errors");
            }
        }

        private void OnEnable()
        {
            selectedObject = null;
            moved = false;
        }

        public override void Update()
        {
            //TODO function this out, and get rid of magic numbers.
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!moved && selectedObject == null && Physics.Raycast(ray, out hit, 100.0f, raycastLayer))
                {
                    switch (hit.transform.gameObject.tag)
                    {
                        case "Movable":
                            {
                                MoveableUnit unit = hit.transform.gameObject.GetComponentInParent<MoveableUnit>();
                                if (unit != null && unit.GetOwner() == playerNumber)
                                {
                                    selectedObject = unit.gameObject;
                                    objectPositionWhenSelected = selectedObject.transform.position;
                                    objectRotationWhenSelected = selectedObject.transform.rotation;

                                    blah = selectedObject.transform.rotation.eulerAngles.y;

                                    Transform go = selectedObject.transform.FindDeepChild("FloorIgnorer");
                                    if (go != null)
                                    {
                                        go.gameObject.SetActive(false);
                                    }
                                    //selectedObject.transform.Find("FloorIgnorer").gameObject.SetActive(false);
                                    unit.UnitSelected();
                                }
                                break;
                            }
                        case "Laser":
                            {
                                Debug.Log("laser hit");
                                if (hit.transform.gameObject.GetComponent<LaserGun>().GetOwner() == playerNumber)
                                {
                                    hit.transform.gameObject.GetComponent<LaserGun>().ToggleLaserAim();
                                    movedObject = hit.transform.gameObject;
                                }
                                break;
                            }
                        default:
                            {
                                Debug.Log("Correct tag was not hit. Object was: " + hit.transform.gameObject.name + " with a tag of: " + hit.transform.gameObject.tag);
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
                                    MoveableUnit u = selectedObject.GetComponent<MoveableUnit>();
                                    if (u != null)
                                    {
                                        u.UnitUnselected();
                                    }
                                    moved = true;
                                    movedObject = selectedObject;
                                    selectedObject = null;
                                    moveMade.Raise();
                                }
                                else
                                {
                                    UndoMove();
                                }
                                break;
                            }
                        case "Movable":
                            {
                                MoveableUnit unit = hit.transform.GetComponentInParent<MoveableUnit>();
                                if (unit != null && selectedObject == unit.gameObject && IsNewLocation(hit) && IsValidLocation(hit))
                                {
                                    //MoveableUnit u = selectedObject.GetComponentInParent<MoveableUnit>();
                                    if (unit != null)
                                    {
                                        unit.UnitUnselected();
                                    }
                                    moved = true;
                                    movedObject = selectedObject;
                                    selectedObject = null;
                                    moveMade.Raise();
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UndoMove();
            }

            OnMouseDrag();
            OnMouseScroll();

            LogDebugInfo();
        }

        private void LogDebugInfo()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                DebugInfo();
            }
        }

        private void DebugInfo()
        {
            string so = selectedObject == null ? "null" : selectedObject.name;
            string mo = movedObject == null ? "null" : movedObject.name;
            Debug.Log(so + "::" + mo);
        }

        /// <summary>
        /// Resets the position of the selected object to the original location
        /// </summary>
        void UndoMove()
        {
            if (movedObject != null)
                selectedObject = movedObject;
            Unit u = selectedObject.GetComponent<Unit>();
            if (u == null)
            {
                u = selectedObject.GetComponentInParent<Unit>();
            }
            u.UndoMove();

            if (u.CanMove())
            {
                selectedObject.transform.position = objectPositionWhenSelected;
                selectedObject.transform.rotation = objectRotationWhenSelected;

                objectPositionWhenSelected = Vector3.zero;
                objectRotationWhenSelected = Quaternion.identity;
            }
            selectedObject = null;
            movedObject = null;
            moved = false;
            moveUndone.Raise();
        }

        /// <summary>
        /// Checks if the location in the RaycastHit is a valid place to move the selected unit
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        bool IsValidLocation(RaycastHit hit)
        {
            Debug.Log(hit.transform.gameObject.ToString());
            if (spaceColorMismatch)
            {
                return false;
            }
            else if (Mathf.Abs(selectedObject.transform.position.x - objectPositionWhenSelected.x) < 1.5f &&
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
        public override void FinalizeMove()
        {
            Debug.Log("finalizing move here");
            if (movedObject == null)
            {
                return;
            }

            Unit u = movedObject.GetComponent<Unit>();
            if (u == null)
            {
                u = movedObject.GetComponentInParent<Unit>();
            }
            u.FinalizeMove();
            //movedObject.transform.Find("FloorIgnorer").gameObject.SetActive(true);
            if (u.CanMove())
            {
                objectPositionWhenSelected = Vector3.zero;
                objectRotationWhenSelected = Quaternion.identity;
            }

            SendMessage(movedObject);
            selectedObject = null;
            movedObject = null;
            moved = false;
            turnOver.Raise();
        }

        void SendMessage(GameObject movingObject)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                Transform go = movingObject.transform.parent;
                int objectIndex = -1;
                for(int i = 0; i < go.childCount; i++)
                {
                    if(go.GetChild(i).gameObject == movingObject)
                    {
                        objectIndex = i;
                        break;
                    }
                }
                writer.Write(objectIndex);
                writer.Write(movingObject.transform.position.x);
                writer.Write(movingObject.transform.position.z);
                writer.Write(movingObject.transform.rotation.eulerAngles.y);
                Debug.Log("asdfkjhasdfkljhasdklfjh");
                using (Message message = Message.Create(MOVEMENT_TAG, writer))
                {
                    client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        bool IsNewLocation(RaycastHit hit)
        {
            if (hit.transform.position != objectPositionWhenSelected || hit.transform.rotation != objectRotationWhenSelected)
                return true;

            return false;
        }

        bool spaceColorMismatch = false;

        void OnMouseDrag()
        {
            //TODO: get rid of magic numbers
            if (selectedObject)
            {
                if(mainCamera == null)
                {
                    GameObject camObject = GameObject.Find("Main Camera");
                    if(camObject == null)
                    {
                        Debug.LogError("There is no Main Camera object in the scene. Aborting OnMouseDrag");
                        return;
                    }
                    mainCamera = camObject.GetComponent<Camera>();
                }
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (GameManager.gameStarted && Physics.Raycast(ray, out hit, 100.0f, raycastLayerWithSelectedObject))
                {
                    switch (hit.transform.gameObject.tag)
                    {
                        case "Tile":
                            {
                                //selectedObject.transform.position = hit.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
                                Vector3 startPosition = selectedObject.transform.position;
                                Vector3 endPosition = hit.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
                                selectedObject.GetComponent<MoveUnit>().MoveToPosition(startPosition, endPosition);
                                RestrictedSpace space = hit.transform.gameObject.GetComponent<RestrictedSpace>();
                                if (space == null)
                                {
                                    Debug.Log("space is null");
                                }
                                else
                                    Debug.Log("space ISNT null");
                                if (space != null && space.GetPlayerNumber() != selectedObject.transform.GetComponent<MoveableUnit>().GetOwner())
                                {
                                    Debug.Log("color mismatch");
                                    spaceColorMismatch = true;
                                }
                                else
                                    spaceColorMismatch = false;
                                break;
                            }
                    }
                }
            }
        }

        public float blah = 0.0f;
        void OnMouseScroll()
        {
            if (selectedObject == null)
            {
                Debug.LogError("No selectedObject");
                return;
            }

            float inputY = Input.mouseScrollDelta.y;
            if (inputY != 0.0f)
            {
                blah += 90.0f * inputY;
                selectedObject.GetComponent<RotateUnit>().SetNewRotation(selectedObject.transform.eulerAngles, inputY);
            }
        }
    }
}