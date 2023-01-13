using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float pi = 3.1415f;
    public GameManager gameManager;
    public float angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        float speed = 0.5f;

        float angleRad = angle * (pi / 180);
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        direction *= speed;

        //���ł�������
        Vector2 pos = transform.position;

        pos.x += direction.x;
        pos.y += direction.y;

        transform.position = new Vector2(pos.x, pos.y);

        //��ʊO�o���疳���Ȃ�
        if (pos.x >= 9)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
