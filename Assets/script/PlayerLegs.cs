using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLegs : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            //�v���C���[�ɓ�����ƃv���C���[�̃X�N���v�g�̃W�����v�J�E���g���O�ɖ߂�
            player.ResetJumpCount();
            Debug.Log("��������");
        }
    }

}
