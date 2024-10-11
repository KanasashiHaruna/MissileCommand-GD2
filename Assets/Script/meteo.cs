using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class meteo : MonoBehaviour
{
    
    //発射Point
    private Vector3 targetPosition;
    public float speed = 2;
    private float x1;
    private float min, max;

    //シェイク
    private bool isShake = false;

    //
    private NewBehaviourScript gameManager;
    [SerializeField]
    //public ParticleSystem particle;
    public ParticleSystem newParticle;
    private Tower tower;
    public explosion explosion;
    public void SettUp(SpriteRenderer sp, NewBehaviourScript gm)
    {
        min = sp.bounds.min.x;
        max = sp.bounds.max.x;
        gameManager = gm;
    }

    // Start is called before the first frame update



    void Start()
    {
        x1 = Random.Range(min, max);
        targetPosition = new Vector2(x1, -14.3f);
        //Debug.Log(new Vector2(x1, -14.3f));

        newParticle.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        //メテオの移動----------------------------------------------

        //現在の位置を取得
        Vector3 currentPosition=transform.position;

        //目標地点への方向の計算
        Vector3 direction =(targetPosition-currentPosition).normalized;

        direction.z = 0.0f;

        //移動距離を計算
        Vector3 move =direction*speed*Time.deltaTime;

        //オブジェクトの移動
        transform.position += move;

        
    }

    //当たり判定-------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("メテオと地面が当たったよー");
            Destroy(this.gameObject,3.0f);

            newParticle.Stop();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false; 
            
            isShake = true;

            if (isShake == true)
            {
                gameManager.TriggerShake();
            }
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            gameManager.meteoExShake();
            Destroy(this.gameObject);
            explosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
            gameManager.ScoreAdd();
        }
    }
}
