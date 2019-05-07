using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInfo : MonoBehaviour
{
    float weight;
    [HideInInspector]
    public bool hasRoof = false;
    int layermask;
    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Roof");
        weight = Random.Range(0, 1);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.up,out hit, Mathf.Infinity, layermask))
        {
            hasRoof = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
