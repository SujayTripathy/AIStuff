using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{

    Vector3 dir;
    float initial;
    int point;
    [SerializeField]
    GameObject[] points;
    float movement = 10;
    float speed = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        initial = -movement;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - initial > movement)
        {
            initial = Time.time;
            point = Random.Range(0, points.Length);
            gameObject.transform.LookAt(points[point].transform);
            dir = (points[point].transform.position - gameObject.transform.position).normalized;
            //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + dir * 10, Color.red, Mathf.Infinity);
        }
        if ((gameObject.transform.position - points[point].transform.position).magnitude >= 1) 
        {
            
            gameObject.transform.Translate(dir.normalized * Time.deltaTime*5,Space.World);
            Debug.DrawLine(gameObject.transform.position, gameObject.transform.position+dir*speed, Color.red, Mathf.Infinity);
        }
        else
        {
           
        }
        
    }
}
