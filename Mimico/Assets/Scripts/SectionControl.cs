using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionControl : MonoBehaviour
{
    [SerializeField] private Text nameTxt, costTxt;
    [SerializeField] private GameObject canvasObj;

    public void EnableSection(Transform tr)
    {
        canvasObj.SetActive(true);
        nameTxt.text = transform.name;
        costTxt.text = ((int)(Vector3.Distance(tr.position, transform.position)/2)).ToString() + " E";         //formula energia mov
    }

    public void DisableSection()
    {
        canvasObj.SetActive(false);
    }
}
