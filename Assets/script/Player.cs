using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Bullet;
    private float speed = 0.05f;
    private int jumpCount = 0;
    private float jumpForce = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ç∂âEÇÃà⁄ìÆ
        Vector2 position = transform.position;
        if (Input.GetKey("left") || (Input.GetKey(KeyCode.A)))
        {
            position.x -= speed;

        }
        else if (Input.GetKey("right") || (Input.GetKey(KeyCode.D)))
        {
            position.x += speed;
        }

        //ÉWÉÉÉìÉv
        if (Input.GetKeyDown("space") && this.jumpCount < 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
            jumpCount++;
            GameObject bulletObject = Instantiate(Bullet, transform.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.angle = 0;

            Debug.Log("jupCount : " + jumpCount);
        }
        transform.position = position;
    }

    //è∞Ç…ç~ÇËÇΩÇÁjumpCountÇ0Ç…
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            jumpCount = 0;
        }
    }
}
