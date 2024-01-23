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
    public float magCap;
    public float roundsRemaining;
    public bool needsToCharge;
    private WeaponAnims weaponAnims;



    private void Awake()
    {
        weaponAnims = GetComponent<WeaponAnims>();
        StartCoroutine(FireRate());
    }


    public void Charge()
    {
        needsToCharge = false;
    }
    public void Reload()
    {
        if (roundsRemaining == 0)
        {
            roundsRemaining = magCap;
            needsToCharge = true;
        }
        else
        {
            roundsRemaining = magCap + 1;
        }
    }
    private IEnumerator FireRate()
    {
        fireRateTicker -= 0.01f;
        fireRateTicker = Mathf.Clamp(fireRateTicker, 0, 100);
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(FireRate());
    }

    public void CreatePorjectile(GameObject projectile)
    {
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = gunBarrel.transform.position;
        bullet.transform.rotation = gunBarrel.transform.rotation;
        bullet.GetComponent<Projectile>().damage *= damageMod;
        roundsRemaining--;
        weaponAnims.HasFired();

    }

    public bool FireWeapon( )
    {
        if (fireRateTicker == 0 && roundsRemaining !=0 && !needsToCharge)
        {
            fireRateTicker = fireRate;     
            weaponAnims.WeaponFired();
            //add last round anim at some point
            return true;
        }
        else
        {
            return false;
        }

    }
}
