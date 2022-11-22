

using UnityEngine;

public class ExplosiveHealthTimed : ExplosiveBasicTimed, IExplodable{

    public float health;
    bool _isDone;

    public void Activate(HeroType heroType){

        if (_isDone) { return; }

        this.heroType = heroType;

    }

    protected override void Use()
    {
        throw new System.NotImplementedException();
    }

    protected override void PushRigidbody(Collider collider)
    {
        Rigidbody rigidbody = collider.attachedRigidbody;
        rigidbody.AddExplosionForce(force * 100000, transform.position, radius, 1f);
    }

    protected override void ActivateExplodable(Collider collider)
    {

        if (collider.gameObject.GetComponent<IExplodable>() != null) collider.gameObject.GetComponent<IExplodable>().Activate(heroType);


    }

    protected override void GiveDemage(Collider collider)
    {

        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null)
        {

            health.TakeDamage(demage);

        }

    }




}
