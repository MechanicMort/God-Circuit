using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnims : MonoBehaviour
{

    public Animator anim;
    public AnimationClip RealoadClip;
    public AnimationClip RealoadEmptyClip;
    public AnimationClip FireClip;
    public AnimationClip MagCheckClip;
    public AnimationClip ChargeClip;
    public AnimationClip FireSelectAutoClip;
    public AnimationClip FireSelectSemiClip;
    public GameObject weaponsCase;
    public GameObject weaponsBullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");

        }
            if (Input.GetKeyDown(KeyCode.V))
        {

            anim.SetTrigger("Charge");
        }
    }
}
