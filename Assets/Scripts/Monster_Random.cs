using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster_Random : MonoBehaviour
{

    [SerializeField] private float _enemyDamage;
    bool _hasDied = false;
    public Animator monsterAnim;
    public SpriteRenderer spriteMonster;

    void Awake()
    {
        spriteMonster = GetComponent<SpriteRenderer>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            _enemyDamage -= 1;
            //MonsterAnimator("MonsterAnimHit");

            if (_enemyDamage == 0)
            {
                StartCoroutine(Die());
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

        if (collision.contacts[0].normal.y < -0.5)
        {
            StartCoroutine(waitColor());
            return true;
        }
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        MonsterAnimator("MonsterAnimDie");
        SoundController.Instance.PlaySound(2);
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }

    void MonsterAnimator(string aMonster)
    {
        monsterAnim.Play(aMonster);
    }

    public void Kill()
    {
        _enemyDamage = 0;
        StartCoroutine(Die());
    }
    IEnumerator waitColor()
    {
        spriteMonster.color = Color.red;
        yield return new WaitForSeconds(2);
        spriteMonster.color = Color.white;
    }

}
