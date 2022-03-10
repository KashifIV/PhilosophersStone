using System; 

[Serializable]
public class GameSaveData{
  public Vec3Serializable PlayerPosition; 
  public CharacterStats CurrentStats, OriginalStats;

  public int Scene; 
  
  public string[] EventMarkers; 
  public string[] PowerPotions; 
  public string[] HealthPotions; 
}

[Serializable]
public class Vec3Serializable{
  public float x, y, z; 
  public Vec3Serializable(float x , float y, float z){
    this.x = x; 
    this.y = y; 
    this.z = z; 
  }
}