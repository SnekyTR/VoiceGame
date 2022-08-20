using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{

    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;
        private CameraFollow gameM;
        private PlayerMove plyM;
        private PlayerStats plyStats;
        int dmg;
        int crit;

        public void Start()
        {
            gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
            plyM = gameM.GetComponent<PlayerMove>();
            plyStats = gameM.playerParent.GetComponent<PlayerStats>();
        }

        public void OnCollisionEnter(Collision col)
        {
            CollisionHandler.HandleCollision(gameObject, col);
            if (col.transform.tag != "Enemy") return;
            if (col.gameObject.GetComponent<EnemyStats>().inmunity) return;

            if (plyM.GetAtkState() == "Lluvia de meteoritos")
            {
                dmg = Random.Range((int)(plyStats.GetIntellect() * 0.4f), (int)(plyStats.GetIntellect() * 0.6f));
            }
            else if(plyM.GetAtkState() == "atk")
            {
                dmg = Random.Range((int)(plyStats.GetIntellect() * 1f), (int)(plyStats.GetIntellect() * 2f));
            }
            else if (plyM.GetAtkState() == "Bola de fuego")
            {
                dmg = Random.Range((int)(plyStats.GetIntellect() * 1.3f), (int)(plyStats.GetIntellect() * 1.9f));
            }
            else if(plyM.GetAtkState() == "Lluvia de flechas")
            {
                dmg = Random.Range((int)(plyStats.GetAgility() * 0.2f), (int)(plyStats.GetAgility() * 0.4f));
            }

            crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            if (col.transform.tag == "Enemy") col.gameObject.GetComponent<EnemyStats>().SetLife(-dmg);

            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
