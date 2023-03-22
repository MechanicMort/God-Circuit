using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour,IEnemyModule
{

    [SerializeField]
    private float Hp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void TakeDamage(float damage)
    {
        print("Damage Taken From Enemy");
        Hp -= damage;
    }
}

public interface IEnemyModule
{
    void TakeDamage(float damage);
}
