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

    private float time = 0;  //弾の処理
    private float startTime = 1.0f;

    //explosionのTime
    private float explosionTime = 0.0f;
    private float startExTime = 0.5f;

    public List<GameObject> spawnPoints = new List<GameObject>();  //発射位置のリスト
    public List<Tower> Tower = new List<Tower>();

    private bool isAlive = true;
    Tower towerPoint;

    [SerializeField]
    SpriteRenderer ground;   //下のステージ
    //Groundのライフ
    public float Life = 100.0f;
    private bool isLife = true;
    public Slider slider;


    private Vector3 clickPosition;

    //スコア---------------------------------------
    private int plusScore = 0; //スコアの加算用
    public Text scoreText;

    //フェードイン----------------------------------
    public CanvasGroup canvasGroup;
    private bool isFade = false;
    private float fadeSpeed = 0.5f; //フェードの速さ

    public Text gameover;  //フェード終わった後のテキスト
    
    // Start is called before the first frame update
    void Start()
    {
        plusScore = 0; // 初期化
        scoreText.text = "SCORE：" + plusScore.ToString("d8");
        canvasGroup.alpha = 0;  //初期は透明
        gameover.gameObject.SetActive(false);
    }

    public void ScoreAdd()
    {
        plusScore = plusScore + 100;
        //Debug.Log("Score added: " + plusScore); 
        scoreText.text = "SCORE：" + plusScore.ToString("d8");
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
        //カメラの範囲を取得
        Camera mainCamera=Camera.main;
        Vector3 bottomLeft=mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));


        //X,Yでランダムな位置
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);

        return new Vector3(randomX, randomY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = Life;


        #region レティクル
        //レティクルの生成-------------------------------------------------
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
        #region 弾関係
        //弾の処理---------------------------------------------------------
        time += Time.deltaTime;
        if (time >= startTime)
        {
            //ランダムに発射位置を選択------------------------------
            int index = Random.Range(0, spawnPoints.Count);
            GameObject spawnPoint = spawnPoints[index];

            if (isLife)
            {
                // 弾の生成---------------------------------------------
                obj = Instantiate(meteo, spawnPoint.transform.position, Quaternion.identity);
                obj.SettUp(ground, this);
            }

            time = time - startTime;
        }
        #endregion

        #region タワーが全部消えたら
        for (int i = 0; i < Tower.Count; i++)
        {
            if (Tower[i] != null)//じゃなかったら
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
            //Debug.Log("全部消えたよー");
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

                //フェードイン開始
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


    //Groundとmeteoが当たった時のシェイク
    public void TriggerShake()
    {
        cameraShake.StartShake(0.5f, 0.2f);
        Life = Life - 10.0f;

    }
    //Towerが壊れたときのシェイク
    public void explosionShake()
    {
        cameraShake.StartShake(2.0f, 1.0f);
    }
    //meteoとexplosionが当たった時のシェイク
    public void meteoExShake()
    {
        cameraShake.StartShake(0.1f, 0.2f);
    }
}
