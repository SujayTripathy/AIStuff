using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{

    Vector3 dir;
    float initial;
    float initialhouse;
    int point;
    int layermask;
    int ground;
    public GameObject house;
    [HideInInspector]
    public List<GameObject> points = new List<GameObject>(50);
    [SerializeField]
    float movement = 10;
    [SerializeField]
    float checkdistance = 30;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float sight = 20;
    float basespeed;
    public Material dry;
    public Material raincoat;
    NavMeshAgent agent;
    float housebuilding = 20;
    public GameObject dirtTrail;
    RaycastHit hitground;

    
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initial = -movement;
        initialhouse = -housebuilding;
        ground = LayerMask.GetMask("Floor");
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
                agent.SetDestination(points[point].transform.position);
            }
            if (GameObject.FindGameObjectWithTag("Weather").GetComponent<Weather>().current == "Rain")
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up,out hit,Mathf.Infinity,layermask))
                {
                    agent.speed = speed * .06f;
                    gameObject.GetComponent<MeshRenderer>().material = dry;
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().material = raincoat;
                    if (!points[point].GetComponent<PointInfo>().hasRoof)
                    {
                        foreach(GameObject p in points)
                        {
                            if (p.GetComponent<PointInfo>().hasRoof)
                            {
                                agent.destination = p.transform.position;
                            }
                        }
                    }
                }
            }
            else
            {
                agent.speed = basespeed;
                gameObject.GetComponent<MeshRenderer>().material = dry;
            }
        }
        ///Spawn Dirt Trail
        if(Physics.Raycast(transform.position,-transform.up,out hitground, Mathf.Infinity, ground))
        {
            GameObject dirt = Instantiate(dirtTrail, hitground.point,new Quaternion());
            dirt.transform.localEulerAngles = new Vector3(90, 0, 0);
        }
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        ///Check if there is space to build houses in area
        if (other.transform.tag == "BuildingZone")
        {
            if (Time.time - initialhouse > housebuilding)
            {
                Collider[] houses = Physics.OverlapSphere(gameObject.transform.position, checkdistance, layermask);
                if (houses.Length == 0)
                {
                    initialhouse = Time.time;
                    Instantiate(house, transform.position, new Quaternion());
                }
            }
        }
    }
}
