using System.Collections.Generic;
using UnityEngine;

public delegate void BattleFunction(); 
public class BattleConductor{
  private Queue<BattleFunction> score;
  private List<BattleFunction> order;  
  public List<CharRef> Players, Enemies; 
  private BattleFunction onWin, onLose; 

  public BattleConductor(List<CharRef> players, List<CharRef> enemies, BattleFunction win, BattleFunction lose, List<BattleFunction> order){
    this.Players = players; 
    this.Enemies = enemies; 

    this.onWin = win; 
    this.onLose = lose; 

    this.order = order;
    score = new Queue<BattleFunction>(); 
    foreach(BattleFunction function in order){
      score.Enqueue(function); 
    }
    score.Enqueue(Loop);
  } 

  public void ExecuteNext(){
    BattleFunction function = score.Dequeue(); 
    function(); 
  }
  
  private void Loop(){
    Debug.Log("Looping");
    bool win = true; 
    foreach(CharRef enemy in Enemies){
      if (enemy.currentStats.Health > 0){
        win = false; 
      }
    }
    if (win){
      onWin(); 
      return; 
    }
    bool lose = true; 
    foreach(CharRef player in Players){
      if (player.currentStats.Health > 0){
        lose = false; 
      }
    }
    if (lose){
      onLose();
      return; 
    }

    foreach(BattleFunction function in order){
      score.Enqueue(function); 
    }
    score.Enqueue(Loop); 
    ExecuteNext();
  }

}