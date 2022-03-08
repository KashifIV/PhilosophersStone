using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using TMPro; 

public class BattleUIHandler : MonoBehaviour
{
  public bool IsDisabled {get; private set;}
  public TextMeshProUGUI nameCard; 
  public StatUIHandler statsUIHandler; 
  public Button[] buttons; 
  public CharacterSelectIcon[] characterSelectIcons; 
  public GameObject victoryScreen; 
  ImageReference imageReference; 
  private void Start() {
    imageReference = GameObject.Find("data").GetComponent<ImageReference>();
    DisableButtons(); 
    ToggleCharacterSelect(false);
  }

  public void DisableButtons(){
    foreach(Button button in buttons){
      button.interactable = false; 
    }
    IsDisabled = true; 
  }

  public void ToggleButtons(bool isActive){
    foreach(Button button in buttons){
      button.gameObject.SetActive(isActive); 
    }
  }
  public void ToggleCharacterSelect(bool isActive){
    foreach(CharacterSelectIcon icon in characterSelectIcons){
      if (isActive){
        icon.Show();
      }
      else 
        icon.Hide(); 
    }
  }

  public void ToggleCharacterSelect(List<CharRef> characters){
    for (int i = 0; i < characterSelectIcons.Length; i++){
      if (i < characters.Count){
        characterSelectIcons[i].Show();
        characterSelectIcons[i].UpdateData(characters[i]); 
      } 
      else{
        characterSelectIcons[i].Hide();
      }
    }
  }

  public void UpdateSelectedCharacter(CharRef character){
    nameCard.text = character.characterData.Name; 
    statsUIHandler.UpdateStats(character.currentStats, character.characterData.Stats);
    DisableButtons(); 
    buttons[0].interactable = true; 
    if (character.currentStats.Power >= 15){
      buttons[1].interactable = true; 
    }
    if (character.currentStats.Power >= 30){
      buttons[2].interactable = true; 
    }
  }

  // Update is called once per frame
  void Update() 
  {
      
  }
}
