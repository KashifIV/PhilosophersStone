using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float speed = 10.0f;
  private Rigidbody2D rigidbody2D;
  private SpriteRenderer spriteRenderer;
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D = this.GetComponent<Rigidbody2D>();
    spriteRenderer = this.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    float direction = Input.GetAxis("Horizontal");
    rigidbody2D.velocity= new Vector3(0, rigidbody2D.velocity.y, 0) +  Vector3.right * direction * speed;
    if (direction < 0){
      spriteRenderer.flipX = false;
    }
    else{
      spriteRenderer.flipX = true; 
    }
  }
}
