using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.1f;
        float distanceCount = 0f;

        //ê^â∫Ç…îÚÇ‘ãììÆ
        Vector2 pos = transform.position;

        pos.y -= speed;
        distanceCount++;

        transform.position = new Vector2(pos.x, pos.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            gameManager.AddScoreCount();
        }

        if (other.gameObject.tag == "BulletDestroy")
        {
            Destroy(this.gameObject);
        }
    }


}
