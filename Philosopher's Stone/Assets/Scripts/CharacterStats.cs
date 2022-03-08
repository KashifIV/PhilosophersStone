using System; 

[Serializable]
public class CharacterStats{
  public int Attack, Defense, Health, Speed, Power;
  public CharacterStats(int attack, int defense, int health, int speed, int power){
    this.Attack = attack; 
    this.Defense = defense; 
    this.Health = health; 
    this.Speed = speed;
    this.Power = power;
  }
  public CharacterStats(CharacterStats stats){
    this.Attack = stats.Attack; 
    this.Defense = stats.Defense; 
    this.Health = stats.Health; 
    this.Speed = stats.Speed; 
    this.Power = stats.Power; 
  }
}