using UnityEditor.Animations;
using UnityEngine;

public class WeaponController : WeaponBasicSwitch, ISwitchable
{

    [SerializeField] GameObject weaponGripsContainer;
    Transform weaponContainer;
    int currentWeaponID;
    int weaponSwitchID = 0;

  public  float mouseScroll;

    public float timer = 0.3f;
    public float repeatTimer;

    bool canScroll;
    public bool canShoot;

    void Awake() {

        weaponContainer = this.transform;

    }

    public void Equip(GameObject go, WeaponType weaponType) {

        go.GetComponent<Collider>().enabled = false;
        go.GetComponent<Rigidbody>().useGravity = false;
        go.GetComponent<Rigidbody>().isKinematic = true;

        go.transform.SetParent(weaponContainer);

        Transform gripsContainer = weaponGripsContainer.transform.Find(weaponType.ToString() + "Grips");

        go.transform.position = gripsContainer.position;
        go.transform.rotation = gripsContainer.rotation;

    //    SaveWeaponPose(gripsContainer, go);

        ShowActiveWeapon();

    }

    void Update() {

        if (weaponContainer.childCount <= 1) { return; }

        timer -= Time.deltaTime;

        currentWeaponID = weaponSwitchID;

        if (timer <= 0) {

            ScrollWeapon();

            canScroll = true;

        }

        if (currentWeaponID != weaponSwitchID && canScroll){

            Switch();

        }

        if (canShoot)
        {

            weaponContainer.GetChild(currentWeaponID).GetComponent<Weapon>().canShoot = true;

        }
        else {

            weaponContainer.GetChild(currentWeaponID).GetComponent<Weapon>().canShoot = false;

        }

    }

    protected override void Switch() {


        if (weaponContainer.childCount <= 1) { return; }

        for (int i = 0; i < weaponContainer.childCount; i++)
        {

            if (i == weaponSwitchID) {

                weaponContainer.GetChild(i).gameObject.SetActive(true);


            }
            else
            {

                weaponContainer.GetChild(i).gameObject.SetActive(false);

            }

        }

        timer = repeatTimer;
        canScroll = false;

    }

    protected override void ShowActiveWeapon()
    {
        if (weaponContainer.childCount <= 1) { return; }

        currentWeaponID = weaponContainer.childCount - 1;

        for (int i = 0; i < weaponContainer.childCount; i++) {

            if (i != currentWeaponID) {

                weaponContainer.GetChild(i).gameObject.SetActive(false);

            }

        }
    }

    [ContextMenu("Save Weapon Pose")]
    void SaveWeaponPose(Transform grips, GameObject go) {

        GameObjectRecorder recorder = new GameObjectRecorder(go);
        recorder.BindComponentsOfType<Transform>(grips.gameObject, false);
        recorder.BindComponentsOfType<Transform>(grips.transform.GetChild(0).gameObject, false);
        recorder.BindComponentsOfType<Transform>(grips.transform.GetChild(1).gameObject, false);
        recorder.TakeSnapshot(0.0f);

    }

    void ScrollWeapon(){

        if (mouseScroll > 0.0f)
        {

            if (weaponSwitchID >= (weaponContainer.childCount - 1))
            {

                weaponSwitchID = 0;

            }
            else
            {

                weaponSwitchID++;

            }

        }

        if (mouseScroll < 0.0f)
        {

            if (weaponSwitchID <= 0)
            {

                weaponSwitchID = weaponContainer.childCount - 1;

            }
            else
            {

                weaponSwitchID--;

            }

        }

    }

}
