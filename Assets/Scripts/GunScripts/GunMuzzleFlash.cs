using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

public class GunMuzzleFlash : NetworkBehaviour
{

    [SerializeField]
    ParticleSystem muzzleFlash;
    [SerializeField]
    GunMaster gunMaster;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventPlayerInput += PlayMuzzleFlash;

    }

    void OnDisable()
    {
        SetInitialReferences();
        gunMaster.EventPlayerInput -= PlayMuzzleFlash;

    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
    }

	void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
            Debug.Log("playing");
        }
    }
}
