using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public bool IsPlaying { get => _audioSource.isPlaying; }
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip clip)
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}