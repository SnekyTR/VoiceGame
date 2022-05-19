using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnter : MonoBehaviour
{
    [SerializeField] private SingleCombatChanger single;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform player;
    private void Update()
    {
        if (Vector3.Distance(player.position, gameObject.transform.position) < 8)
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            print("Ha entrado");
            panel.SetActive(true);
            single.SelectLvl();
        }
    }
}
