using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;
using TMPro; 

public class DialogBoxHandler : MonoBehaviour
{

  const string IsOpened = "isOpened"; 
  public Animator animator; 
  public Image icon; 
  public TextMeshProUGUI characterName, content; 
  private GameObject player, grid; 
  List<DialogData> dialogData; 
  int dialogIndex; 

  bool isBattling = false;
  GameObject destroyOnComplete; 

  private void Start() {
    player = GameObject.Find("Player"); 
    grid = GameObject.Find("Grid"); 
  }

  public void InvokeDialogBox(List<DialogData> data, GameObject destroyOnComplete){
    dialogIndex = 0;
    UpdateDialogBox(data[dialogIndex]);
    dialogData = data; 
    animator.SetBool(IsOpened, true); 
    this.destroyOnComplete = destroyOnComplete;
  }

  void UpdateDialogBox(DialogData data){
    icon.sprite = data.Sprite; 
    characterName.text = data.Name; 
    content.text = data.Text; 
    dialogIndex++; 
  }

  public void OnContinue(){
    if (dialogIndex >= dialogData.Count){
      animator.SetBool(IsOpened, false);
      EventTracker.CloseEvent(EventType.Dialog);
      Destroy(this.destroyOnComplete);
      if (EventTracker.ContainsEvent(EventType.Battle) && EventTracker.Events.Count == 1){
        SceneManager.LoadScene(1, LoadSceneMode.Additive); 
        player.SetActive(false); 
        grid.SetActive(false); 
        isBattling = true; 
      }
    }
    else{
      UpdateDialogBox(dialogData[dialogIndex]); 
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (isBattling && !EventTracker.ContainsEvent(EventType.Battle)){
      Debug.Log(isBattling); 
      Debug.Log(EventTracker.ContainsEvent(EventType.Battle));
      isBattling = false; 
      grid.SetActive(true); 
      player.SetActive(true); 
    }
  }
}
