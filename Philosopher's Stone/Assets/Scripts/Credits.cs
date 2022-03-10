using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour{

  CanvasGroup canvasGroup;

  private void Start() {
    canvasGroup = this.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    StartCoroutine(Fade(true)); 
  }
  IEnumerator Fade(bool fadeIn){
    float factor = 0.05f; 

    for (int i = 0; i < 20; i++){
      if (fadeIn){
        canvasGroup.alpha += factor; 
      }
      else {
        canvasGroup.alpha -= factor; 
      }
      yield return null; 
    }

    yield return new WaitForSecondsRealtime(5.0f);
    SceneManager.LoadScene(2);
  }
}
