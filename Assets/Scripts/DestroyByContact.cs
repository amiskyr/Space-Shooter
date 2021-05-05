using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    public GameObject playerExplosion;

    private void Start()
    {
        playerExplosion = (GameObject)GameObject.Find("CartoonBlast_Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Bullet"))
        {
            if(health < 0)
            {
                explosion.transform.position = transform.position;
                explosion.GetComponent<ParticleSystem>().Play();
                bool vfxComplete = explosion.GetComponent<ParticleSystem>().isStopped;
                other.gameObject.SetActive(false);
                if(vfxComplete)
                    gameObject.SetActive(false);
            }
            else
            {
                health -= 1;
            }
        }
        if(other.gameObject.tag.Equals("Player"))
        {
            playerExplosion.transform.position = other.transform.position;
            playerExplosion.GetComponent<ParticleSystem>().Play();
            bool vfx1Complete = playerExplosion.GetComponent<ParticleSystem>().isStopped;
            if(vfx1Complete)
                other.gameObject.SetActive(false);

            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            bool vfx2Complete = explosion.GetComponent<ParticleSystem>().isStopped;
            if(vfx2Complete)
                gameObject.SetActive(false);

        }
    }
}
