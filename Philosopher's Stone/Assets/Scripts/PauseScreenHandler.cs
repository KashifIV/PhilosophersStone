using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreenHandler : MonoBehaviour
{
  public GameObject pauseScreen;
  GameSaver gameSaver; 
  bool isOpen; 

  public void OnSaveGame(){
    gameSaver.Save();
  }

  public void OnReturnToMenu(){
    SceneManager.LoadScene(2);
  }
  public void OnReturnToGame(){
    isOpen = false; 
    pauseScreen.SetActive(false); 
  }

  private void Start() {
    isOpen = false; 
    pauseScreen.SetActive(false); 
    gameSaver = this.GetComponent<GameSaver>();
  }
  
  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)){
      isOpen = !isOpen;
      pauseScreen.SetActive(isOpen);
    }
  }
}
