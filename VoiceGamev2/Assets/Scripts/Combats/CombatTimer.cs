using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTimer : MonoBehaviour
{
    public Text timerTxt;
    public Image filledImg;
    public float maxTime;

    public bool isPlaying = false;
    private float timer;
    private WinLoose wl;

    void Start()
    {
        isPlaying = true;
        timer = maxTime;
        wl = GameObject.Find("GameManager").GetComponent<WinLoose>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isPlaying)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                isPlaying = false;
                wl.OverTime();
                return;
            }

            string min = ((int)timer / 60).ToString();
            string sec = ((int)(timer % 60)).ToString();

            timerTxt.text = min + ":" + sec;

            filledImg.fillAmount = (timer / maxTime);
        }
    }
}
