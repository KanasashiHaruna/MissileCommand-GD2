using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    Vector3 clickPosition;
    private GameObject cursor;

    private float rotateSpeed = 300.0f;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotateSpeed * Time.deltaTime;
        transform.rotation=Quaternion.Euler(0,0,angle);
    }

    
}
