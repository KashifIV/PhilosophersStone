using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro; 

public class DialogBoxHandler : MonoBehaviour
{

  const string IsOpened = "isOpened"; 
  public Animator animator; 
  public Image icon; 
  public TextMeshProUGUI characterName, content; 

  List<DialogData> dialogData; 
  int dialogIndex; 

  public void InvokeDialogBox(List<DialogData> data){
    dialogIndex = 0;
    UpdateDialogBox(data[dialogIndex]);
    dialogData = data; 
    animator.SetBool(IsOpened, true); 
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
    }
    else{
      UpdateDialogBox(dialogData[dialogIndex]); 
    }
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
