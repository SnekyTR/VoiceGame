using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CanvasFollow : MonoBehaviour
{
    private Quaternion starRotation;
    private Transform cam;
    void Start()
    {
        starRotation = transform.rotation;
        cam = GameObject.Find("Main Camera").transform;
        if (SceneManager.GetActiveScene().buildIndex != 1) transform.parent.GetComponent<EnemyStats>().canv = GetComponent<CanvasFollow>();
    }

    void LateUpdate()
    {
        Vector3 direction = cam.transform.position - transform.position;

        Quaternion rt = Quaternion.LookRotation(direction);
        rt.x = transform.rotation.x;
        rt.z = transform.rotation.z;
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            transform.rotation = cam.rotation;
            ScaleCanvas();
        }
        else transform.rotation = starRotation;
    }

    private void ScaleCanvas()
    {
        float dis = Vector3.Distance(cam.position, transform.position);

        transform.localScale = new Vector3((2.2f + dis*0.14f), (2.2f + dis * 0.14f), 1);
    }

    public void EnemyB(bool t)
    {
        if (t) transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        else if (!t) transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }

    public void EnemyC(bool t)
    {
        if (t) transform.GetChild(0).gameObject.SetActive(true);
        else if (!t) transform.GetChild(0).gameObject.SetActive(false);
    }
}
