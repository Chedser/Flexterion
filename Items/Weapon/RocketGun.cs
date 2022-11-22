using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : WeaponMissiled, IShootable
{
    protected override void Use(){
        Shoot();
    }

    public void Shoot() { 
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
