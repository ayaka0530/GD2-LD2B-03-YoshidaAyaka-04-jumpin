using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererOnOffExample : MonoBehaviour
{
    // 点滅させる対象
    [SerializeField] private Renderer target;
    // 点滅周期[s]
    [SerializeField] private float cycle = 1;

    private float time;

    private void Update()
    {
        // 内部時刻を経過させる
        time += Time.deltaTime;

        // 周期cycleで繰り返す値の取得
        // 0〜cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat(time, cycle);

        // 内部時刻timeにおける明滅状態を反映
        target.enabled = repeatValue >= cycle * 0.5f;
    }
    private void OnDisable()
    {
        target.enabled = true;
    }
}
