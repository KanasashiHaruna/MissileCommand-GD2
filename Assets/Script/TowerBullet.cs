using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TowerBullet : MonoBehaviour
{
    //public NewBehaviourScript gameManager;

    private float speed = 5;
    private Vector3 targetPosition;
    public explosion explosion;
    public TowerBullet target;

    [SerializeField]
    public ParticleSystem particle;
    public ParticleSystem newParticle;

    // Start is called before the first frame update
    void Start()
    {
        newParticle.Play();
    }

    public void SettUp(Vector3 clickPosition)
    {
        targetPosition= clickPosition;  

    }

    // Update is called once per frame
    void Update()
    {
        //���݂̈ʒu���擾
        Vector3 currentPosition = transform.position;
        
        //�ړI�n�_�ւ̕����̌v�Z
        Vector3 direction = (targetPosition - currentPosition).normalized;

        //�ړ��������v�Z
        Vector3 move = direction * speed * Time.deltaTime;
        
        //�I�u�W�F�N�g�̈ړ�
        transform.position += move;

        //��]
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        //�ړI�n�ɂ�����
        if(Vector2 .Distance(currentPosition,targetPosition) < 0.1f)
        {
            Destroy(gameObject);
            GameObject targe = GameObject.FindGameObjectWithTag("target");
            Destroy(targe);
            explosion obj = Instantiate(explosion, transform.position, Quaternion.identity);

        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("target"))
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(this.gameObject);
    //
    //        explosion obj = Instantiate(explosion, transform.position, Quaternion.identity);
    //        //obj.SettUp(targetPosition);
    //    }
    //}
}
