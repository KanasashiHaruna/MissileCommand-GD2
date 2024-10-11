using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Explosion();
    }

    public virtual void Explosion()
    {
        float scaleSpeed = 2.0f; //大きくなるスピード
        transform.localScale+= Vector3.one * scaleSpeed * Time.deltaTime;

        if(transform.localScale.x >= 3.5f)
        {
            Destroy(gameObject);
        }
    }
}
