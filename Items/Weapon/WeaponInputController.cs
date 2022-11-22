using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInputController : MonoBehaviour
{

    WeaponController weaponController;

    float mouseScroll;

    private void Awake(){

        weaponController = GetComponent<WeaponController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScroll != 0){

            weaponController.mouseScroll = mouseScroll;

        }
        else {

            weaponController.mouseScroll = 0;

        }

        if (Input.GetMouseButtonDown(0)){
            weaponController.canShoot = true;
        }
        else {

            weaponController.canShoot = false;

        }

    }
}
