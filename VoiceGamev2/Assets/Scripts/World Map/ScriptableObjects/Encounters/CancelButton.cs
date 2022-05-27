using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    // Start is called before the first frame update

    public void CancelBattle()
    {
        panel.SetActive(false);
    }
}
