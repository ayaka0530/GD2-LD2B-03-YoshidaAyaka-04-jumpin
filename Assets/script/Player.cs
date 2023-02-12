using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Renderer target;

    public GameObject Bullet;
    public GameObject gameoverText;    //ゲームオーバーと表示するテキストを格納
    public AudioSource jumpAudio;   //音楽を再生するコンポーネント
    public GameObject[] heartArray = new GameObject[3]; //ハートの表示
    public GameObject[] bulletArray = new GameObject[3]; //弾の表示
    public GameObject bombParticle;
    [SerializeField] GameObject dustCloud;//着地エフェクト
    public GameManager gameManager;
    public Rigidbody2D rb;
    public Renderer vertical;

    private float speed = 0.05f;
    private int jumpCount = 0;
    private float jumpForce = 12f;
    private int playerHp = 3;
    private float invincibleTime = 0f;
    private bool isInvincible = false;//trueの時に無敵時間になる
    private bool isDmg = false;
    private int direction = 0;
    private Renderer spriteRenderer;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        vertical = GameObject.Find("Vertical").GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Physics2D.gravity = new Vector2(30f, 0);

    }

    // Update is called once per frame
    void Update()
    {
Vector2 position = transform.position;

            //左右の移動
            if (Input.GetKey("left") || (Input.GetKey(KeyCode.A)))
            {
                position.x -= speed;
                direction = -1;

            }
            else if (Input.GetKey("right") || (Input.GetKey(KeyCode.D)))
            {
                position.x += speed;
                direction = 1;
            }

            if (direction != 0)
            {
                //動く向きに反転
                transform.localScale = new Vector3(direction, 1, 1);
            }
            transform.position = position;


        //ダメージを受けていなかったら動く(無敵時間内じゃない)
        if (isDmg == false && playerHp > 0)
        {
            //ジャンプ
            if (Input.GetKeyDown("space") && this.jumpCount < 3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
                jumpCount++;//ジャンプのカウントを増やす
                GameObject bulletObject = Instantiate(Bullet, transform.position, Quaternion.identity);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                jumpAudio.Play();

                Debug.Log("jupCount : " + jumpCount);

                gameManager.SetDistance(transform.position.y);
            }
            transform.position = position;

            //左右ループ
            if (transform.position.x > 3.4f)
            {
                transform.position = new Vector3(-3.3f, transform.position.y, 0);
            }
            else if (transform.position.x < -3.4f)
            {
                transform.position = new Vector3(3.3f, transform.position.y, 0);
            }
        }
        //無敵時間内
        else if (isDmg == true)
        {
            GetComponent<RendererOnOffExample>().enabled = true;
            //無敵時間のカウントを始める
            invincibleTime += Time.deltaTime;

            //向きでノックバック方向を判断
            if (transform.localScale.x >= 0)
            {
                rb.AddForce(transform.right * -5.0f);
            }
            else
            {
                rb.AddForce(transform.right * 5.0f);
            }

            //無敵時間が終わったら
            if (invincibleTime >= 1)
            {
                invincibleTime = 0;//無敵時間カウントを元に戻す
                isDmg = false;//ダメージ受けていない状態に戻す
                isInvincible = false;//無敵時間の終了
                GetComponent<RendererOnOffExample>().enabled = false;
            }
            //プレイヤーのHPが0になったらゲームオーバー
            if (playerHp <= 0)
            {
                gameoverText.SetActive(true); //無効になって非表示になったゲームオブジェクトを
                spriteRenderer.enabled = false;//プレイヤーのイラストを非表示
                vertical.enabled = false;//プレイヤーのイラストを非表示
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                //titleButton.SetActive(true); //このタイミングで有効にする。
                gameManager.ChangeScene();
            }
        }

        //ハートの表示
        /*if (playerHp == 3)
        {
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerHp == 2)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }
        if (playerHp == 1)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerHp == 0)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(false);
        }

        if (playerHp == 3)
        {
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }*/

        //弾数の表示
        if (jumpCount == 0)
        {
            bulletArray[2].gameObject.SetActive(true);
            bulletArray[1].gameObject.SetActive(true);
            bulletArray[0].gameObject.SetActive(true);
        }
        if (jumpCount == 1)
        {
            bulletArray[2].gameObject.SetActive(false);
            bulletArray[1].gameObject.SetActive(true);
            bulletArray[0].gameObject.SetActive(true);
        }

        if (jumpCount == 2)
        {
            bulletArray[2].gameObject.SetActive(false);
            bulletArray[1].gameObject.SetActive(false);
            bulletArray[0].gameObject.SetActive(true);
        }
        if (jumpCount == 3)
        {
            bulletArray[2].gameObject.SetActive(false);
            bulletArray[1].gameObject.SetActive(false);
            bulletArray[0].gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            //床に降りたらjumpCountを0に
            //jumpCount = 0;
            //床にプレイヤーがあたったらエフェクトを出す
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
            /*GameObject[] enemyObjects =
              GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                Destroy(enemyObjects[i]);
            }
            Instantiate(bombParticle, transform.position, bombParticle.transform.rotation);*/
        }

        if (other.gameObject.tag == "Enemy")
        {
            //敵にあたったらjumpCountを0に
            jumpCount = 0;

            if (isInvincible == false)
            {
                //プレイヤーのHPを１減らす
                //playerHp = playerHp - 1;
                Debug.Log("playerHp" + playerHp);
                isDmg = true;
                isInvincible = true;
            }

            //PlayerオブジェクトのLayerを"Player"から"Invisible"へ変更
            gameObject.layer = LayerMask.NameToLayer("Invisible");

        }
    }
    private IEnumerator dmgFalse()
    {
        //一秒遅延させる
        yield return new WaitForSeconds(1);
        isDmg = false;

        //Layerを"Player"に戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    private void OnDisable()
    {
        target.enabled = true;
    }

}
