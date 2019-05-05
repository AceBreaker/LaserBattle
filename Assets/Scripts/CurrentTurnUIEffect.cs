using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserBattle
{
    public class CurrentTurnUIEffect : MonoBehaviour {

        [SerializeField] UnityEngine.UI.Outline outline1;
        [SerializeField] UnityEngine.UI.Outline outline2;
        [SerializeField] UnityEngine.UI.Outline outline3;

        [SerializeField] Color redColor;
        [SerializeField] Color blueColor;

        public void ChangeToRed()
        {
            ChangeColor(PlayerNumbers.ONE);
        }

        public void ChangeToBlue()
        {
            ChangeColor(PlayerNumbers.TWO);
        }

        public void ChangeColor(PlayerNumbers player)
        {
            switch (player)
            {
                case PlayerNumbers.ONE:
                    {
                        outline1.effectColor = outline3.effectColor = redColor;
                        Color blah = new Color(redColor.r, redColor.g, redColor.b, 0.062f);
                        Debug.Log(blah.r.ToString() +" "+ blah.g.ToString() + " " + blah.b.ToString() + " " + blah.a.ToString());
                        outline2.effectColor = blah;
                        break;
                    }
                case PlayerNumbers.TWO:
                    {
                        outline1.effectColor = outline3.effectColor = blueColor;
                        Color blah = new Color(blueColor.r, blueColor.g, blueColor.b, 0.062f);
                        Debug.Log(blah.r.ToString() + " " + blah.g.ToString() + " " + blah.b.ToString() + " " + blah.a.ToString());
                        outline2.effectColor = blah;
                        break;
                    }
                case PlayerNumbers.THREE:
                    {
                        Debug.LogError("Three players not currently supported");
                        break;
                    }
                case PlayerNumbers.FOUR:
                    {
                        Debug.LogError("Four players not currently supported");
                        break;
                    }
                default:
                    break;
            }
        }
    }
}