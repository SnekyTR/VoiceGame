using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGrid : MonoBehaviour
{
    RaycastHit hit;
    private PlayerMove playerM;
    private Transform playerS;

    private void OnEnable()
    {
        playerM = GameObject.Find("GameManager").GetComponent<PlayerMove>();
        playerS = playerM.GetComponent<CameraFollow>().playerParent;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                hit.transform.GetComponent<SectionControl>().EnableSection(playerS);
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
                hit.transform.GetComponent<SectionControl>().DisableSection();
                hit.transform.tag = "Untagged";
            }
        }
    }
}