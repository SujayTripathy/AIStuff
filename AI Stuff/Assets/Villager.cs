using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{

    Vector3 dir;
    float initial;
    int point;
    int layermask;
    [HideInInspector]
    public List<GameObject> points = new List<GameObject>(50);
    [SerializeField]
    float movement = 10;
    [SerializeField]
    float speed = 5;
    float basespeed;

    

    // Start is called before the first frame update
    void Start()
    {
        initial = -movement;
        layermask = LayerMask.GetMask("Roof");
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Roof"))
        {
            points.Add(p.GetComponentInChildren<PointInfo>().gameObject);
        }
        basespeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count != 0)
        {
            if (Time.time - initial > movement)
            {
                initial = Time.time;
                point = Random.Range(0, points.Count);
                gameObject.transform.LookAt(points[point].transform);
                dir = (points[point].transform.position - gameObject.transform.position).normalized;
                //Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + dir * 10, Color.red, Mathf.Infinity);
            }
            if ((gameObject.transform.position - points[point].transform.position).magnitude >= 1)
            {
                gameObject.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
                Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + dir * speed, Color.red, Mathf.Infinity);
            }
            if (GameObject.FindGameObjectWithTag("Weather").GetComponent<Weather>().current == "Rain")
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up,out hit,Mathf.Infinity,layermask))
                {
                    Debug.Log("I'm safe");
                    speed = speed * .06f;
                }
                else
                {
                    Debug.Log("I'm getting wet");
                    if (!points[point].GetComponent<PointInfo>().hasRoof)
                    {
                        foreach(GameObject p in points)
                        {
                            if (p.GetComponent<PointInfo>().hasRoof)
                            {
                                dir = (p.transform.position - gameObject.transform.position).normalized;
                            }
                        }
                    }
                }
            }
            else
            {
                speed = basespeed;
                Debug.Log("It's sunny");
            }
        }
    }
}
