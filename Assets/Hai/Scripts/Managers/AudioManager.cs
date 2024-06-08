using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("- Audio Sources -")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public void PlaySFX(AudioClip clip) => sfxSource.PlayOneShot(clip);

    public void PlayBackgroundMusic(AudioClip clip) => musicSource.PlayOneShot(clip);

    public void StopSFX() => sfxSource.Stop();
}
