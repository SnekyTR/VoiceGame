using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActivation : MonoBehaviour
{
    public GameObject[] rays;
    public List<Transform> casillas = new List<Transform>();
    public string[] nameC;
    private PlayerMove playerM;

    private void Awake()
    {
        GameObject[] casO = GameObject.FindGameObjectsWithTag("Section");

        for(int i = 0; i < casO.Length; i++)
        {
            casillas.Add(casO[i].transform);
        }

        nameC = new string[casillas.Count];

        for(int i = 0; i < casillas.Count ; i++)
        {
            nameC[i] = casillas[i].name;
        }
        playerM = GameObject.Find("GameManager").GetComponent<PlayerMove>();
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
