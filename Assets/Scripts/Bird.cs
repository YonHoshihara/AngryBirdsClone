using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{

    [SerializeField] 
    private float _launchForce = 1000;
    
    [SerializeField] 
    private float _maxDragDistance = 50;
    

    private Animator _animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _startPosition;
    private int _desh=0;
    private bool _movement=false;
    private bool _isReseting = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.isKinematic = true;
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        Desh();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isReseting)
        {
            SoundController.Instance.PlaySound(3);
        }
        StartCoroutine(ResetAfterDelay());
      
    }
    IEnumerator ResetAfterDelay()
    {
        //Se o objeto player estiver parado , ele poderá realizar a sua tentativa.
         if(_movement==false){
            //Life();
            _movement=true;
        }
        _isReseting = true;
        yield return new WaitForSeconds(3);
        _animator.SetBool("Shoot", false);
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _desh=0;
        _movement=false;
        _isReseting = false;

    }
    private void OnMouseDown()
    {
        SoundController.Instance.PlaySound(1);
        spriteRenderer.color = Color.red;
    }
    private void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _animator.SetBool("Shoot",true);
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);
        LifeController.Instance.Life();
        spriteRenderer.color = Color.white;
        SoundController.Instance.PlaySound(0);

    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

     private void Desh(){
          if(Input.GetKey(KeyCode.Space) &&  _desh==0){
            _rigidbody2D.AddForce(transform.up* 30f, ForceMode2D.Impulse);
            _rigidbody2D.AddForce(transform.right* 5f, ForceMode2D.Impulse);
            _desh=1;
            SoundController.Instance.PlaySound(0);
        }
     }
}
