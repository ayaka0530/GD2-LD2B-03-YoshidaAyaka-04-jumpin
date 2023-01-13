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

        //�^���ɔ�ԋ���
        Vector2 pos = transform.position;

        pos.y -= speed;
        distanceCount++;

        transform.position = new Vector2(pos.x, pos.y);

        //������x�i�񂾂疳���Ȃ�
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
