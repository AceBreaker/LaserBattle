using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LaserBattle
{
    public class LaserCharger : MonoBehaviour {

        public GameObject laserGraphics;
        public float laserHeight;

        public float rotationAcceleration;
        public float rotationSpeed;

        public delegate void Job();

        List<Job> jobsList = new List<Job>();

        bool jobsDone = false;
        bool jobsDone1 = false;
        bool jobsDone2 = false;
        bool allJobsDone = false;

        Transform startingTransform;

        public float movementTime;
        public float rotationTime;

        float timeSinceStart1;
        float timeSinceStart2;

        float time = 0.0f;
        bool blah = false;

        public PlayerNumbers myPlayerNumber;

        AudioManager audio;

        private void Start()
        {
            audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        private void Update()
        {
            if (jobsDone)
            {
                if (jobsList.Count > 0)
                    jobsList.RemoveAt(0);
                allJobsDone = jobsList.Count == 0;
                if (!allJobsDone)
                    jobsDone = false;
                else
                {
                    ResetAllData();
                }
            }
            else
            {
                if (jobsList.Count > 0)
                    jobsList[0]();
            }

            rotVel += rotSpeed;
                
            if (rotVel > 360.0f)
            {
                audio.PlayAudioOneShot("whoosh");
                rotVel -= 360.0f;
            }
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotVel);
        }

        void ResetAllData()
        {
            timeSinceStart1 = timeSinceStart2 = time = 0.0f;
            jobsDone = jobsDone1 = jobsDone2 = false;
        }

        public void StartCharge()
        {
            if (myPlayerNumber != GameManager.number)
                return;

            if (jobsList == null)
            {
                Debug.Log("jobsList is null");
                return;
            }
            ResetAllData();
            jobsList.Clear();

            jobsList.Add(MoveToPositionAndRotate);

            jobsDone = false;

            startingTransform = transform;
        }

        public void ResetCharge()
        {
            if (jobsList == null)
            {
                Debug.Log("jobsList is null");
                return;
            }

            if (rotVel <= 0.0f)
            {
                Debug.Log("rotationSpeed is zero. Not already charging so don't stop charging");
                return;
            }

            ResetAllData();
            jobsList.Clear();

            jobsList.Add(StartReset);
            jobsList.Add(StartRotationCorrection);
            jobsList.Add(CorrectRotation);

            jobsDone = false;

            startingTransform = transform;
        }

        void MoveToPositionAndRotate()
        {

            MoveToPosition();
            RotationCharge();
            if (jobsDone1 && jobsDone2)
            {
                timeSinceStart1 = timeSinceStart2 = 0.0f;
                jobsDone1 = jobsDone2 = false;
                jobsDone = true;
            }
        }

        private void MoveToPosition()
        {
            Debug.Log("position");
            timeSinceStart1 += Time.deltaTime;
            float t = timeSinceStart1 / movementTime;
            if (t >= 1.0f)
            {
                t = 1.0f;
                jobsDone1 = true;
            }
            transform.localPosition = Vector3.Lerp(startingTransform.localPosition, new Vector3(startingTransform.localPosition.x, laserHeight, startingTransform.localPosition.z), t);
        }

        public float rotVel;
        public float rotSpeed;
        public float rotMax;
        private void RotationCharge()
        {
            timeSinceStart2 += Time.deltaTime;
            float t = timeSinceStart2 / rotationTime;
            if (t >= 1.0f)
            {
                t = 1.0f;
                jobsDone2 = true;
            }
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.maxAngularVelocity = 1000.0f;
            rotSpeed = Mathf.SmoothStep(0.0f, rotMax, t);
            //rotVel += rotSpeed;
            //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotVel);
            //rb.angularVelocity = Vector3.Lerp(Vector3.zero, new Vector3(0.0f, 0.0f, rotationSpeed), t);
        }

        private void StartReset()
        {
            RotationStartingCharge();
            if (jobsDone2)
            {
                timeSinceStart1 = timeSinceStart2 = 0.0f;
                jobsDone = true;
            }
        }

        private void MoveToStartingPosition()
        {
            timeSinceStart1 += Time.deltaTime;
            float t = timeSinceStart1 / movementTime;
            if (t >= 1.0f)
            {
                t = 1.0f;
                jobsDone1 = true;
            }
            transform.localPosition = Vector3.Lerp(startingPosition, Vector3.zero, t);
        }

        private void RotationStartingCharge()
        {
            timeSinceStart2 += Time.deltaTime;
            float t = timeSinceStart2 / rotationTime;
            if (t >= 1.0f)
            {
                t = 1.0f;
                jobsDone2 = true;
            }
            //Rigidbody rb = GetComponent<Rigidbody>();
            rotSpeed = Mathf.SmoothStep(rotMax, 0.0f, t);
        }

        Vector3 startingPosition;
        private void StartRotationCorrection()
        {
            startingCorrectRotation = transform.rotation.eulerAngles;
            startingPosition = transform.localPosition;
            timeSinceStart1 = 0.0f;
            jobsDone = true;
        }

        Vector3 startingCorrectRotation;
        private void CorrectRotation()
        {
            MoveToStartingPosition();
            timeSinceStart1 += Time.deltaTime;
            float t = timeSinceStart1 / rotationTime;
            if (t >= 1.0f)
            {
                t = 1.0f;
                jobsDone2 = true;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.SmoothStep(startingCorrectRotation.z, 0.0f, t)));
            rotVel = transform.rotation.eulerAngles.z;
            //transform.rotation = Quaternion.Euler( Vector3.Lerp(startingCorrectRotation, Vector3.zero, t));
        }
    }
}