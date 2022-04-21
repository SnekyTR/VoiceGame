using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActivation : MonoBehaviour
{
    public GameObject[] casillas;
    public string[] nameC;
    public PlayerMove playerM;

    private void Awake()
    {
        playerM.SetList(casillas, nameC);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableGrid()
    {
        transform.position = new Vector3(playerM.transform.position.x, transform.position.y, playerM.transform.position.z);
        for(int i = 0; i < casillas.Length; i++)
        {
            casillas[i].SetActive(true);
        }
    }

    public void DisableGrid()
    {
        for (int i = 0; i < casillas.Length; i++)
        {
            casillas[i].SetActive(false);
        }
    }
}
