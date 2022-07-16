using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster_Random : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider;
    private int rand;
    public Sprite[] SpriteImgRand;
    public Sprite[] SpriteImgRandDie;

    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, SpriteImgRand.Length);
        _spriteRenderer.sprite = SpriteImgRand[rand];
        if (rand > 0) {
            _polygonCollider.pathCount = SpriteImgRand[rand].GetPhysicsShapeCount();
            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < _polygonCollider.pathCount; i++)
            { 
                path.Clear();
                SpriteImgRand[rand].GetPhysicsShape(i, path);
                _polygonCollider.SetPath(i, path.ToArray());
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
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
            return true;
        }
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        _spriteRenderer.sprite = SpriteImgRandDie[rand];
        _particleSystem.Play();
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }

}
