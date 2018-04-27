using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//should be attached to gun rifle
public class GunMaster : MonoBehaviour {


    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventPlayerInput;
    public event GeneralEventHandler EventGunNotUsable;
    public event GeneralEventHandler EventRequestReload;
    public event GeneralEventHandler EventRequestGunReset;
    public event GeneralEventHandler EventToggleBurstFire;

    public delegate void GunHitEventHandler(Vector3 hitposition, Transform hitTrasform);
    public event GunHitEventHandler EventShotDefault;
    public event GunHitEventHandler EventShotEnemy;

    public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
    public event GunAmmoEventHandler EventAmmoChanged;

    public delegate void GunCrosshairEventHandler(float speed);
    public event GunCrosshairEventHandler EventSpeedCaptured;

    public bool isGunLoaded;
    public bool isReloading;

    public void CallEventPlayerInput()
    {
        if (EventPlayerInput!= null)
        {
            EventPlayerInput();
        }
    }

    public void CallEventGunNotUsable()
    {
        if (EventGunNotUsable != null)
        {
            EventGunNotUsable();
        }
    }

    public void CallEventRequestreload()
    {
        if (EventRequestReload != null)
        {
            EventRequestReload();
        }
    }

    public void CallEventRequestGunReset()
    {
        if (EventRequestGunReset != null)
        {
            EventRequestGunReset();
        }
    }

    public void CallEventToggleBurstFire()
    {
        if (EventToggleBurstFire != null)
        {
            EventToggleBurstFire();
        }
    }

    public void CallEventShotDefault(Vector3 hPos, Transform hTransform)
    {
        if (EventShotDefault != null)
        {
            EventShotDefault(hPos, hTransform);
        }
    }

    public void CallEventShotEnemy(Vector3 hPos, Transform hTransform)
    {
        if (EventShotEnemy != null)
        {
            EventShotEnemy(hPos, hTransform);
        }
    }

    public void CallEventAmmoChanged(int curAmmo, int carryAmmo)
    {
        if (EventAmmoChanged != null)
        {
            EventAmmoChanged(curAmmo, carryAmmo);
        }
    }

    public void CallEventSpeedCaptured(float spd)
    {
        if (EventSpeedCaptured != null)
        {
            EventSpeedCaptured(spd);
        }
    }

}
