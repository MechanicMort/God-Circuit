using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
