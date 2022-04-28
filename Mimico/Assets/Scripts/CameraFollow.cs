using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerParent;
    
    void Start()
    {
        
    }
    
    void LateUpdate()
    {
        if(playerParent != null)
        {
            Vector3 newPos = new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z);
            transform.position = newPos;
        }
    }

    public void NewParent(Transform tr)
    {
        playerParent = tr;
    }
}
