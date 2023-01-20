using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Player player;

    private int enemyHp = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.tag == "Bullet")
        {
            //�v���C���[�ɓ�����ƃv���C���[�̃X�N���v�g�̃W�����v�J�E���g���O�ɖ߂�
            player.ResetJumpCount();

            enemyHp = enemyHp - 1;
            Debug.Log("playerHp" + enemyHp);
        }
    }
}
