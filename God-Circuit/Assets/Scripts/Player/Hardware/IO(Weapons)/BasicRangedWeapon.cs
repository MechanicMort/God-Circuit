using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedWeapon : MonoBehaviour
{
    public Transform gunBarrel;
    // FIRE RATE IS TIME INBETWEEN SHOTS  
    public float fireRate;
    public string emmisionType = "PROJECTILE";



    private void Awake()
    {
        
    }

    private IEnumerator FireRate

    public void FireWeapon(GameObject projectile)
    {
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = gunBarrel.transform.position;
        bullet.transform.rotation = gunBarrel.transform.rotation;
        print("WeaponFired");
    }
}
