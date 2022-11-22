using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicHealth : MonoBehaviour
{
   protected HeroType heroType;
    protected abstract void Die();
    protected abstract float InitHealth(HeroType heroType);
    protected abstract void Respawn();
    protected abstract Vector3 GetRandomSpawnPoint();
}
