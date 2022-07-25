using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss : MonoBehaviour
{

    [SerializeField] public float _bossDamage;
    bool _hasDied = false;

    public GameObject health;
    public Animator bossAnim;

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindWithTag("HealthBarCanvas");
        health.GetComponent<HealthController>().healthMax = _bossDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            if (collision.gameObject.tag != "Player")
            {
                _bossDamage -= 1;
                BossAnimator("BossAnimHit");
                health.GetComponent<HealthController>().UpdateHealthBar(_bossDamage);
                //print("Dano: " + _bossDamage);

                if (_bossDamage <= 0)
                {
                    StartCoroutine(Die());
                }
            }
        }
        

    }

    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
        {
            return false;
        }


        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            return true;
        }

        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        BossAnimator("BossAnimDie");
        SoundController.Instance.PlaySound(2);
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
    
    void BossAnimator(string aBoss)
    {
        bossAnim.Play(aBoss);
    }
}
