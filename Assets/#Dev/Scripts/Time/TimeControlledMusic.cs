using System.Collections;
using UnityEngine;

public class TimeControlledMusic : TimeControlled
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceReversed;
    private float lastGameTime = float.NaN;
    private float sharedAudioTime = 0f; 
    protected override void Start()
    {
        audioSource.Play();
        audioSource.Pause();
        audioSourceReversed.Play();
        audioSourceReversed.Pause();
    }

    public override void OnTimeUpdate()
    {
        float currentGameTime = TimeController.GameTime;

        // 检查游戏时间是否改变来判断是否需要更新播放状态
        if (!float.IsNaN(lastGameTime) && currentGameTime != lastGameTime)
        {
            float deltaTime = currentGameTime - lastGameTime;
            if (deltaTime > 0)
            {
                // 游戏时间前进
                if (!audioSource.isPlaying)
                {
                    sharedAudioTime = audioSource.time;
                    audioSource.UnPause();
                    audioSourceReversed.Pause();
                }
            }
            else
            {
                // 游戏时间后退
                if (!audioSourceReversed.isPlaying)
                {
                    sharedAudioTime = audioSourceReversed.time;
                    audioSourceReversed.UnPause();
                    audioSource.Pause();
                }
            }
        }
        else if (currentGameTime == lastGameTime)
        {
            // 游戏时间没有变化时，暂停音乐
            if (audioSource.isPlaying)
            {
                sharedAudioTime = audioSource.time;
                audioSource.Pause();
            }
            else if (audioSourceReversed.isPlaying)
            {
                sharedAudioTime = audioSourceReversed.time;
                audioSourceReversed.Pause();
            }
        }

        // 初始状态
        if (float.IsNaN(lastGameTime))
        {
            if (currentGameTime >= 0)
            {
                audioSource.time = sharedAudioTime;
                audioSource.Play();
            }
            else
            {
                audioSourceReversed.time = sharedAudioTime;
                audioSourceReversed.Play();
            }
        }

        lastGameTime = currentGameTime;
    }
}
