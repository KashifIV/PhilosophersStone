using System.Collections.Generic;
public class GameEvent{
  public string EventName {get; private set;}
  public Dictionary<string, dynamic> Metadata {get; private set;}

  public GameEvent(string name){
    EventName = name; 
  }
  public GameEvent(string name, Dictionary<string, dynamic> metadata){
    EventName = name; 
    Metadata = metadata;
  }
}