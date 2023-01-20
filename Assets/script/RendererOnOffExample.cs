using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererOnOffExample : MonoBehaviour
{
    // “_–Å‚³‚¹‚é‘ÎÛ
    [SerializeField] private Renderer target;
    // “_–ÅŽüŠú[s]
    [SerializeField] private float cycle = 1;

    private float time;

    private void Update()
    {
        // “à•”Žž‚ðŒo‰ß‚³‚¹‚é
        time += Time.deltaTime;

        // ŽüŠúcycle‚ÅŒJ‚è•Ô‚·’l‚ÌŽæ“¾
        // 0`cycle‚Ì”ÍˆÍ‚Ì’l‚ª“¾‚ç‚ê‚é
        var repeatValue = Mathf.Repeat(time, cycle);

        // “à•”Žžtime‚É‚¨‚¯‚é–¾–Åó‘Ô‚ð”½‰f
        target.enabled = repeatValue >= cycle * 0.5f;
    }
    private void OnDisable()
    {
        target.enabled = true;
    }
}
