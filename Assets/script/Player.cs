using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject gameoverText;    //レベルクリアと表示するテキストを格納
    public GameObject titleButton;   //次のレベルへ遷移するボタンを格納
    public AudioSource gameoverAudio;   //音楽を再生するコンポーネント
    public GameObject[] heartArray = new GameObject[3]; //ハートの表示
    public Rigidbody2D rb;


    private float speed = 0.05f;
    private int jumpCount = 0;
    private float jumpForce = 15f;
    private int playerHp = 3;
    private float invincibleTime = 0f;
    private bool isInvincible = false;//trueの時に無敵時間になる
    private bool isDmg = false;
    private int direction = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ダメージを受けていなかったら動く(無敵時間内じゃない)
        if (isDmg == false)
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

            //ジャンプ
            if (Input.GetKeyDown("space") && this.jumpCount < 3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
                jumpCount++;
                GameObject bulletObject = Instantiate(Bullet, transform.position, Quaternion.identity);
                Bullet bullet = bulletObject.GetComponent<Bullet>();

                Debug.Log("jupCount : " + jumpCount);
            }
            transform.position = position;
        }
        //無敵時間内
        else if (isDmg == true)
        {
            //無敵時間のカウントを始める
            invincibleTime++;

            //向きでノックバック方向を判断
            if (transform.localScale.x >= 0)
            {
                rb.AddForce(transform.right * -5.0f);
            }
            else
            {
                rb.AddForce(transform.right * 5.0f);
            }

            if (invincibleTime >= 200)
            {
                invincibleTime = 0;//無敵時間カウントを元に戻す
                isDmg = false;//ダメージ受けていない状態に戻す
                isInvincible = false;//無敵時間の終了
            }
        }


        //プレイヤーのHPが0になったらゲームオーバー
        if (playerHp <= 0)
        {
            Destroy(this.gameObject);
            //gameoverText.SetActive(true);  //無効になって非表示になったゲームオブジェクトを
            //titleButton.SetActive(true); //このタイミングで有効にする。
            //gameoverAudio.Play();          //Playメソッドを実行することが出来る
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
        }*/
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            //床に降りたらjumpCountを0に
            jumpCount = 0;
        }

        if (other.gameObject.tag == "Enemy")
        {
            //敵にあたったらjumpCountを0に
            jumpCount = 0;

            if (isInvincible == false)
            {
                //プレイヤーのHPを１減らす
                playerHp = playerHp - 1;
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

    //プレイヤーの現在のジャンプ数を教える関数
    public int TeachJumpCount()
    {
        return jumpCount;
    }
}
