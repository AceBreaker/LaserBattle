using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class GameBoard : MonoBehaviour
    {
        public float spacer;
        public GameObject gameSpace;
        public Vector2 boardSize;
        public Material color;
        public List<SpecialInstructions> instructions;

        public GameObject spacerObject;

        [System.Serializable]
        public struct SpecialInstructions
        {
            public int columnIndex;
            public int rowIndex;
            public Color materialColor;
            public MonoBehaviour restricted;
            public GameObject decoration;
            public Sprite sprite;
            public Color spriteColor;
        }
    }

    
}