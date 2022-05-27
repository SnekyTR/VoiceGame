using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUIMovement : MonoBehaviour
{
    [SerializeField] private GameObject partyInf;
    [SerializeField] private GameObject characterInf;
    public void ClosePartyInf()
    {
        partyInf.SetActive(false);
    }
    public void OpenPartyInf()
    {
        partyInf.SetActive(true);
    }
    public void CloseCharInf()
    {
        characterInf.SetActive(false);
    }
}
