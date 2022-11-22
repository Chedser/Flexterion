
using UnityEngine;

public abstract class WeaponRaycast : Weapon
{
    public int bulletsToGo;
    public int bulletsToGoInit;
    public float maxKillDistance;


    public int damageBody;
    public int damageHead;
    public int damageLimb;

    public float thrustToMove;

    // BULLET IMPACTS
    [Space, Header("Bullet Impacts")]
    [SerializeField] protected GameObject bulletImpactGroundPr;
    [SerializeField] protected GameObject bulletImpactSandPr;
    [SerializeField] protected GameObject bulletImpactWoodPr;
    [SerializeField] protected GameObject bulletImpactIronPr;

    // BLOOD IMPACTS
    [Space, Header("Blood Impacts")]
    [SerializeField] protected GameObject bloodImpactPr;

    protected abstract void SetDemage(RaycastHit hit);
    protected abstract void ShowBulletImpact(RaycastHit hit);
    protected abstract GameObject GetBulletImpact(RaycastHit hit);

    protected abstract void ShowBloodImpact(RaycastHit hit);
    protected abstract void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust);
    protected abstract void DestroyFixedJoint(GameObject hitGo);
    protected abstract void TryToExplode(GameObject hitGo);

}
