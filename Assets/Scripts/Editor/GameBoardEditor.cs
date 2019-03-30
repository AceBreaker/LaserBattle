using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace LaserBattle
{
    [CustomEditor(typeof(GameBoard))]
    public class GameBoardEditor : Editor
    {
        GameBoard board;

        private void OnEnable()
        {
            board = target as GameBoard;
        }

        int materialIndex = 0;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create Board"))
            {
                for (int i = 0; i < board.boardSize.x; i++)
                {
                    for (int j = 0; j < board.boardSize.y; j++)
                    {
                        Vector3 v = new Vector3(i * 1 + board.spacer * i, 0.0f, j * 1 + board.spacer * j);
                        GameObject go = Instantiate(board.gameSpace, v, Quaternion.identity, board.transform);
                        go.GetComponent<Renderer>().material = board.color;

                        CheckForSpecialInstructions(go, i, j);
                    }

                }

                CreateSpacers();

            }
            else if(GUILayout.Button("Delete Board"))
            {
                var tempArray = new GameObject[board.transform.childCount];

                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = board.transform.GetChild(i).gameObject;
                }

                foreach (var child in tempArray)
                {
                    DestroyImmediate(child);
                }
            }
        }

        private void AdjustMaterialIndex()
        {
            materialIndex++;
            if (materialIndex > 1)
                materialIndex = 0;
        }

        private void CheckForSpecialInstructions(GameObject go, int x, int y)
        {
            int count = board.instructions.Count;
            for (int i = 0; i < board.instructions.Count; ++i)
            {
                if (x == board.instructions[i].columnIndex && y == board.instructions[i].rowIndex)
                {
                    try
                    {
                        go.AddComponent<RestrictedSpace>();
                        //go.GetComponent<Renderer>().sharedMaterial.color = board.instructions[i].materialColor;
                        GameObject child = Instantiate(board.instructions[i].decoration, go.transform);
                        child.GetComponent<SpriteRenderer>().sprite = board.instructions[i].sprite;
                        child.GetComponent<SpriteRenderer>().color = board.instructions[i].spriteColor;
                        child.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                        child.transform.localPosition = new Vector3(0.0f, 0.501f, 0.0f);
                    }
                    catch(Exception e)
                    {
                        break;
                    }
                    break;
                }
            }
        }

        public void CreateSpacers()
        {
            int spacerColumnCount = (int)(board.boardSize.x - 1);
            int spacerRowCount = (int)(board.boardSize.y - 1);

            for (int i = -1; i < spacerColumnCount+1; ++i)
            {
                GameObject spacerObject = Instantiate(board.spacerObject, board.transform);

                spacerObject.transform.localPosition = new Vector3(0.5f+ (1.0f + board.spacer) * i + board.spacer*0.5f, 0.0f, (1.0f + board.spacer) * (spacerRowCount * 0.5f));
                spacerObject.transform.localScale = new Vector3(board.spacer, 1.0f + board.spacer, 1.0f + (1.0f + board.spacer) * spacerRowCount);
            }
            for (int j = -1; j < spacerRowCount+1; ++j)
            {
                GameObject spacerObject = Instantiate(board.spacerObject, board.transform);

                spacerObject.transform.localPosition = new Vector3((1.0f + board.spacer) * (spacerColumnCount * 0.5f), 0.0f, 0.5f + (1.0f + board.spacer) * j + board.spacer * 0.5f);
                spacerObject.transform.localScale = new Vector3(1.0f + (1.0f + board.spacer) * spacerColumnCount, 1.0f + board.spacer,    board.spacer);
            }
        }
    }
}