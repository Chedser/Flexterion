using UnityEngine;

public abstract class Item:MonoBehaviour
{

    protected abstract void Use();
    protected HeroType heroType;

    protected Health health;

}
