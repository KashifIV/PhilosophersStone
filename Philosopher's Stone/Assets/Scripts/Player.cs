using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 10.0f;
  private Rigidbody2D physics;
  private SpriteRenderer spriteRenderer;
  private Animator animator; 
  const string AnimSpeed = "Speed"; 
  // Start is called before the first frame update
  void Start()
  {
    physics = this.GetComponent<Rigidbody2D>();
    spriteRenderer = this.GetComponent<SpriteRenderer>();
    animator = this.GetComponent<Animator>(); 
  }

  // Update is called once per frame
  void Update()
  {
    float direction = Input.GetAxis("Horizontal");
    if (EventTracker.ContainsEvent(EventType.Dialog)){
      direction = 0; 
    }
    physics.velocity= new Vector3(0, physics.velocity.y, 0) +  Vector3.right * direction * speed;
    if (direction < 0){
      spriteRenderer.flipX = false;
    }
    else if (direction > 0){
      spriteRenderer.flipX = true; 
    }
    animator.SetFloat(AnimSpeed, Math.Abs(physics.velocity.x)); 
  }
}
