using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg : MonoBehaviour
{
    public Transform target;
    public int dmg;
    bool magic;

    public GameObject blood;

    private void OnEnable()
    {
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
        else if (GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().isTrigger = true;
            magic = true;
        }
    }

    private void LateUpdate()
    {
        if (magic)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            other.gameObject.GetComponent<PlayerStats>().SetLife(-dmg);

            Vector3 pos = other.transform.position;
            pos.y += 1;

            Destroy(Instantiate(blood, pos, transform.rotation), 1.5f);

            Destroy(this.gameObject);
        }
    }
}
