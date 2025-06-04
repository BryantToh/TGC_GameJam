using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //[Header("Event")]
    //[SerializeField] private EventSO audioEvent;

    [Header("Audio Settings")]
    [SerializeField] private List<AudioClip> listOfBGAudioClips;
    [SerializeField] private List<AudioSFX> listOfAudioClip;
    public static AudioManager instance;
    AudioSource audioSourceBG;
    AudioSource audioSourceSFX;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AudioInit();
        StartCoroutine(PlayAudio());
    }

    void AudioInit()
    {
        GameObject audioSFX = AudioPool.instance.GetPoolObj("SFX", transform, transform.position);
        GameObject audioBGM = AudioPool.instance.GetPoolObj("BGM", transform, transform.position);

        audioSourceBG = audioBGM.GetComponent<AudioSource>();
        audioSourceSFX = audioSFX.GetComponent<AudioSource>();
    }

    IEnumerator PlayAudio()
    {

        foreach (var clip in listOfBGAudioClips)
        {
            audioSourceBG.clip = clip;

            audioSourceBG.Play();

            yield return new WaitForSeconds(clip.length);
        }
    }

    public void PlaySFX(string tag, float volume)
    {
        foreach (AudioSFX s in listOfAudioClip)
        {
            if (tag == s.tag)
            {
                audioSourceSFX.clip = s.audioClip;
                audioSourceSFX.volume = volume;

                audioSourceSFX.Play();
            }
        }
    }


    //IEnumerator WaitForAudioToFinish()
    //{
    //    yield return new WaitForSeconds(audioSourceBG.clip.length);

    //    if (audioEvent != null)
    //    {
    //        audioEvent.InvokeEvent();
    //    }

    //    StartCoroutine(PlayAudio());
    //}
}
[System.Serializable]
public class AudioSFX
{
    public string tag;
    public AudioClip audioClip;
}