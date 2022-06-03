using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public void StarIA()
    {
        if (GetComponent<SpiderAI>())
        {
            GetComponent<SpiderAI>().StarIA();
        }
        else if (GetComponent<SkeletonArcherAI>())
        {
            print("sadfadsf");
            GetComponent<SkeletonArcherAI>().StarIA();
        }
        else if (GetComponent<SkeletonGuardianAI>())
        {
            GetComponent<SkeletonGuardianAI>().StarIA();
        }
    }
}
