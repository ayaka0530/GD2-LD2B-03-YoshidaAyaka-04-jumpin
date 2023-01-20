using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererOnOffExample : MonoBehaviour
{
    // �_�ł�����Ώ�
    [SerializeField] private Renderer target;
    // �_�Ŏ���[s]
    [SerializeField] private float cycle = 1;

    private float time;

    private void Update()
    {
        // �����������o�߂�����
        time += Time.deltaTime;

        // ����cycle�ŌJ��Ԃ��l�̎擾
        // 0�`cycle�͈̔͂̒l��������
        var repeatValue = Mathf.Repeat(time, cycle);

        // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
        target.enabled = repeatValue >= cycle * 0.5f;
    }
    private void OnDisable()
    {
        target.enabled = true;
    }
}
