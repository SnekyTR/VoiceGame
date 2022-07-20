using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Vector3 player;
    [SerializeField] private Vector3 enemy;

    public Transform target;

    float maxViewpowertDistance = 1;
    float viewpowertDistance;
    // Start is called before the first frame update
    void Start()
    {
        //enemy = CalculateEnemyPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private Vector3 CalculateEnemyPos()
    {

    }*/
}
