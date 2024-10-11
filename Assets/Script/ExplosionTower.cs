using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExplosionTower : explosion
{
    public override void Explosion()
    {
        float scaleSpeed = 2.0f; //大きくなるスピード
        Vector3 newScale = transform.localScale;
        newScale.x += scaleSpeed * Time.deltaTime;
        transform.localScale = newScale;

        // 現在の位置を取得し、z座標を変更する
        Vector3 newPosition = transform.position;
        newPosition.z = 1.0f;
        transform.position = newPosition;

        if (transform.localScale.x >= 4.0f)
        {
            Destroy(gameObject);
        }
    }
}
