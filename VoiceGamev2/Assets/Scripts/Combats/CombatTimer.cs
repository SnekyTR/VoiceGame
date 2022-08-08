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
    public GameObject blue, red;
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

            if(int.Parse(sec) < 10)
            {
                sec = "0" + sec;
            }

            timerTxt.text = min + ":" + sec;

            filledImg.fillAmount = (timer / maxTime);
        }
    }

    public void NoEnergyMove()
    {
        StartCoroutine(Blue("Energía insuficiente"));
    }

    public void NoEnergyAction()
    {
        StartCoroutine(Red("Energía insuficiente"));
    }

    public void SectionIsFar()
    {
        StartCoroutine(Blue("Casilla inválida"));
    }

    public void EnemyFar()
    {
        StartCoroutine(Red("Enemigo inválido"));
    }

    private IEnumerator Red(string e)
    {
        red.SetActive(true);
        red.transform.GetChild(0).GetComponent<Text>().text = e;

        yield return new WaitForSeconds(2f);

        red.SetActive(false);
    }

    private IEnumerator Blue(string e)
    {
        blue.SetActive(true);
        blue.transform.GetChild(0).GetComponent<Text>().text = e;

        yield return new WaitForSeconds(2f);

        blue.SetActive(false);
    }
}
