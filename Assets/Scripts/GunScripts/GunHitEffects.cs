using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHitEffects : MonoBehaviour {

    private GunMaster gunMaster;
    public GameObject defaultHitEffects;
    //public GameObject enemyHitEffect;


    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotDefault += SpawnDefaultHitEffect;
        //gunMaster.EventShotEnemy += SpawnEnemyHitEffect;

    }

    void OnDisable()
    {
        gunMaster.EventShotDefault -= SpawnDefaultHitEffect;
        //gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;

    }


    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();

    }
    void SpawnDefaultHitEffect(Vector3 hitPosition, Transform hitTransform)
    {
        if (defaultHitEffects != null)
        {
            Instantiate(defaultHitEffects, hitPosition, Quaternion.identity);

        }
    }

    //void SpawnEnemyHitEffect(Vector3 hitPosition, Transform hitTransform)
    //{
    //    if (enemyHitEffect != null)
    //    {
    //        Instantiate(enemyHitEffect, hitPosition, Quaternion.identity);

    //    }
    //}

}
