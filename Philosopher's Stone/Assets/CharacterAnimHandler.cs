using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimHandler : MonoBehaviour
{
  const string AttackAnim = "Attacking"; 
  private Animator animator; 
  void Start()
  {
    animator = this.GetComponent<Animator>(); 
  }

  public void Attack(int attackLevel){
    animator.SetInteger(AttackAnim, attackLevel); 
  }
  IEnumerator FadeToDeath(){
    SpriteRenderer renderer = this.GetComponent<SpriteRenderer>(); 
    int frames = 20; 
    float slice = renderer.color.a / (float)frames; 
    yield return new WaitForSecondsRealtime(1.0f); 
    for (int i = 0; i < frames; i++){
      renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - slice); 
      yield return null; 
    }
    Destroy(this.gameObject); 
  }

  public void OnDeath(){
    StartCoroutine(FadeToDeath()); 
  }

  
}
