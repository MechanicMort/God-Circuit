using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedWeapon : MonoBehaviour
{
    public Transform gunBarrel;
  public void FireWeapon(GameObject projectile)
    {
        GameObject bullet = Instantiate(projectile);
        print("WeaponFired");
    }
}
