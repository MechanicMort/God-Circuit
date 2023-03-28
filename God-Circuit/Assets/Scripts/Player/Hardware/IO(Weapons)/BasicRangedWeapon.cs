using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedWeapon : MonoBehaviour
{
    public Transform gunBarrel;
    // FIRE RATE IS TIME INBETWEEN SHOTS  
    public float fireRate;
    public float fireRateTicker = 0;
    public string emmisionType = "PROJECTILE";
    public float damageMod;



    private void Awake()
    {
        StartCoroutine(FireRate());
    }

    private IEnumerator FireRate()
    {
        fireRateTicker -= 0.1f;
        fireRateTicker = Mathf.Clamp(fireRateTicker, 0, 100);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FireRate());
    }

    public bool FireWeapon(GameObject projectile)
    {

        if (fireRateTicker == 0)
        {
            fireRateTicker = fireRate;
            GameObject bullet = Instantiate(projectile);
            bullet.transform.position = gunBarrel.transform.position;
            bullet.transform.rotation = gunBarrel.transform.rotation;
            bullet.GetComponent<Projectile>().damage *= damageMod;
            print("WeaponFired");
            return true;
        }
        else
        {
            return false;
        }

    }
}
