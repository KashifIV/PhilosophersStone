using System;

public static class GameState{
  public static string Level {get; set;}
  public static int CurrentScene;
  public static bool LoadFromSave = false; 
  public static CharacterStats PantaristeStats, HypatiaStats, PantaristeOriginalStats; 
  
}