using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Player player;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] float spawnInterval;
    GameManager gameManager;

    float speed = 1.0f;
    float spawnTimer;
    private Vector3 StartPosition;
    private int direction = 1;


    private int enemyHp = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();
        StartPosition = transform.position;
        spawnTimer = spawnInterval;
        leftEdge = GameObject.Find("LeftEdge").transform;
        rightEdge = GameObject.Find("RightEdge").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //左右移動のあれそれ
        if (transform.position.x >= rightEdge.position.x)
        {
            direction = -1;
        }

        if (transform.position.x <= leftEdge.position.x)
        {
            direction = 1;
        }

        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * direction, StartPosition.y);

        //敵が死んだらいなくなる
        if (enemyHp <= 0)
        {
            gameManager.EnemyDead(this.StartPosition);
            Destroy(this.gameObject);

            //３秒後に復活
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                spawnTimer += spawnInterval;
                enemyHp = 1;
                Debug.Log("spawnTimer : " + spawnTimer);
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0);
                Instantiate(this.gameObject, spawnPos, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //プレイヤーに当たるとプレイヤーのスクリプトのジャンプカウントが０に戻る
            player.ResetJumpCount();

            enemyHp = enemyHp - 1;
            Debug.Log("playerHp" + enemyHp);
        }
    }
}
