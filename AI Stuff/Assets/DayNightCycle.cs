using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localEulerAngles.x);
        //time++;
        transform.Rotate(new Vector3(1, 0, 0));
        //if (transform.rotation.x ==1)
        //{
        //    time = 0;
        //    transform.eulerAngles = new Vector3(time / 100, transform.eulerAngles.y, transform.eulerAngles.z);
        //}
    }
}
