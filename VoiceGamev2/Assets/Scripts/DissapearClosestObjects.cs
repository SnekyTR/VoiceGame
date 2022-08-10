using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearClosestObjects : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ocultable"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ocultable"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
