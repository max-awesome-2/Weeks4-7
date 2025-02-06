using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Slider s;

    private float t;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        s.value = t % s.maxValue;
    }
}
