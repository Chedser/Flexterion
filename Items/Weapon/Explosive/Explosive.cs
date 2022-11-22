using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explosive : Item
{

    public float force;
    public float radius;
    public int demage;
    public float jumpForce;
  
    [SerializeField] protected GameObject exploisionEffect;

    protected abstract void PushRigidbody(Collider collider);
    protected abstract void ActivateExplodable(Collider collider);
    protected abstract void GiveDemage(Collider collider);

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius / 2f);
    }

}
