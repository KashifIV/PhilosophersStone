using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class CharacterSelectIcon : MonoBehaviour
{
  [SerializeField]
  private ProgressBarHandler progressBarHandler;
  [SerializeField]
  private TextMeshProUGUI textMesh; 
  private Image image; 
  private ImageReference imageReference; 

  private void Awake() {
    imageReference = GameObject.Find("data").GetComponent<ImageReference>();
    image = this.GetComponent<Image>(); 
  }

  public void UpdateData(CharRef charRef){
    image.sprite = imageReference.Get(charRef.characterData.Name);
    CharacterStats stats = charRef.currentStats; 
    CharacterStats originalStats = charRef.characterData.Stats; 
    textMesh.text = charRef.characterData.Name;
    progressBarHandler.UpdateCompletion(stats.Health == 0? 0 : (float)stats.Health / (float)originalStats.Health);
    if (stats.Health == 0){
      this.GetComponent<Button>().interactable = false; 
    }
  }

  public void Hide(){
    image.enabled = false; 
    this.GetComponent<Button>().enabled = false; 
    for (int i = 0; i < transform.childCount; i++){
      transform.GetChild(i).gameObject.SetActive(false); 
    }
  }
  public void Show(){
    image.enabled = true; 
    this.GetComponent<Button>().enabled = true; 
    for (int i = 0; i < transform.childCount; i++){
      transform.GetChild(i).gameObject.SetActive(true); 
    }
  }
  public void Disable(){
    this.GetComponent<Button>().interactable = false; 
  }
}
