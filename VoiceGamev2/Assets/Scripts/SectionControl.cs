using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionControl : MonoBehaviour
{
    [SerializeField] private Text nameTxt, costTxt;
    [SerializeField] private GameObject canvasObj;
    public bool isOcuped = false;
    private Image sectImg;
    private Color originColor;

    private MultipleTargetCamera cam;

    private void Awake()
    {
        sectImg = canvasObj.transform.GetChild(2).GetComponent<Image>();
        originColor = sectImg.color;
        cam = GameObject.Find("Main Camera").GetComponent<MultipleTargetCamera>();
    }

    public void EnableSection(Transform tr)
    {
        float energy = (Vector3.Distance(tr.position, transform.position) / 2);

        if (energy > ((int)energy + 0.1f) && energy < ((int)energy + 0.7f))
        {
            energy = (int)energy + 0.5f;
        }
        else if (energy > ((int)energy + 0.7f)) energy = (int)energy + 1;       //formula energia mov
        else energy = (int)energy;

        if (tr.gameObject.GetComponent<PlayerStats>().GetEnergy(1) < energy || isOcuped)
        {
            DisableSection();
            return;
        }

        StartCoroutine(anim(energy, tr));
    }

    public void DisableSection()
    {
        canvasObj.SetActive(false);
        transform.tag = "Untagged";
    }

    private void OnTriggerEnter(Collider other)
    {
        isOcuped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOcuped = false;
    }

    private IEnumerator anim(float n, Transform tr)
    {
        transform.tag = "Section";

        float m = n/4;
        yield return new WaitForSeconds(m);

        string[] names = transform.name.Split(' ');

        cam.SectionAdd(transform);
        canvasObj.SetActive(true);
        nameTxt.text = transform.name;
        costTxt.text = n.ToString();
    }

    public void AtkShowRange(Color c, bool t)
    {
        if (t)
        {
            canvasObj.SetActive(true);
            sectImg.color = c;
            canvasObj.transform.GetChild(0).gameObject.SetActive(false);
            canvasObj.transform.GetChild(1).gameObject.SetActive(false);
            canvasObj.transform.GetChild(3).gameObject.SetActive(false);

            cam.SectionAdd(transform);
        }
        else
        {
            sectImg.color = originColor;
            canvasObj.transform.GetChild(0).gameObject.SetActive(true);
            canvasObj.transform.GetChild(1).gameObject.SetActive(true);
            canvasObj.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void ShowRange()
    {
        canvasObj.SetActive(true);
        sectImg.color = originColor;
        canvasObj.transform.GetChild(0).gameObject.SetActive(true);
        canvasObj.transform.GetChild(1).gameObject.SetActive(true);
        canvasObj.transform.GetChild(3).gameObject.SetActive(true);
    }
}
