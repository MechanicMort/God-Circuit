using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject projectile;


 public void Fire(float damageMod, float fireRateMod)
    {
        print(damageMod + " : " + fireRateMod);
    }
    public void Reload()
    {
        print("Reloading");
    }
}
