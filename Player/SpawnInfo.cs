using UnityEngine;
public enum HeroGroup {Human, Alien, Helper, Neutral }
public enum HeroType { Player}


public class SpawnInfo:MonoBehaviour
{

    public HeroType heroType;
    public HeroGroup heroGroup;
}
