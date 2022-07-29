using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
   [SerializeField] 
   private ParticleSystem _eggExplosin;
   
   [SerializeField]
   private SpriteRenderer _eggSprintRenderer;
   
   [SerializeField]  
   private float m_ExplosionRadius;


   [SerializeField]
   private float m_ExplosionForce;


    private void OnCollisionEnter2D(Collision2D other) {

    if(!_eggExplosin.isPlaying && other.gameObject.tag!="Player"){
           _eggExplosin.Play();
     _eggSprintRenderer.enabled=false;
            Explode();
    Invoke("reset",3);
     
    }
     
    }
    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);

        foreach (Collider2D obj in objects)
        {
            if (obj.gameObject.tag == "Box")
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * m_ExplosionForce);
            }

            if (obj.gameObject.tag == "Enemy")
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * m_ExplosionForce);
                obj.GetComponent<Monster_Random>().Kill();
            }
        }

        SoundController.Instance.PlaySound(4);
    }
    
}
