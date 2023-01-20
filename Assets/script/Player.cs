using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject gameoverText;    //���x���N���A�ƕ\������e�L�X�g���i�[
    public GameObject titleButton;   //���̃��x���֑J�ڂ���{�^�����i�[
    public AudioSource gameoverAudio;   //���y���Đ�����R���|�[�l���g
    public GameObject[] heartArray = new GameObject[3]; //�n�[�g�̕\��
    public Rigidbody2D rb;


    private float speed = 0.05f;
    private int jumpCount = 0;
    private float jumpForce = 15f;
    private int playerHp = 3;
    private float invincibleTime = 0f;
    private bool isInvincible = false;//true�̎��ɖ��G���ԂɂȂ�
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
        //�_���[�W���󂯂Ă��Ȃ������瓮��(���G���ԓ�����Ȃ�)
        if (isDmg == false)
        {
            Vector2 position = transform.position;

            //���E�̈ړ�
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
                //���������ɔ��]
                transform.localScale = new Vector3(direction, 1, 1);
            }

            //�W�����v
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
        //���G���ԓ�
        else if (isDmg == true)
        {
            //���G���Ԃ̃J�E���g���n�߂�
            invincibleTime++;

            //�����Ńm�b�N�o�b�N�����𔻒f
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
                invincibleTime = 0;//���G���ԃJ�E���g�����ɖ߂�
                isDmg = false;//�_���[�W�󂯂Ă��Ȃ���Ԃɖ߂�
                isInvincible = false;//���G���Ԃ̏I��
            }
        }


        //�v���C���[��HP��0�ɂȂ�����Q�[���I�[�o�[
        if (playerHp <= 0)
        {
            Destroy(this.gameObject);
            //gameoverText.SetActive(true);  //�����ɂȂ��Ĕ�\���ɂȂ����Q�[���I�u�W�F�N�g��
            //titleButton.SetActive(true); //���̃^�C�~���O�ŗL���ɂ���B
            //gameoverAudio.Play();          //Play���\�b�h�����s���邱�Ƃ��o����
        }

        //�n�[�g�̕\��
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
            //���ɍ~�肽��jumpCount��0��
            jumpCount = 0;
        }

        if (other.gameObject.tag == "Enemy")
        {
            //�G�ɂ���������jumpCount��0��
            jumpCount = 0;

            if (isInvincible == false)
            {
                //�v���C���[��HP���P���炷
                playerHp = playerHp - 1;
                Debug.Log("playerHp" + playerHp);
                isDmg = true;
                isInvincible = true;
            }

            //Player�I�u�W�F�N�g��Layer��"Player"����"Invisible"�֕ύX
            gameObject.layer = LayerMask.NameToLayer("Invisible");

        }
    }
    private IEnumerator dmgFalse()
    {
        //��b�x��������
        yield return new WaitForSeconds(1);
        isDmg = false;

        //Layer��"Player"�ɖ߂�
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //�v���C���[�̌��݂̃W�����v����������֐�
    public int TeachJumpCount()
    {
        return jumpCount;
    }
}
