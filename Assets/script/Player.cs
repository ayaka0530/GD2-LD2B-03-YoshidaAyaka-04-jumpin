using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Renderer target;

    public GameObject Bullet;
    public GameObject gameoverText;    //�Q�[���I�[�o�[�ƕ\������e�L�X�g���i�[
    public AudioSource jumpAudio;   //���y���Đ�����R���|�[�l���g
    public GameObject[] heartArray = new GameObject[3]; //�n�[�g�̕\��
    public GameObject[] bulletArray = new GameObject[3]; //�e�̕\��
    public GameObject bombParticle;
    [SerializeField] GameObject dustCloud;//���n�G�t�F�N�g
    public GameManager gameManager;
    public Rigidbody2D rb;
    public Renderer vertical;

    private float speed = 0.05f;
    private int jumpCount = 0;
    private float jumpForce = 12f;
    private int playerHp = 3;
    private float invincibleTime = 0f;
    private bool isInvincible = false;//true�̎��ɖ��G���ԂɂȂ�
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
            transform.position = position;


        //�_���[�W���󂯂Ă��Ȃ������瓮��(���G���ԓ�����Ȃ�)
        if (isDmg == false && playerHp > 0)
        {
            //�W�����v
            if (Input.GetKeyDown("space") && this.jumpCount < 3)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce, 0);
                jumpCount++;//�W�����v�̃J�E���g�𑝂₷
                GameObject bulletObject = Instantiate(Bullet, transform.position, Quaternion.identity);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                jumpAudio.Play();

                Debug.Log("jupCount : " + jumpCount);

                gameManager.SetDistance(transform.position.y);
            }
            transform.position = position;

            //���E���[�v
            if (transform.position.x > 3.4f)
            {
                transform.position = new Vector3(-3.3f, transform.position.y, 0);
            }
            else if (transform.position.x < -3.4f)
            {
                transform.position = new Vector3(3.3f, transform.position.y, 0);
            }
        }
        //���G���ԓ�
        else if (isDmg == true)
        {
            GetComponent<RendererOnOffExample>().enabled = true;
            //���G���Ԃ̃J�E���g���n�߂�
            invincibleTime += Time.deltaTime;

            //�����Ńm�b�N�o�b�N�����𔻒f
            if (transform.localScale.x >= 0)
            {
                rb.AddForce(transform.right * -5.0f);
            }
            else
            {
                rb.AddForce(transform.right * 5.0f);
            }

            //���G���Ԃ��I�������
            if (invincibleTime >= 1)
            {
                invincibleTime = 0;//���G���ԃJ�E���g�����ɖ߂�
                isDmg = false;//�_���[�W�󂯂Ă��Ȃ���Ԃɖ߂�
                isInvincible = false;//���G���Ԃ̏I��
                GetComponent<RendererOnOffExample>().enabled = false;
            }
            //�v���C���[��HP��0�ɂȂ�����Q�[���I�[�o�[
            if (playerHp <= 0)
            {
                gameoverText.SetActive(true); //�����ɂȂ��Ĕ�\���ɂȂ����Q�[���I�u�W�F�N�g��
                spriteRenderer.enabled = false;//�v���C���[�̃C���X�g���\��
                vertical.enabled = false;//�v���C���[�̃C���X�g���\��
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                //titleButton.SetActive(true); //���̃^�C�~���O�ŗL���ɂ���B
                gameManager.ChangeScene();
            }
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
        }

        if (playerHp == 3)
        {
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }*/

        //�e���̕\��
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
            //���ɍ~�肽��jumpCount��0��
            //jumpCount = 0;
            //���Ƀv���C���[������������G�t�F�N�g���o��
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
            //�G�ɂ���������jumpCount��0��
            jumpCount = 0;

            if (isInvincible == false)
            {
                //�v���C���[��HP���P���炷
                //playerHp = playerHp - 1;
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

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    private void OnDisable()
    {
        target.enabled = true;
    }

}
