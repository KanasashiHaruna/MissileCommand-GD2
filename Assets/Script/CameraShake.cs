using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //�J����----------------------
    private Vector3 originalPosition;      //�J�����̌��̈ʒu
    private float shakeDuration = 0.0f;  �@//�V�F�C�N�̎�������
    private float shakeMagnitude = 0.0f;�@ //�V�F�C�N�̋��x
    private float initialShakeDuration;    //�����̃V�F�C�N�̎�������

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̃V�F�C�N------------------------------------------
        if (shakeDuration > 0.0f)
        {

            //�������Ԃ��c���Ă��鎞�J�����̈ʒu��ύX
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
