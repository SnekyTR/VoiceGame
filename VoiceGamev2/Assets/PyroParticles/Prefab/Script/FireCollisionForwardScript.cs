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
        private PlayerStats plyStats;

        public void Start()
        {
            gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
            plyStats = gameM.playerParent.GetComponent<PlayerStats>();
        }

        public void OnCollisionEnter(Collision col)
        {
            CollisionHandler.HandleCollision(gameObject, col);

            int dmg = Random.Range((int)(plyStats.GetIntellect() * 0.3f), (int)(plyStats.GetIntellect() * 0.5f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            if (col.transform.tag == "Enemy") col.gameObject.GetComponent<EnemyStats>().SetLife(-dmg);
        }
    }
}
