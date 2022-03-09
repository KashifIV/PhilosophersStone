using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType{Power, Health}; 
public class Potion : MonoBehaviour
{
  [SerializeField]
  public PotionType potionType;
  public int Amount = 10;
  
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.GetComponent<Player>() != null){
      if (potionType == PotionType.Power){
        GameState.PantaristeStats.Power += Amount; 
        if (GameState.PantaristeOriginalStats.Power < GameState.PantaristeStats.Power){
          GameState.PantaristeStats.Power = GameState.PantaristeOriginalStats.Power; 
        }
      }
      else {
        GameState.PantaristeStats.Health += Amount; 
        if (GameState.PantaristeOriginalStats.Health < GameState.PantaristeStats.Health){
          GameState.PantaristeStats.Health = GameState.PantaristeOriginalStats.Health; 
        }
      }
      Destroy(this.gameObject);
    }
  }
}
