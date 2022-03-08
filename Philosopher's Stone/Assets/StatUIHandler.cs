using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUIHandler : MonoBehaviour
{
  [SerializeField]
  private ProgressBarHandler healthBar, powerBar; 
  public void UpdateStats(CharacterStats stats, CharacterStats originalStats){

    healthBar.UpdateCompletion(stats.Health == 0? 0 : (float)stats.Health / (float)originalStats.Health); 
    powerBar.UpdateCompletion(stats.Power == 0 ? 0 : (float)stats.Power / (float)originalStats.Power); 
  }
}
