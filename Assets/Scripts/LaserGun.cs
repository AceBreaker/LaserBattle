using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LaserBattle
{
    public class LaserGun : MonoBehaviour {

        [SerializeField] int holdLaserFrameCount;
        LineRenderer line;
        [SerializeField] LayerMask layer;

        [SerializeField] Vector3 facingDirection;

        [SerializeField] PlayerNumbers playerNumber;

        private void Awake()
        {
            line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    FireLaser();
            //}
        }

        public void FireLaser()
        {
            StartCoroutine(HoldLaser());
        }

        private IEnumerator HoldLaser()
        {
            line.enabled = true;
            CalculateLaserTrajectory();
            for (int i = 0; i < holdLaserFrameCount; ++i)
            {
                yield return null;
            }
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
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Base"))
                    {
                        AddToLine(vectors);
                        Debug.Log("A player base has been hit and someone loses here");
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

        void AddToLine(List<Vector3> linePoints)
        {
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }
    }
}