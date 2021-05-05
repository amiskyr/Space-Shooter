using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int health;
    public ParticleSystem explosion;
    public ParticleSystem playerExplosion;

    private void Start()
    {        
        explosion = GameObject.Find("CartoonBlast_Medium").GetComponent<ParticleSystem>();
        playerExplosion = GameObject.Find("CartoonBlast_Player").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Bullet"))
        {
            if(health < 0)
            {
                explosion.transform.position = transform.position;
                explosion.Play();
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
            playerExplosion.Play();
            other.gameObject.SetActive(false);

            explosion.transform.position = transform.position;
            explosion.Play();
            gameObject.SetActive(false);
            
            FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlayAudio(0);

        }
    }
}
