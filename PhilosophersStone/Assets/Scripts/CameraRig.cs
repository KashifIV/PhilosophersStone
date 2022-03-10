using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
  [SerializeField]
  private Transform actor;
  
  [SerializeField]
  private float maxX, maxY, minX, minY; 

  private Transform cameraTransform;
  private float offsetX, offsetY;   
  // Start is called before the first frame update
  void Start()
  {
    cameraTransform = this.GetComponent<Transform>(); 
    offsetX = cameraTransform.position.x - actor.position.x; 
    offsetY = cameraTransform.position.y - actor.position.y; 
  }

  // Update is called once per frame
  float GetX(){
    float x = offsetX + actor.position.x; 
    if (x > maxX){
      return maxX; 
    }
    else if (x < minX){
      return minX; 
    }
    return x; 
  }
  float GetY(){
    float y = offsetY + actor.position.y; 
    if (y > maxY){
      return maxY; 
    }
    else if (y < minY){
      return minY;
    }
    return y; 
  }
  void Update()
  {
    this.cameraTransform.position = new Vector3(GetX(), GetY(), cameraTransform.position.z); 
  }
}
