using System.Collections;
using UnityEngine;

public class BirbPlayer : MonoBehaviour
{


    private Rigidbody rb;


    public Vector3 momentum;
    [SerializeField]
    private float flapSpeed;
    [SerializeField]
    private float diveSpeed;
    private bool flapped = false;
    private bool flying = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        momentum = rb.linearVelocity;
        if (flying)
        {
            if (Input.GetKeyDown(KeyCode.Space)) { Flap(); }
          
        }
        else{

        }
        
    }

    private IEnumerator flapCool() {
        yield return new WaitForSeconds(0.1f);
        flapped = false;
            }

    public Vector3  Lean()
    {
         Vector3 leanDir = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W)) { leanDir.x += 15;Dive(); }
        else if (Input.GetKey(KeyCode.S)) { leanDir.x -=15;Glide(); }

        if (Input.GetKey(KeyCode.A)) { leanDir.z += 15; }
        else if (Input.GetKey(KeyCode.D)) { leanDir.z -= 15; }
        // else if (Input.GetKey(KeyCode.S)) { transform.Rotate(new Vector3(1, 0, 0), -15); }

        else
        {

        }

        
        return leanDir;
    }


    public void FixedUpdate()
    {
        // momentum.x = Mathf.Clamp(momentum.x, -50f, 100f);
        //  momentum.y = Mathf.Clamp(momentum.x, -50f, 100f);
         Vector3 dir = Lean();
        print (dir.normalized);
        print(momentum);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(dir), 0.1f);
        transform.Rotate(new Vector3(0,0,0));
        momentum = new Vector3(momentum.x + -dir.normalized.z,momentum.y,momentum.z);
        rb.linearVelocity = momentum;
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 70);
    }

    public void TakeOff() { }

    public void Land() { }
    public void Flap()
    {
        momentum.y += flapSpeed;
        momentum.z += flapSpeed/6;
        flapped = true;
        StartCoroutine(flapCool());
    }

    public void Dive() {
        momentum.y += diveSpeed;
        momentum.z += diveSpeed * Time.deltaTime * 2;
    }

    public void Glide() {
        if (!flapped)
        {
            momentum.y = Mathf.Lerp(rb.linearVelocity.y, -0.5f, 2f);
            momentum.z = Mathf.Lerp(rb.linearVelocity.z, 5f, 2f);
            
        }

    }
}
