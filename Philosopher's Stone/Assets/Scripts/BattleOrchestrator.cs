using System.Collections;
using System.Linq; 
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;
public class CharRef{
  public Character characterData; 
  public CharacterStats currentStats; 
  public GameObject gameObject;
  public CharRef(Character data, GameObject gameObject){
    characterData = data; 
    currentStats = new CharacterStats(data.Stats); 
    this.gameObject = gameObject; 
  }
}

public class BattleOrchestrator : MonoBehaviour
{
  CharRef currentCharacter; 
  private BattleConductor conductor; 
  public BattleUIHandler uiHandler;

  private int attackLevel; 
  private void Awake() {
    string filename; 
    if (EventTracker.Events.Count == 0){
      filename = "FirstBattle"; 
    }
    else {
      filename = EventTracker.Events[EventType.Battle].Metadata["filename"]; 
    }
    if (GameState.PantaristeStats == null){
      GameState.PantaristeStats = new CharacterStats(15, 10, 20, 8, 30); 
      GameState.HypatiaStats = new CharacterStats(20, 6, 20, 6, 30); 
    }
    BattleData data = this.GetComponent<BattleDataGenerator>().LoadFromFile(filename); 

    List<CharRef> players, enemies; 
    players = new List<CharRef>(); 
    enemies = new List<CharRef>(); 
    foreach(Character character in data.Player){
      if (character.Name == "Pantariste") character.Stats = GameState.PantaristeStats;
      else if (character.Name == "Hypatia") character.Stats = GameState.HypatiaStats; 
      players.Add(new CharRef(character, (GameObject)Instantiate(Resources.Load(character.Name)))); 
    }
    foreach(Character character in data.Enemy){
      enemies.Add(new CharRef(character, (GameObject)Instantiate(Resources.Load(character.Name)))); 
    }

    List<BattleFunction> order = players.Select<CharRef, BattleFunction>(x => () => PlayerAttack(x)).ToList();
    order.AddRange(enemies.Select<CharRef, BattleFunction>(x => () => EnemyAttack(x)).ToList());
    conductor = new BattleConductor(players, enemies, Win, Lose, order);

    PositionPlayers();
    PositionEnemies();
    currentCharacter = conductor.Players[0]; 

  }
  private void Start() {
   conductor.ExecuteNext(); 
  }

  delegate void DelayedFunction(); 

  IEnumerator ExecuteDelayed(float delayInSeconds, DelayedFunction function){
    yield return new WaitForSecondsRealtime(delayInSeconds); 
    function(); 
  }
  IEnumerator ExecuteDelayed(AsyncOperation operation, DelayedFunction function){
    while(!operation.isDone){
      yield return null; 
    }
    function(); 
  }

  private void Win(){
    Debug.Log("Player Wins!");
    uiHandler.victoryScreen.GetComponent<Animator>().SetTrigger("Animate"); 
    EventTracker.CloseEvent(EventType.Battle);
    StartCoroutine(ExecuteDelayed(3.0f, () => {
      AsyncOperation operation = SceneManager.UnloadSceneAsync(1);
      GameState.PantaristeStats = currentCharacter.currentStats;
      StartCoroutine(ExecuteDelayed(operation, () => {
        EventTracker.CloseEvent(EventType.Battle);
      }));
    })); 
  }
  private void Lose(){
    Debug.Log("Player Loses!"); 
    uiHandler.victoryScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "DEFEAT"; 
    uiHandler.victoryScreen.GetComponent<Animator>().SetTrigger("Animate"); 
    StartCoroutine(ExecuteDelayed(3.0f, () => {
      SceneManager.LoadScene(0); 
      EventTracker.CloseEvent(EventType.Battle);
    })); 
  }

  private void PlayerAttack(CharRef player){
    Debug.Log("Player Attack"); 
    if (player.currentStats.Health <= 0){
      conductor.ExecuteNext();
      return; 
    }
    currentCharacter = player; 
    uiHandler.UpdateSelectedCharacter(currentCharacter); 
  }
  private void EnemyAttack(CharRef enemy){
    Debug.Log("Enemy Attack"); 
    if (enemy.currentStats.Health <= 0){
      conductor.ExecuteNext(); 
      return; 
    }
    enemy.gameObject.GetComponent<CharacterAnimHandler>().Attack(1);
    currentCharacter.currentStats.Health -=Math.Max(enemy.currentStats.Attack - currentCharacter.currentStats.Defense, 0); 
    uiHandler.UpdateSelectedCharacter(currentCharacter); 
    StartCoroutine(ExecuteDelayed(2.0f, conductor.ExecuteNext)); 
  }


  private void PositionPlayers(){
    float[] positions; 
    if (conductor.Players.Count == 1){
      positions = new float[]{-4}; 
    }
    else{
      positions = new float[]{-3, -6}; 
    }
    for (int i = 0; i < conductor.Players.Count; i++){
      conductor.Players[i].gameObject.GetComponent<SpriteRenderer>().flipX = true; 
      conductor.Players[i].gameObject.GetComponent<Transform>().position = new Vector3(positions[i], 0, 0); 
    }
  }
  private void PositionEnemies(){
    for (int i = 0; i < conductor.Enemies.Count; i++){
      conductor.Enemies[i].gameObject.GetComponent<Transform>().position = new Vector3(i*2, 0, 0); 
    }
  }

  public void OnSelectAttack(int attackLevel){
    this.attackLevel = attackLevel; 
    uiHandler.ToggleButtons(false); 
    uiHandler.ToggleCharacterSelect(conductor.Enemies); 
  }
  public void OnSelectCharacter(int index){
    currentCharacter.gameObject.GetComponent<CharacterAnimHandler>().Attack(attackLevel);
    currentCharacter.currentStats.Power -= new int[]{0, 10, 30}[attackLevel - 1]; 
    uiHandler.UpdateSelectedCharacter(currentCharacter);

    conductor.Enemies[index].currentStats.Health -= Math.Max(currentCharacter.currentStats.Attack*attackLevel-conductor.Enemies[index].currentStats.Defense, 0);
    if (conductor.Enemies[index].currentStats.Health <= 0){
      conductor.Enemies[index].gameObject.GetComponent<CharacterAnimHandler>().OnDeath();
      conductor.Enemies.RemoveAt(index);  
    }

    uiHandler.ToggleCharacterSelect(false); 
    uiHandler.ToggleButtons(true);
    uiHandler.DisableButtons(); 
    this.attackLevel = 0; 

    StartCoroutine(ExecuteDelayed(2.0f, conductor.ExecuteNext)); 
  }
}
