using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Ocean : MonoBehaviour
{
    private WaterSurface surface;
    private WaterSearchParameters searchParameters;
    private WaterSearchResult result;
    // Start is called before the first frame update
    void Start()
    {
        surface = GameObject.FindGameObjectWithTag("Ocean").GetComponent<WaterSurface>();
        searchParameters = new WaterSearchParameters();
        result = new WaterSearchResult();
       // result;
       //  surface.FindWaterSurfaceHeight(searchParameters,out result);
     
    }

    // Update is called once per frame
    void Update()
    {

        searchParameters.startPosition = result.candidateLocation;
        searchParameters.targetPosition = transform.position;

        searchParameters.error = 0.01f;
        searchParameters.maxIterations = 8;

        if (surface.FindWaterSurfaceHeight(searchParameters,out result))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, result.height, gameObject.transform.position.z);
        }


    }
}
