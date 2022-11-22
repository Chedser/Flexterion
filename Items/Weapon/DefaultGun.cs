
using System.Collections;
using System.IO;
using UnityEngine;

public class DefaultGun : WeaponRaycast, IShootable{

    PlayerStats playerStats;

    void Awake() {

        health = GetComponentInParent<Health>();
        heroType = GetComponentInParent<SpawnInfo>().heroType;
        cam = GetComponentInParent<VarsManager>().cam;        
    }

    void Start() {

        if (heroType == HeroType.Player) {

            playerStats = GetComponentInParent<PlayerStats>();

        }
    
    }

    // Update is called once per frame
    void Update(){

        if (health.CurrentHealth <= 0) { return; }

        if (canShoot)
        {

            Use();

        }
    }

    protected override void Use()
    {
     
            Shoot();

    }

    public void Shoot(){

        Vector3 directionRay = cam.transform.TransformDirection(Vector3.forward);

        Debug.DrawLine(cam.transform.position, directionRay * maxKillDistance, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, directionRay, out hit, maxKillDistance))
        {

            GameObject hitGo = hit.collider.gameObject;
            Rigidbody hitRb = hitGo.GetComponent<Rigidbody>();

            if (hitGo.GetComponent<Rigidbody>()){

                if (hitGo.GetComponent<PartOfBodyHealth>()) { // ѕопали по врагу

                    SetDemage(hit);
                    ShowBloodImpact(hit);

                }else{ // ѕопали по твердому телу

                    DestroyFixedJoint(hitGo);

                    AddForceToBody(hitRb, directionRay, thrustToMove);

                    ShowBulletImpact(hit);

                    TryToExplode(hitGo);

                }

            }
           
        }

        StartCoroutine(CoroutineShoot());

    }

    public IEnumerator CoroutineShoot()
    {

        // небольша€ задержка
        yield return new WaitForSeconds(shootTime);
        // если еще не все выстрелы произвели...
        if (bulletsToGo > 0)
        {

            //     WeaponManager.canSwitch = false;


            if (!shootSound.isPlaying) { shootSound.Play(); }
            shootEffect.Play();
   
            // то уменьшаем нашу переменную
            bulletsToGo--;
            // и еще раз стрел€ем
            Use();

        }
        else
        {// если все выстрелы уже произвели...
         // то небольша€ задержка
            yield return new WaitForSeconds(shootTime);

            shootSound.Stop();
            shootEffect.Stop();
        
            bulletsToGo = bulletsToGoInit;

            // выходим с сопрограммы
            yield break;

        }

    }

  protected override  void SetDemage(RaycastHit hit) {

        if (hit.collider.gameObject.GetComponent<PartOfBodyHealth>().health.CurrentHealth <= 0) { return; }

        int damageToBody = damageBody;

        switch (hit.collider.gameObject.GetComponent<PartOfBodyHealth>().partOfBody)
        {

            case PartOfBodyHealth.PartOfBody.Limb: damageToBody = damageLimb; break;
            case PartOfBodyHealth.PartOfBody.Head: damageToBody = damageHead; break;

        }

        hit.collider.gameObject.GetComponent<PartOfBodyHealth>().health.TakeDamage(damageToBody);

        if (hit.collider.gameObject.GetComponent<PartOfBodyHealth>().health.CurrentHealth <= 0) { playerStats.SetKills(); }

    }

 protected override   void ShowBulletImpact(RaycastHit hit) {

        GameObject goPr = GetBulletImpact(hit);

        GameObject obj = Instantiate(goPr, hit.collider.transform.position, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();
        

    }

    protected override GameObject GetBulletImpact(RaycastHit hit) {

        GameObject goPr = bulletImpactGroundPr;

        if (hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>())
        {

            RigidbodyType rigidbodyType = hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>().rigidbodyType;

            switch (rigidbodyType)
            {

                case RigidbodyType.Iron: goPr = bulletImpactIronPr; break;
                case RigidbodyType.Sand: goPr = bulletImpactSandPr; break;
                case RigidbodyType.Wood: goPr = bulletImpactWoodPr; break;

            }

        }

        return goPr;

    }

  protected override  void ShowBloodImpact(RaycastHit hit){
   
        GameObject obj = Instantiate(bloodImpactPr, hit.collider.transform.position, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();
           
    }

  protected override  void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust) {

        rb.AddForce(dir * thrust, ForceMode.Impulse);

    }

 protected override void DestroyFixedJoint(GameObject hitGo) {

        FixedJoint fj = hitGo.GetComponent<FixedJoint>();

        if (fj){

            Destroy(fj);

        }

    }

    protected override void TryToExplode(GameObject hitGo) {

        if (hitGo.GetComponent<IExplodable>() != null) {

            hitGo.GetComponent<IExplodable>().Activate(heroType);

        }

    }

}
