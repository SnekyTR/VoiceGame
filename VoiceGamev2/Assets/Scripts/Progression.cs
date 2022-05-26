using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    private GameObject combat1;
    private GameObject combat2;
    private GameObject combat3;
    private GameObject combat4;
    private GameObject combat5;
    private GameObject combat6;
    private GameObject combat7;
    private GameObject combat8;
    public int progression;
    private void Start()
    {
        combat1 = GameObject.Find("Combat1");
        combat2 = GameObject.Find("Combat2");
        combat3 = GameObject.Find("Combat3");
        combat4 = GameObject.Find("Combat4");
        combat5 = GameObject.Find("Combat5");
        combat6 = GameObject.Find("Combat6");
        combat7 = GameObject.Find("Combat7");
        combat8 = GameObject.Find("Combat8");
        CheckProgression();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void IncrementProgresion(int num)
    {
        progression += num;
        CheckProgression();
    }
    private void CheckProgression()
    {
        if (progression >= 1)
        {
            combat1.SetActive(false);
            if(progression >= 2)
            {
                combat2.SetActive(false);
                if (progression >= 3)
                {
                    combat3.SetActive(false);
                    if (progression >= 4)
                    {
                        combat4.SetActive(false);
                        if (progression >= 5)
                        {
                            combat5.SetActive(false);
                            if (progression >= 6)
                            {
                                combat6.SetActive(false);
                                if (progression >= 7)
                                {
                                    combat7.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
