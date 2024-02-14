using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform target;
    public float distanceNeededEachMove;
    public float timeTakenToMove;
    public void MoveTheCam()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distanceNeededEachMove = Vector3.Distance(transform.position, target.position) / (timeTakenToMove / 0.01f);
        StartCoroutine(MoveTheCamTimer());
    }

    private IEnumerator MoveTheCamTimer()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, distanceNeededEachMove);
        yield return new WaitForSeconds(0.01f);
        if (Vector3.Distance(transform.position,target.position) < 0.1f)
        {
            target.gameObject.SetActive(true);
            target.parent.GetComponent<BaseOverWorldController>().canMove = true;
            target.parent.GetComponent<BaseOverWorldController>().canTurn = true;
            transform.gameObject.SetActive(false);
            
        }
        else
        {
            StartCoroutine(MoveTheCamTimer());
        }
    }
}
