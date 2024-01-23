using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAnims : MonoBehaviour
{

    public Animator anim;
    public GameObject weaponsCase;
    public GameObject weaponsBullet;
    public GameObject muzzleFlash;
    public float flashChance;
    private BasicRangedWeapon BasicRangedWeapon;
    private Rigidbody rgb;
    [SerializeField]
    private float recoil;

    // Start is called before the first frame update
    void Start()
    {
       
        rgb = GetComponent<Rigidbody>();
        BasicRangedWeapon = GetComponent<BasicRangedWeapon>();
    }

    public void OnCanvasGroupChanged()
    {
        
    }

    public void HasFired()
    {
       // anim.set
    }
    public void WeaponFired()
    {
        //    print("FirAnim");
         anim.SetTrigger("Shot");
    //    rgb.AddForce(transform.forward * recoil);
        if (Random.Range(0, flashChance) == 1) {
            for (int i = 0; i < muzzleFlash.GetComponentsInChildren<ParticleSystem>().Length; i++)
            {
                muzzleFlash.GetComponentsInChildren<ParticleSystem>()[i].Play();
            }
        } ;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            BasicRangedWeapon.Reload();

        }
            if (Input.GetKeyDown(KeyCode.V))
        {

            anim.SetTrigger("Charge");
            BasicRangedWeapon.Charge();
        }
    }
}
