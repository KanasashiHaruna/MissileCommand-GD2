using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class meteo : MonoBehaviour
{
    
    //����Point
    private Vector3 targetPosition;
    public float speed = 2;
    private float x1;
    private float min, max;

    //�V�F�C�N
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
        //���e�I�̈ړ�----------------------------------------------

        //���݂̈ʒu���擾
        Vector3 currentPosition=transform.position;

        //�ڕW�n�_�ւ̕����̌v�Z
        Vector3 direction =(targetPosition-currentPosition).normalized;

        direction.z = 0.0f;

        //�ړ��������v�Z
        Vector3 move =direction*speed*Time.deltaTime;

        //�I�u�W�F�N�g�̈ړ�
        transform.position += move;

        
    }

    //�����蔻��-------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("���e�I�ƒn�ʂ�����������[");
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
