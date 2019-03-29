using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class ChangeTurnText : MonoBehaviour
    {
        UnityEngine.UI.Text turnText;

        public void ChangeText()
        {
            if (turnText.text == "Blue Turn")
                turnText.text = "Red Turn";
            else if (turnText.text == "Red Turn")
                turnText.text = "Blue Turn";
        }
    }
}