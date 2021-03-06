using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BlueBird : MonoBehaviour
{

    [SerializeField] private float _launchForce = 400;
    [SerializeField] private float _maxDragDistance = 5;
    [SerializeField] private Rigidbody2D _eggRigidBody;
    [SerializeField] private SpriteRenderer _eggSpriteRederer;
    [SerializeField] private Image uiBird1;
    [SerializeField] private Image uiBird2;
    [SerializeField] private Image uiBird3;



    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _startPosition;
    private int _desh=0;
    private int _life=3;
    private bool _movement=false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
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
        Desh();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
        


    }
    IEnumerator ResetAfterDelay()
    {

          //Se o objeto player estiver parado , ele poderá realizar a sua tentativa.
         if(_movement==false){
            Life();
            _movement=true;
        }
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = _startPosition;
        GetComponent<Rigidbody2D>().isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _desh=0;
        EggReset();
         _movement=false;
      
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

        spriteRenderer.color = Color.white;
   

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

     private void Desh(){
          if(Input.GetKey(KeyCode.Space) &&  _desh==0 && _rigidbody2D.isKinematic==false){
            _rigidbody2D.AddForce(transform.up* 30f, ForceMode2D.Impulse);
            _rigidbody2D.AddForce(transform.right* 5f, ForceMode2D.Impulse);
            _desh=1;
            EggDrop();
       
        }
     }

     private void EggDrop(){
        _eggRigidBody.transform.position= _rigidbody2D.position;
        _eggSpriteRederer.enabled=true;
        _eggRigidBody.simulated=true;
        _eggRigidBody.AddForce(transform.up* -8f, ForceMode2D.Impulse);
     }


    private void EggReset(){
        _eggSpriteRederer.enabled=false;
        _eggRigidBody.simulated=false;
    }


      private void Life(){
         _life--;
        switch(_life){
        case 2:uiBird3.color=Color.gray;break;
        case 1:uiBird2.color=Color.gray;break;
        case 0:uiBird1.color=Color.gray;
         Invoke("LoadDefeat",4);break;  
        }
      

     }

     public void LoadDefeat(){
        //chamando o menu , no caso alterar para a tela desejada.
        SceneManager.LoadScene(0);
     }
   
}
