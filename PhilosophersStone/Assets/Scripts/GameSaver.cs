using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
  [SerializeField]
  private int Scene; 
  GameSaveData previousSave; 
  private void Awake() {
    if (GameState.LoadFromSave){
      previousSave = LoadFromFile();
      if (previousSave != null){
        if (Scene != previousSave.Scene){
          SceneManager.LoadScene(previousSave.Scene);
        }
      }
    }
  }
  private void Start() {
    GameState.CurrentScene = Scene;
    if (GameState.LoadFromSave && previousSave != null){
      GameObject.Find("Player").GetComponent<Transform>().position = 
        new Vector3(previousSave.PlayerPosition.x, previousSave.PlayerPosition.y, previousSave.PlayerPosition.z); 
      
      GameState.PantaristeOriginalStats = previousSave.OriginalStats;
      GameState.PantaristeStats = previousSave.CurrentStats;

      foreach(GameObject item in GameObject.FindGameObjectsWithTag("EventMarker")){
        if (!previousSave.EventMarkers.Contains(item.name)){
          Destroy(item); 
        }
      }

      foreach(GameObject item in GameObject.FindGameObjectsWithTag("PowerPotion")){
        if (!previousSave.PowerPotions.Contains(item.name)){
          Destroy(item); 
        }
      }

      foreach(GameObject item in GameObject.FindGameObjectsWithTag("HealthPotion")){
        if (!previousSave.HealthPotions.Contains(item.name)){
          Destroy(item); 
        }
      }
    }
    StartCoroutine(KillLoadFlag());
  }
  IEnumerator KillLoadFlag(){
    yield return new WaitForEndOfFrame(); 
    GameState.LoadFromSave = false; 
  }

  public void Save(){
    GameSaveData data = new GameSaveData(); 
    Transform player = GameObject.Find("Player").GetComponent<Transform>();

    data.PlayerPosition = new Vec3Serializable(player.position.x, player.position.y, player.position.z); 
    data.CurrentStats = GameState.PantaristeStats; 
    data.OriginalStats = GameState.PantaristeOriginalStats;
    data.Scene = Scene;

    data.EventMarkers = 
      GameObject.FindGameObjectsWithTag("EventMarker").Select(x => x.name).ToArray<string>();

    data.PowerPotions = 
      GameObject.FindGameObjectsWithTag("PowerPotion").Select(x => x.name).ToArray<string>(); 

    data.HealthPotions = 
      GameObject.FindGameObjectsWithTag("HealthPotion").Select(x => x.name).ToArray<string>();
    
    SaveToFile(data);
    Debug.Log("Saved Game");
    
  }

  private void SaveToFile(GameSaveData data){
    string directory = Application.streamingAssetsPath + "/save/"; 
    if (!Directory.Exists(directory)){
      Directory.CreateDirectory(directory); 
    }

    string json = JsonUtility.ToJson(data); 
    File.WriteAllText(directory+ "save.json", json); 
  }
  private GameSaveData LoadFromFile(){
    string path = Application.streamingAssetsPath + "/save/save.json"; 
    if (!File.Exists(path)){
      Debug.Log("Failed to find Save Data");
      return null; 
    }
    string json = File.ReadAllText(path); 
    GameSaveData data = JsonUtility.FromJson<GameSaveData>(json); 
    return data; 
  }
}
