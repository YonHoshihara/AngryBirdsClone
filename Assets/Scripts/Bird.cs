using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] private float _launchForce = 400;
    [SerializeField] private float _maxDragDistance = 5;
    //[SerializeField] private ParticleSystem _particleSystem;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _startPosition;
    private int _desh=0;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_particleSystem=GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        _startPosition = _rigidbody2D.position;
        GetComponent<Rigidbody2D>().isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        desh();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());

    }
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startPosition;
        GetComponent<Rigidbody2D>().isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _desh=0;
        /*
        if(_particleSystem.isPlaying){
            _particleSystem.Stop();
        }
        */
    }
    private void OnMouseDown()
    {
        // GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
       // GetComponent<SpriteRenderer>().color = Color.red;

        spriteRenderer.color = Color.red;
    }
    private void OnMouseUp()
    {
        Vector2 currentPosition = GetComponent<Rigidbody2D>().position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(direction * _launchForce);

        /*
        spriteRenderer.color = Color.white;
        if(!_particleSystem.isPlaying){
            _particleSystem.Play();
        }
       if(!_particleSystem.isStopped){
            _particleSystem.Play();
        }
        */

    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);

        if(distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        if(desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }

        _rigidbody2D.position = desiredPosition;
    }

     private void desh(){
          if(Input.GetKey(KeyCode.Space) &&  _desh==0){
            
            // _particleSystem.Play();
            _rigidbody2D.AddForce(transform.up* 30f, ForceMode2D.Impulse);
            _rigidbody2D.AddForce(transform.right* 5f, ForceMode2D.Impulse);
            _desh=1;
         
       
        }
     }


}
