using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActivation : MonoBehaviour
{
    public GameObject[] rays;
    public Transform[] casillas;
    public string[] nameC;
    public PlayerMove playerM;

    private void Awake()
    {
        for(int i = 0; i < casillas.Length ; i++)
        {
            nameC[i] = casillas[i].name;
        }
        playerM.SetList(nameC);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EnableGrid(Transform tr)
    {
        transform.position = new Vector3(tr.position.x, transform.position.y, tr.position.z);
        for(int i = 0; i < rays.Length; i++)
        {
            rays[i].SetActive(true);
        }
    }

    public void DisableGrid()
    {
        for (int i = 0; i < rays.Length; i++)
        {
            rays[i].SetActive(false);
        }
    }
}
