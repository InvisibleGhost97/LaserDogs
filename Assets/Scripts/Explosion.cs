using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {

    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject blowUp;
    [SerializeField]
    GameObject sparks;
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    Shield shield;
    [SerializeField]
    float LaserHitModifier = 20f;

    public void HittiedMe(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);

        if (shield == null)
        {
            return;
        }
        shield.TakeDamage();
    }

    public void Sparks(Vector3 pos)
    {
        GameObject go = Instantiate(sparks, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            HittiedMe(contact.point);
        }
    }

    public void AddForce(Vector3 hitPosition, Transform hitSource)
    {
        HittiedMe(hitPosition);
        if (rigidbody == null)
        {
            return;
        }

        Vector3 forceVector = (hitSource.position - hitPosition).normalized;
        rigidbody.AddForceAtPosition(forceVector.normalized* LaserHitModifier, hitPosition, ForceMode.Impulse);
    }

    public void BlowUp()
    {
        EventManager.PlayerDeath(); // onPlayerDeathEvent
        //call particles
        GameObject temp =  Instantiate(blowUp, transform.position, Quaternion.identity)as GameObject;
        // add points to score
        //destory particle system
        Destroy(temp, 3f);

        //destroy
        Destroy(gameObject);
    }

}
