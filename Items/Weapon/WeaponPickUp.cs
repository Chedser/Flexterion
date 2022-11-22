using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    private void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<SpawnInfo>()) {

            ISwitchable weapon = collision.gameObject.GetComponentInChildren<ISwitchable>();
            Weapon weaponScript = GetComponent<Weapon>();
            weaponScript.enabled = true;

            WeaponInfo weaponInfo = GetComponent<WeaponInfo>();

            weapon.Equip(this.gameObject,weaponInfo.weaponType);

        }

    }

}
