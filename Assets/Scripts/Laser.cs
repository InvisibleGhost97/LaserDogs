using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {
    [SerializeField]
    float LaserOffTime = .5f;
    [SerializeField]
    float maxDistance = 300f;
    [SerializeField]
    float fireDelay = 2f;
    LineRenderer lr;
    Light laserLight;
    bool canFire;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }
    // Use this for initialization
    void Start () {
        lr.enabled = false;
       // laserLight.enabled = false;
        canFire = true;
	}
	
	// Update is called once per frame
	void Update () {
        FireLaser(transform.forward * maxDistance);
	}

    Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("We hit: " + hit.transform.name);
            SpawnExplosion(hit.point, hit.transform); 
            

            //hit.transform.GetComponent<Explosion>().HittiedMe(hit.point);
            return hit.point;

        }
        Debug.Log("We missed");
        return transform.position + (transform.forward * maxDistance);
    }

    void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        Explosion temp = target.GetComponent<Explosion>();
        if (temp != null)
        {
            //temp.HittiedMe(hitPosition);
            temp.AddForce(hitPosition, transform);
        }
    }


    public void FireLaser()
    {
        // Vector3 pos = CastRay();
        // FireLaser(pos);

        FireLaser(CastRay());
    }


    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if (canFire)
        {
            if (target != null)
            {
                SpawnExplosion(targetPosition, target);
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPosition);
            lr.enabled = true;
           // laserLight.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", LaserOffTime);
            Invoke("CanFire", fireDelay);

        }
        

    }

    void TurnOffLaser()
    {
        lr.enabled = false;
       // laserLight.enabled = false;

    }

    public float Distance
    {
        get { return maxDistance; }
    }
    void CanFire()
    {
        canFire = true;
    }
}
