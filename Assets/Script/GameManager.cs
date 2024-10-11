using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public meteo meteo;
    public CameraShake cameraShake;
    public target target;
    public explosion explosion;

    private float time = 0;  //�e�̏���
    private float startTime = 1.0f;

    //explosion��Time
    private float explosionTime = 0.0f;
    private float startExTime = 0.5f;

    public List<GameObject> spawnPoints = new List<GameObject>();  //���ˈʒu�̃��X�g
    public List<Tower> Tower = new List<Tower>();

    private bool isAlive = true;
    Tower towerPoint;

    [SerializeField]
    SpriteRenderer ground;   //���̃X�e�[�W
    //Ground�̃��C�t
    public float Life = 100.0f;
    private bool isLife = true;
    public Slider slider;


    private Vector3 clickPosition;

    //�X�R�A---------------------------------------
    private int plusScore = 0; //�X�R�A�̉��Z�p
    public Text scoreText;

    //�t�F�[�h�C��----------------------------------
    public CanvasGroup canvasGroup;
    private bool isFade = false;
    private float fadeSpeed = 0.5f; //�t�F�[�h�̑���

    public Text gameover;  //�t�F�[�h�I�������̃e�L�X�g
    
    // Start is called before the first frame update
    void Start()
    {
        plusScore = 0; // ������
        scoreText.text = "SCORE�F" + plusScore.ToString("d8");
        canvasGroup.alpha = 0;  //�����͓���
        gameover.gameObject.SetActive(false);
    }

    public void ScoreAdd()
    {
        plusScore = plusScore + 100;
        //Debug.Log("Score added: " + plusScore); 
        scoreText.text = "SCORE�F" + plusScore.ToString("d8");
    }

    public Tower ShotSet()
    {
        foreach (Tower towerPoint in Tower)
        {
            if (towerPoint.isAttack == true)
            {
                return towerPoint;
            }
        }

        return null;
    }

    
    private Vector3 CameraRandomPosition()
    {
        //�J�����͈̔͂��擾
        Camera mainCamera=Camera.main;
        Vector3 bottomLeft=mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));


        //X,Y�Ń����_���Ȉʒu
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);

        return new Vector3(randomX, randomY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = Life;


        #region ���e�B�N��
        //���e�B�N���̐���-------------------------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
            clickPosition.z = Camera.main.nearClipPlane;
            towerPoint = ShotSet();

            if (towerPoint != null)
            {
                target reticle = Instantiate(target, Camera.main.ScreenToWorldPoint(clickPosition), Quaternion.identity);
                clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
                //Debug.Log(clickPosition);

                towerPoint.Shot(clickPosition);
            }
        }

        #endregion

        meteo obj = null;
        #region �e�֌W
        //�e�̏���---------------------------------------------------------
        time += Time.deltaTime;
        if (time >= startTime)
        {
            //�����_���ɔ��ˈʒu��I��------------------------------
            int index = Random.Range(0, spawnPoints.Count);
            GameObject spawnPoint = spawnPoints[index];

            if (isLife)
            {
                // �e�̐���---------------------------------------------
                obj = Instantiate(meteo, spawnPoint.transform.position, Quaternion.identity);
                obj.SettUp(ground, this);
            }

            time = time - startTime;
        }
        #endregion

        #region �^���[���S����������
        for (int i = 0; i < Tower.Count; i++)
        {
            if (Tower[i] != null)//����Ȃ�������
            {
                isAlive = true;
                break;
            }
            else
            {
                isAlive = false;
            }
        }
        if (isAlive == false)
        {
            //Debug.Log("�S����������[");
            startTime = 0.5f;
            if (obj != null)
            {
                obj.speed = 5.0f;
            }
        }
        #endregion


        if (Life <= 0.0f)
        {
            explosionTime += Time.deltaTime;
            if (explosionTime >= startExTime)
            {
                if (isFade)
                {
                    Vector3 randomPosition = CameraRandomPosition();
                    explosion obi = Instantiate(explosion, randomPosition, Quaternion.identity);
                    explosionTime = explosionTime - startExTime;
                }

                //�t�F�[�h�C���J�n
                isFade = true;
                isLife = false;
            }

        }

        if (isFade)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1.0f)
            {
                canvasGroup.alpha = 1.0f;
                gameover.gameObject.SetActive(true);
                isFade = false;
                
            }
        }
    }


    //Ground��meteo�������������̃V�F�C�N
    public void TriggerShake()
    {
        cameraShake.StartShake(0.5f, 0.2f);
        Life = Life - 10.0f;

    }
    //Tower����ꂽ�Ƃ��̃V�F�C�N
    public void explosionShake()
    {
        cameraShake.StartShake(2.0f, 1.0f);
    }
    //meteo��explosion�������������̃V�F�C�N
    public void meteoExShake()
    {
        cameraShake.StartShake(0.1f, 0.2f);
    }
}
