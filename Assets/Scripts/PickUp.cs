using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class PickUp : MonoBehaviour {
    public int points = 100;

    [SerializeField]
    float rotationOffset = 50f;

    Transform myT;
    Vector3 randomRotation;

    bool gotHit = false;

    void Awake()
    {
        myT = transform;
    }



    void Start()
    {
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);

    }

    void Update()
    {
        myT.Rotate(randomRotation * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if (!gotHit)
            {
                gotHit = true;
                PickUpHit();
            }
            
        }
    }
	

    public void PickUpHit()
    {
        EventManager.ScorePoints(points);
        EventManager.SpawnPickup();
        Destroy(gameObject);
    }
}
