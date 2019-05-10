using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class CalculateMoveTargets : MonoBehaviour {

        [SerializeField] GameObject[] moveIndicators;
        [SerializeField] GameObject myParent;
        [SerializeField] GameObject unitsContainer;

        [SerializeField] Color[] playerColors;

        private void Awake()
        {
            playerColors = new Color[2];
            playerColors[0] = Color.red;
            playerColors[1] = Color.cyan;

            unitsContainer = transform.root.gameObject;

            foreach (GameObject go in moveIndicators)
            {
                go.GetComponent<Light>().color = playerColors[(int)(myParent.GetComponent<MoveableUnit>().GetOwner())];
            }
        }

        public void PieceSelected(GameObject go)
        {
            if (go == myParent)
            {
                FindValidMoveTargets();
            }
        }

        private void FindValidMoveTargets()
        {
            foreach (GameObject go in moveIndicators)
            {
                if (go.transform.position.x + 0.1f >= GameSettings.boardWidth * GameSettings.spaceBetweenTiles ||
                    go.transform.position.x < 0.0f ||
                    go.transform.position.y + 0.1f >= GameSettings.boardHeight * GameSettings.spaceBetweenTiles ||
                    go.transform.position.y < 0.0f)
                {
                    continue;
                }
                bool toContinue = false;
                if (go.name == "MoveTarget (7)")
                    Debug.Log(go.transform.position * -1);
                foreach (Transform child in unitsContainer.transform)
                {
                    if (child.gameObject.name == "Mirror (7)")
                        Debug.Log(child.position);
                    if (Mathf.Approximately(child.position.x, go.transform.position.x) && Mathf.Approximately(child.position.z, go.transform.position.z))
                    {
                        Debug.Log("moving right along here");
                        toContinue = true;
                        break;
                    }
                    if (Mathf.Approximately(child.position.x, 2.2f) && Mathf.Approximately(child.position.z, 6.6f))
                    {
                        Debug.Log("right here");
                    }
                }
                if (toContinue)
                    continue;
                go.SetActive(true);
            }
        }

        public void PieceUnselected(GameObject go)
        {
            if (go == myParent)
            {
                UnhighlightMoveTargets();
            }
        }

        private void UnhighlightMoveTargets()
        {
            foreach (GameObject go in moveIndicators)
            {
                go.SetActive(false);
            }
        }
    }
}