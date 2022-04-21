using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGrid : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] GameObject pos;
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
            if (!hit.transform.gameObject.CompareTag("Collide"))
            {
                Vector3 newPos = hit.point;
                newPos.y += 1;
                pos.transform.position = newPos;
            }
        }
    }
}