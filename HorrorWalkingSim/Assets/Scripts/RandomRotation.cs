using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Quaternion q;

    void Start()
    {
        Vector3 v;

        v.x = -90.0f;
        v.y = 0.0f; ;
        v.z = Random.Range(0.0f, 360.0f);

        q = Quaternion.Euler(v);

        transform.rotation = q;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
