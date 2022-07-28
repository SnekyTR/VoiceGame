using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridActivation : MonoBehaviour
{
    public List<Transform> casillas = new List<Transform>();
    public string[] nameC;
    private PlayerMove playerM;

    public Color red;

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

        for (int i = 0; i < casillas.Count; i++)
        {
            casillas[i].tag = "Untagged";
        }
    }

    public void EnableGrid(Transform tr)
    {
        for (int i = 0; i < casillas.Count; i++)
        {
            float d = Vector3.Distance(tr.position, casillas[i].position);
            if (d <= 10.2f && d >= 1)
            {
                casillas[i].GetComponent<SectionControl>().ShowRange();
                casillas[i].GetComponent<SectionControl>().EnableSection(tr);
            }
        }
    }

    public void DisableGrid()
    {
        for (int i = 0; i < casillas.Count; i++)
        {
            casillas[i].GetComponent<SectionControl>().DisableSection();
            casillas[i].GetComponent<SectionControl>().AtkShowRange(red, false);
        }
    }

    public void EnableAtkGrid(Transform tr, float e)
    {
        for(int i = 0; i < casillas.Count; i++)
        {
            float d = Vector3.Distance(tr.position, casillas[i].position);
            if (d <= e && d >= 1)
            {
                casillas[i].GetComponent<SectionControl>().AtkShowRange(red, true);
            }
        }
    }

    public void DisableAtkGrid()
    {
        for (int i = 0; i < casillas.Count; i++)
        {
            casillas[i].GetComponent<SectionControl>().DisableSection();
            casillas[i].GetComponent<SectionControl>().AtkShowRange(red, false);
        }
    }
}
