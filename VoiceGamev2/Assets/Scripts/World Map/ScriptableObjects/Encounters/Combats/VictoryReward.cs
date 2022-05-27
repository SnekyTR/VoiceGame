using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryReward : MonoBehaviour
{
    [SerializeField] private GameObject vPanel;
    [SerializeField] private GameObject lPanel;
    [SerializeField] private List<GameObject> games = new List<GameObject>();
    private float totalXP;

    // Start is called before the first frame update
    private void Awake()
    {
        CalculateXP();
    }
    void Start()
    {
        
    }
    private void CalculateXP()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            games.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);

            totalXP += games[i].GetComponent<EnemyStats>().xp;
        }
    }
    public void ActivateWinCondition()
    {
        vPanel.SetActive(true);
    }
    public void ActivateLooseCondition()
    {
        lPanel.SetActive(true);
    }
    public void RemoveFromList(GameObject enemy)
    {
        for(int i = 1; i < games.Count; i++)
        {
            if (games.Contains(enemy))
            {
                games.Remove(enemy);
            }
            print(games[i]);
        }
    }
}