using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool pause = false;
   
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject darkPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pause)
        {
            //text.text = "No esta paused";
            //GUI.Label(new Rect(100, 100, 50, 30), "Game Paused");
            Time.timeScale = 1;
        }
        else
        {
            darkPanel.SetActive(true);
            Time.timeScale = 0;
            //text.text = "Game Paused";
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        pause = focus;
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        pause = pauseStatus;
    }
    public void DeactivatePause()
    {
        darkPanel.SetActive(false);
    }
}
