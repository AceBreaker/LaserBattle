using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LaserBattle
{
    public class LaserGun : Unit {

        [SerializeField] int holdLaserFrameCount;
        LineRenderer line;
        [SerializeField] LayerMask layer;

        [SerializeField] Vector3 facingDirection;

        [SerializeField] PlayerNumbers playerNumber;

        int direction = -1;

        [SerializeField] GameEventGameObject1x destroyedObjectGameEvent;

        AudioManager audio;

        private void Awake()
        {
            canMove = false;
            line = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        public PlayerNumbers GetOwner()
        {
            return playerNumber;
        }

        public void ToggleLaserAim()
        {
            transform.Rotate(new Vector3(0.0f, 90.0f * direction, 0.0f));
            direction *= -1;
        }

        public override void FinalizeMove()
        {
            Debug.Log("finalizing move");
        }

        public override void UndoMove()
        {
            ToggleLaserAim();
        }

        public void FireLaser()
        {
            StartCoroutine(HoldLaser());
        }

        private IEnumerator HoldLaser()
        {
            audio.PlayAudio("laser");
            line.enabled = true;
            CalculateLaserTrajectory();
            for (int i = 0; i < holdLaserFrameCount; ++i)
            {
                yield return null;
            }
            audio.StopAudio("laser");
            Vector3[] blahj = new Vector3[0];
            line.SetPositions(blahj);
            line.enabled = false;
        }

        private void CalculateLaserTrajectory()
        {
            Vector3 startVector = new Vector3(transform.position.x, 0.91f, transform.position.z);

            Vector3 directionVector = transform.forward;
            List<Vector3> vectors = new List<Vector3>();
            int i = 0;
            vectors.Add(startVector);
            Debug.Log(directionVector.ToString());
            while (++i < 50)
            {
                Ray ray;
                ray = new Ray(startVector, directionVector);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    startVector = hit.transform.position;

                    directionVector = Vector3.Reflect(directionVector, hit.normal);
                    vectors.Add(hit.transform.position);
                    if (hit.transform.gameObject.name == "IgnoreLaserBounce")
                    {
                        AddToLine(vectors);
                        return;
                    }
                    if (hit.transform.gameObject.name == "DestroyObject")
                    {
                        TriggerObjectDeath(hit.transform.gameObject);
                        AddToLine(vectors);
                        return;
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Base"))
                    {
                        AddToLine(vectors);
                        Base b = hit.transform.gameObject.GetComponent<Base>();
                        b.baseDestroyedEvent.Raise();
                        return;
                    }
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Laser"))
                    {
                        AddToLine(vectors);
                        return;
                    }
                }
                else
                {
                    break;
                }
            }

            vectors.Add(new Vector3(directionVector.x, 0.0091f, directionVector.z) * 100 + vectors[vectors.Count - 1]);
            AddToLine(vectors);
        }

        void TriggerObjectDeath(GameObject gameObject)
        {
            if(destroyedObjectGameEvent == null)
            {
                Debug.LogError("No event has been set.");
            }
            destroyedObjectGameEvent.Raise(gameObject.GetComponent<ParentReference>().parent);
            //DestructibleObject dObject = gameObject.GetComponent<ParentReference>().parent.GetComponent<DestructibleObject>();
            //if(dObject == null)
            //{
            //    Debug.LogError("No DestructibleObject. Consider adding one in the inspector");
            //}
            //dObject.DestroyObject();
        }

        void AddToLine(List<Vector3> linePoints)
        {
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }
    }
}