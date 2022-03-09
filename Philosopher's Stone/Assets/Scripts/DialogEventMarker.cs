using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogEventMarker : MonoBehaviour
{
  [TextArea]
  public string script;
  public bool includeBattle;
  public string battleMetadataFile;

  bool invoked = false; 
  DialogBoxHandler handler; 
  List<DialogData> dialogData;
  ImageReference imageReference;  
  void Start()
  {
    handler = GameObject.Find("UI").GetComponent<DialogBoxHandler>(); 
    imageReference = GameObject.Find("data").GetComponent<ImageReference>(); 
    dialogData = ScriptToData(); 
  }
  
  List<DialogData> ScriptToData(){
    List<DialogData> data = new List<DialogData>(); 
    if (script == null){
      return data; 
    }

    string[] parts = script.Split('\n'); 
    for (int i = 0; i < parts.Length; i+=2){
      data.Add(new DialogData(parts[i], parts[i+1], imageReference.Get(parts[i]))); 
    }
    return data; 
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!invoked 
        && other.gameObject.GetComponent<Player>() != null 
        && EventTracker.IssueEvent(EventType.Dialog, new GameEvent("Dialog Event"))
        ){
      handler.InvokeDialogBox(dialogData, this.gameObject); 
      invoked = true; 
      if (includeBattle){
        EventTracker.IssueEvent(
          EventType.Battle, 
          new GameEvent(
            "Battle", 
            new Dictionary<string, string>{{"filename", battleMetadataFile}}
        ));
      }
    }
  }
}
