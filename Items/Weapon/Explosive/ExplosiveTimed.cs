
using UnityEngine;

public class ExplosiveTimed : ExplosiveBasicTimed, IExplodable
{

    bool _isDone;

    public void Activate(HeroType heroType = HeroType.Player){
      
         if (_isDone) { return; }

        this.heroType = heroType;

        _isDone = true;
         Invoke(nameof(Use), 0.7f);

        }

    protected override void Use(){

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {

            PushRigidbody(overlappedColliders[i]);
            ActivateExplodable(overlappedColliders[i]);
            GiveDemage(overlappedColliders[i]);
        
        }

        Instantiate(exploisionEffect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    protected override void PushRigidbody(Collider collider)
    {
        Rigidbody rigidbody = collider.attachedRigidbody;
   
        if(rigidbody != null) rigidbody.AddExplosionForce(force * 100000, transform.position, radius, jumpForce);


    }

    protected override void ActivateExplodable(Collider collider)
    {
     
        if (collider.gameObject.GetComponent<IExplodable>() != null) collider.gameObject.GetComponent<IExplodable>().Activate(heroType);

    }

    protected override void GiveDemage(Collider collider) {

        IDemagable health = collider.gameObject.GetComponent<IDemagable>();

        if (health != null) {

            health.TakeDamage(demage);

        }

    }
}

    
   

