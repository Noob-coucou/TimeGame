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

        // �����Ϸʱ���Ƿ�ı����ж��Ƿ���Ҫ���²���״̬
        if (!float.IsNaN(lastGameTime) && currentGameTime != lastGameTime)
        {
            float deltaTime = currentGameTime - lastGameTime;
            if (deltaTime > 0)
            {
                // ��Ϸʱ��ǰ��
                if (!audioSource.isPlaying)
                {
                    sharedAudioTime = audioSource.time;
                    audioSource.UnPause();
                    audioSourceReversed.Pause();
                }
            }
            else
            {
                // ��Ϸʱ�����
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
            // ��Ϸʱ��û�б仯ʱ����ͣ����
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

        // ��ʼ״̬
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
