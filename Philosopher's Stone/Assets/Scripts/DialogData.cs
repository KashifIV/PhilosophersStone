using UnityEngine; 

public class DialogData{

  public string Name {get; private set;}
  public string Text {get; private set;}

  public Sprite Sprite {get; private set;}

  public DialogData(string name, string text, Sprite sprite){
    this.Name = name; 
    this.Text = text; 
    this.Sprite = sprite; 
  }


}