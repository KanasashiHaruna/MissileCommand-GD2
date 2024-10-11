using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //カメラ----------------------
    private Vector3 originalPosition;      //カメラの元の位置
    private float shakeDuration = 0.0f;  　//シェイクの持続時間
    private float shakeMagnitude = 0.0f;　 //シェイクの強度
    private float initialShakeDuration;    //初期のシェイクの持続時間

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラのシェイク------------------------------------------
        if (shakeDuration > 0.0f)
        {

            //持続時間が残っている時カメラの位置を変更
            float currentMagnitude = shakeMagnitude * (shakeDuration / initialShakeDuration);
            transform.position = originalPosition + Random.insideUnitSphere * currentMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        initialShakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
