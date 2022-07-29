using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float m_ExplosionRadius;
    
    [SerializeField]
    private float m_ExplosionForce;

    [SerializeField]
    private float m_DelayToExplode;

    [SerializeField]
    private Sprite m_ExplosionSprite;
    
    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    

    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(CallExplosion());
        }
    }

    private void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);

        foreach (Collider2D obj in objects)
        {
            if (obj.gameObject.tag == "Box" )
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

    IEnumerator CallExplosion()
    {
        m_SpriteRenderer.sprite = m_ExplosionSprite;
        yield return new  WaitForSeconds(m_DelayToExplode);
        Explode();
        m_ParticleSystem.Play();
        m_SpriteRenderer.sprite = null;
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
        
    }

}
