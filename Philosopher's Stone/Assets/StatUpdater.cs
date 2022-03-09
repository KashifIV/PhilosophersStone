using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpdater : MonoBehaviour
{
  public StatUIHandler handler; 

  private void Update() {
    handler.UpdateStats(GameState.PantaristeStats, GameState.PantaristeOriginalStats);
  }
}
