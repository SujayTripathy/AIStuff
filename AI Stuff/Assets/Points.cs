using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    float time = 10;
    float initial;
    GameObject[] villagers;
    [SerializeField]
    GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        initial = Time.time;
        villagers = GameObject.FindGameObjectsWithTag("Villager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - initial >= time)
        {
            initial = Time.time;
            GameObject currentpoint= Instantiate(point, new Vector3(Random.Range(-200, 200), 1.6f, Random.Range(-200, 200)), new Quaternion());
            foreach(GameObject v in villagers)
            {
                v.GetComponent<Villager>().points.Add(currentpoint);
            }
        }
    }
}
