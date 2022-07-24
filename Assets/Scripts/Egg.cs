using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
   [SerializeField] private ParticleSystem _eggExplosin;
   [SerializeField] private SpriteRenderer _eggSprintRenderer;

   private void OnCollisionEnter2D(Collision2D other) {

    if(!_eggExplosin.isPlaying && other.gameObject.tag!="Bird"){
           _eggExplosin.Play();
           GetComponent<CircleCollider2D>().radius=11f;
     _eggSprintRenderer.enabled=false;
    Invoke("reset",3);
     
    }
    
     
   }


   private void reset(){
    GetComponent<CircleCollider2D>().radius=0.5f;
   }
    
}
