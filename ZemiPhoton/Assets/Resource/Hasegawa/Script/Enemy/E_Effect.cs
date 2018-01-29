using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class E_Effect : MonoBehaviour
{

    /// オーディオソース
    AudioSource audioSource;
    [SerializeField]AudioClip[] audioClip;
    /// パーティクル
    ParticleSystem[] particles = null;
    /// 再生時間
    float time = 0;

    /// 初期化
    void Start()
    {           
        // 子階層のパーティクルを取得
        particles = GetComponentsInChildren<ParticleSystem>();
        // 再生時間を設定する
        for (int ii = 0; ii < particles.Length; ii++)
        {
            if (time < particles[ii].duration)
                time = particles[ii].duration;
        }

        // オーディオソースを取得
		audioSource = GetComponent<AudioSource> ();
        // 音の再生と再生時間の設定
        if (audioSource != null)
        {
			int ii = Random.Range (0, audioClip.Length);
            audioSource.PlayOneShot(audioClip[ii]);
            if (time < audioClip[ii].length) time = audioClip[ii].length;
        }

        // 再生後の後処理
        StartCoroutine(EndPlayback(time));

    }
     /// 引数分の時間遅延をさせて自身を消す
    IEnumerator EndPlayback(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }
}
