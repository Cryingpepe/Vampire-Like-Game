using UnityEngine;
using UnityEngine.PlayerLoop;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channelCount;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum SFX { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    void Awake()
    {
        instance = this;

        Init();
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("SFXPlayers");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channelCount];

        for (int index = 0; index < channelCount; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < channelCount; index++)
        {
            int loopIndex = (channelIndex + index) % channelCount;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();

            break;
        }
    }

    public void PlayBGM(bool isPlaying)
    {
        if (isPlaying)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBGM(bool isPlaying)
    {
        bgmEffect.enabled = isPlaying;
    }
}
