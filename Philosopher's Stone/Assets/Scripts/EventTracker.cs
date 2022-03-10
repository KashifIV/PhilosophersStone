using System;
using System.Collections.Generic;
using System.Collections; 
using UnityEngine; 

public enum EventType{Dialog, Battle, SceneChange}; 
public static class EventTracker{ 
  // only one of each type of event should be capable of being invoked at the same time. 
  public static Dictionary<EventType, GameEvent> Events {get; private set;} = new Dictionary<EventType, GameEvent>();
  public static bool IssueEvent(EventType eventKey, GameEvent gameEvent){
    if (Events.ContainsKey(eventKey)){
      return false; 
    }
    Events.Add(eventKey, gameEvent);
    return true; 
  }
  public static bool CloseEvent(EventType eventKey){
    if (!Events.ContainsKey(eventKey)){
      return false; 
    }
    Events.Remove(eventKey); 
    return true; 
  }

  public static bool ContainsEvent(EventType eventType){
    return Events.ContainsKey(eventType); 
  }

}