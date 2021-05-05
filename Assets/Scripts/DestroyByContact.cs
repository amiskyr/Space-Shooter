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
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(0);
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
            other.gameObject.SetActive(false);

            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(0);

        }
    }
}
