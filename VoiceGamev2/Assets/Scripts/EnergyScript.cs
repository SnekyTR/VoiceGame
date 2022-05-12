using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    [SerializeField] private Image[] rombos;
    [SerializeField] private Sprite[] icons;

    public void NewEnergyIcon(float e)
    {
        for(float i = 0.5f; i < 10; i += 0.5f)
        {
            if (i == e && e > (int)e)
            {
                rombos[(int)(i)].sprite = icons[1];
            }
            else if(i == e)
            {
                rombos[(int)(i)].sprite = icons[0];
            }
            else if (i > e)
            {
                rombos[(int)(i)].sprite = icons[2];
            }
            else if(i < e)
            {
                rombos[(int)(i)].sprite = icons[0];
            }
        }
    }
}
