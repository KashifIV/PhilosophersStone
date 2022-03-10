using System.Collections.Generic;
public class GameEvent{
  public string EventName {get; private set;}
  public Dictionary<string, string> Metadata {get; private set;}

  public GameEvent(string name){
    EventName = name; 
    Metadata = new Dictionary<string, string>();
  }
  public GameEvent(string name, Dictionary<string, string> metadata){
    EventName = name; 
    Metadata = metadata;
  }
}