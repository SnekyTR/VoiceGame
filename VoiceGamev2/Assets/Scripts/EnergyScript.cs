using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyScript : MonoBehaviour
{
    [SerializeField] private Image[] rombos;
    [SerializeField] private Image[] rombos2;
    [SerializeField] private Sprite[] icons;

    public void NewEnergyIcon(float e)
    {
        for(float i = 0.5f; i < 5; i += 0.5f)
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

    public void NewEnergyActionsIcon(float e)
    {
        print(e);

        for (float i = 0.5f; i < 5; i += 0.5f)
        {
            if (i == e && e > (int)e)
            {
                rombos2[(int)(i)].sprite = icons[1];
            }
            else if (i == e)
            {
                rombos2[(int)(i)].sprite = icons[0];
            }
            else if (i > e)
            {
                rombos2[(int)(i)].sprite = icons[2];
            }
            else if (i < e)
            {
                rombos2[(int)(i)].sprite = icons[0];
            }
        }
    }
}
