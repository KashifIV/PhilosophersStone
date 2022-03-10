using System.IO;
using System; 
using UnityEngine; 

// I was going to allow you to play with two members in your party but
// ran out of time :s
enum PlayerCharacters{Pantariste, Hypatia, Both}; 
public class BattleDataGenerator : MonoBehaviour {
  public string battleName, location; 

  [SerializeField]
  PlayerCharacters playerCharacters; 

  public Character[] enemies; 

  [ContextMenu("Save Battle Data")]
  void SaveBattleData(){
    Character[] player; 
    switch(playerCharacters){
      case PlayerCharacters.Hypatia: 
        Character hypatia = new Character(); 
        hypatia.Name = "Hypatia";
        player = new Character[]{hypatia}; 
        break; 
      case PlayerCharacters.Pantariste: 
        Character pantariste = new Character();
        pantariste.Name = "Pantariste"; 
        player = new Character[]{pantariste}; 
        break;
      default: 
        Character a = new Character(); 
        a.Name = "Pantariste"; 
        Character b = new Character(); 
        b.Name = "Hypatia"; 
        player = new Character[]{a, b}; 
        break; 
    }
    BattleData battleData = new BattleData(); 
    battleData.Player = player; 
    battleData.Enemy = enemies; 
    battleData.Location = location; 

    SaveToFile(battleData); 
  }

  void SaveToFile(BattleData battleData){
    string directory = Application.dataPath + "/Resources/BattleData/"; 
    if (!Directory.Exists(directory)){
      Directory.CreateDirectory(directory); 
    }
    string json = JsonUtility.ToJson(battleData); 
    File.WriteAllText(directory + battleName + ".json", json); 
  }
  public BattleData LoadFromFile(string filename){
    string path = Application.dataPath + "/Resources/BattleData/" + filename + ".json"; 
    if (File.Exists(path)){
      string json = File.ReadAllText(path);
      BattleData data = JsonUtility.FromJson<BattleData>(json); 
      return data; 
    }
    else {
      Debug.Log("Failed to find " + filename); 
    }
    return null; 
  }
}