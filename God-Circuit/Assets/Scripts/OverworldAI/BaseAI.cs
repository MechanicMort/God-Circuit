using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{

    public List<Transform> localNodes = new List<Transform>();
    public Transform currentTarget;
    public NavMeshAgent myAgent;

    // Start is called before the first frame update
    void Start()
    {
        StartOperations();
    }

    public void StartOperations()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.enabled = true;
        currentTarget = myAgent.transform;
        myAgent.SetDestination(currentTarget.position);
        GetLocalNodes();
        StartCoroutine(MoveAI());
    }

    virtual public IEnumerator MoveAI()
    {
        yield return new WaitForSeconds(2);
        if (Vector3.Distance(this.transform.position, currentTarget.transform.position) < 5)
        {
            RandomRoam();
        }
        MoveAI();
    }

    public void GetLocalNodes()
    {
        localNodes.Clear();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("AILocalNodes").Length; i++)
        {
            localNodes.Add(GameObject.FindGameObjectsWithTag("AILocalNodes")[i].transform);
        }
    }

    private void RandomRoam()
    {
        currentTarget = localNodes[Random.Range(0, localNodes.Count)];
   
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.SetDestination(currentTarget.position);
    }
}
