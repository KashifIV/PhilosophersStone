using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarHandler : MonoBehaviour
{
  private float target = 1.0f; 
  private RectTransform rectTransform; 
 

  // Start is called before the first frame update
  void Start()
  {
    rectTransform = this.transform.GetChild(0).GetComponent<RectTransform>(); 
  }

  IEnumerator updateProgressBar(){
    int frames = 20; 
    float originalScale = rectTransform.localScale.x; 
    float slice = (originalScale - target) / frames;
    if (target < originalScale) slice*= -1;  

    for (int i = 0; i < frames; i++){
      rectTransform.localScale = new Vector3(rectTransform.localScale.x + slice, 1, 1); 
      yield return null; 
    }
    rectTransform.localScale = new Vector3(target, 1, 1); 
  }
  
  public void UpdateCompletion(float completion){
    if (completion != target){
      if (target != rectTransform.localScale.x){
        StopAllCoroutines(); 
      }
      target = completion; 
      StartCoroutine(updateProgressBar());
    }
  }
}
