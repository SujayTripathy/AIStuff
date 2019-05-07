using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public string current;
    float change = 20;
    float initial;
    public GameObject rain;
    // Start is called before the first frame update
    void Start()
    {
        current = "Clear";
        initial = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - initial >= change)
        {
            NextWeather();
            initial = Time.time;
            if (current =="Clear")
            {

            }
            if (current == "Rain")
            {
                GameObject water=Instantiate(rain, new Vector3(0, -230, 0), new Quaternion());
                Destroy(water, 20);
            }
            if (current == "Cloud")
            {

            }          

        }
    }
    void NextWeather()
    {
        int determine = Random.Range(0, 2);
        if (determine == 0)
        {
            current = "Clear";
        }
        if (determine == 1)
        {
            current = "Rain";
        }
        if (determine == 2)
        {
            current = "Cloud";
        }
    }
}
