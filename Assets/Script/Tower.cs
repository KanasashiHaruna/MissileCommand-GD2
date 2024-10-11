using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerBullet bullet;
    public meteo meteo;
    public ExplosionTower explosion;
    public NewBehaviourScript gameManager;
    public ParticleSystem newParticle;

    private Vector3 targetPosition;

    private float coolTime = 0.0f;    //クールタイム
    [SerializeField] private float startTime = 5.0f;

    private Color startColor;
    private Color endColor = Color.red;
    private SpriteRenderer spriteRenderer;

    public float Hp = 3.0f;

    public bool isAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        newParticle.Play();
        spriteRenderer = GetComponent<SpriteRenderer>();  //初めの色を取得
        startColor = spriteRenderer.color;
    }
    
    public void Shot(Vector3 clickPosition)
    {
        if (isAttack == true)
        {
            targetPosition = clickPosition;
            TowerBullet obj = Instantiate(bullet, transform.position, Quaternion.identity);
            obj.SettUp(targetPosition);
            isAttack = false;
            coolTime = 0.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == false)
        {
            coolTime += Time.deltaTime;
            float t = coolTime / startTime;
            spriteRenderer.color=Color.Lerp(endColor, startColor, t);
            if (coolTime >= startTime)
            {
                spriteRenderer.color = startColor;
                isAttack = true;
                coolTime = coolTime - startTime;
            }
        }

        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            ExplosionTower obj = Instantiate(explosion, transform.position, Quaternion.identity);
            gameManager.explosionShake();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("meteo"))
        {
           //Debug.Log("メテオとタワーがあたったよー");
            Destroy(collision.gameObject,1.0f);
            newParticle.Stop();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Hp = Hp - 1;
        }
    }
}
