using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 10.0f;
  private float jumpForce = 350.0f; 
  private Rigidbody2D physics;
  private SpriteRenderer spriteRenderer;
  private Animator animator; 
  const string AnimSpeed = "Speed";
  const string AnimJumpSpeed = "VerticalSpeed";  
  // Start is called before the first frame update
  void Start()
  {
    physics = this.GetComponent<Rigidbody2D>();
    spriteRenderer = this.GetComponent<SpriteRenderer>();
    animator = this.GetComponent<Animator>(); 
    if (GameState.PantaristeStats == null){
      GameState.PantaristeOriginalStats = new CharacterStats(15, 10, 20, 8, 30);
      GameState.PantaristeStats =  new CharacterStats(15, 10, 20, 8, 30);
    }
  }

  // Update is called once per frame
  void Update()
  {
    // Horizontal movement 
    float direction = Input.GetAxis("Horizontal");
    if (EventTracker.ContainsEvent(EventType.Dialog)){
      direction = 0; 
    }
    physics.velocity= new Vector3(0, physics.velocity.y, 0) +  Vector3.right * direction * speed;
    if (direction < 0){
      spriteRenderer.flipX = true;
    }
    else if (direction > 0){
      spriteRenderer.flipX = false; 
    }
    animator.SetFloat(AnimSpeed, Math.Abs(physics.velocity.x));
    animator.SetFloat(AnimJumpSpeed, physics.velocity.y); 

    // Jumping 
    if (!EventTracker.ContainsEvent(EventType.Dialog) && physics.velocity.y == 0 && Input.GetKeyDown(KeyCode.Space)){
      physics.AddForce(new Vector2(0, jumpForce));
    }
  }
}
