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
            if(GUILayout.Button("Create Board"))
            {
                for(int i = 0; i < board.boardSize.x; i++)
                {
                    for (int j = 0; j < board.boardSize.y; j++)
                    {
                        Vector3 v = new Vector3(i * 1 + board.spacer * i, 0.0f, j * 1 + board.spacer * j);
                        GameObject go = Instantiate(board.gameSpace, v, Quaternion.identity, board.transform);
                        go.GetComponent<Renderer>().material = board.colors[materialIndex];
                        AdjustMaterialIndex();
                    }
                    AdjustMaterialIndex();
                }
                
            }
        }

        private void AdjustMaterialIndex()
        {
            materialIndex++;
            if (materialIndex > 1)
                materialIndex = 0;
        }
    }
}