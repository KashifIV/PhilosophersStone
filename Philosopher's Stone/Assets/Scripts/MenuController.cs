using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
  [SerializeField]
  private Button fromSaveButton;
  void Start() {
    GameState.LoadFromSave = false; 
    GameState.CurrentScene = 2;
    if (!File.Exists(Application.streamingAssetsPath + "/save/save.json")){
      fromSaveButton.enabled = false; 
    }
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
