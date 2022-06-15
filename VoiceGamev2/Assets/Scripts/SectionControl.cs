using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionControl : MonoBehaviour
{
    [SerializeField] private Text nameTxt, costTxt;
    [SerializeField] private GameObject canvasObj;
    public bool isOcuped = false;

    public void EnableSection(Transform tr)
    {
        float energy = (Vector3.Distance(tr.position, transform.position) / 2);
        if (energy > ((int)energy + 0.1f) && energy < ((int)energy + 0.7f))
        {
            energy = (int)energy + 0.5f;
        }
        else if (energy > ((int)energy + 0.7f)) energy = (int)energy + 1;       //formula energia mov
        else energy = (int)energy;

        if(tr.gameObject.GetComponent<PlayerStats>().GetEnergy(1) <= energy)
        {
            transform.tag = "Untagged";
            DisableSection();
            return;
        }

        canvasObj.SetActive(true);
        nameTxt.text = transform.name;
        costTxt.text = energy.ToString();
    }

    public void DisableSection()
    {
        canvasObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        isOcuped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOcuped = false;
    }
}
