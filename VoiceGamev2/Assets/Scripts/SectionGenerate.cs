using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerate : MonoBehaviour
{
    public GameObject inst;
    public int colum;
    private Vector3 newPos;

    void Start()
    {
        newPos = transform.position;
        for(int i = 0; i < colum; i++)
        {
            newPos.x -= 2;

            GameObject newPiece = Instantiate(inst, newPos, transform.rotation);
            newPiece.name = transform.name + " " + (i + 1f).ToString();
            newPiece.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
