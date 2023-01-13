using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.1f;
        float distanceCount = 0f;

        //真下に飛ぶ挙動
        Vector2 pos = transform.position;

        pos.y -= speed;
        distanceCount++;

        transform.position = new Vector2(pos.x, pos.y);

        //ある程度進んだら無くなる
        if (pos.y <= -5)
        {
            Destroy(this.gameObject);
        }
    }
    /*void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }*/
}
