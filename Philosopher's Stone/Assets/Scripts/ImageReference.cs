using System.Collections.Generic;
using System; 
using UnityEngine; 
public class ImageReference: MonoBehaviour{

  [Serializable]
  public struct ReferenceContext{
    public string CharacterName; 
    public Sprite Image; 
  }
  public ReferenceContext[] referenceData; 

  Dictionary<string, Sprite> reference;

  [SerializeField]
  Sprite defaultSprite; 

  private void Start() {
    reference = new Dictionary<string, Sprite>();
    foreach(ReferenceContext context in referenceData){
      reference.Add(context.CharacterName, context.Image); 
    }
  }
  public Sprite Get(string name){
    if (reference.ContainsKey(name)){
      return reference[name]; 
    }
    return defaultSprite; 
  }
}