using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
  private void Start() {
    GameState.LoadFromSave = false; 
  }
  public void OnStart(){
    SceneManager.LoadScene(0); 
  }

  public void OnStartFromSave(){
    GameState.LoadFromSave = true; 
    OnStart();
  }

  public void Exit(){
    Application.Quit();
  }
}
