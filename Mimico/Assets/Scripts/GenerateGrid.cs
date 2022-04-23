using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGrid : MonoBehaviour
{
    RaycastHit hit;
    private PlayerMove playerM;

    void Start()
    {
        playerM = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void LateUpdate()
    {
        
    }
    private void OnEnable()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                hit.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
                hit.transform.tag = "Section";
            }
        }
    }

    private void OnDisable()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                hit.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
                hit.transform.tag = "Untagged";
            }
        }
    }
}