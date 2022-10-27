using UnityEngine;
public class SoundManager : MonoBehaviour
{
    //##### OBJECTS ##################################################################################################################
    private AudioSource _audioSource;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    #endregion
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    #endregion
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    public bool IsPlaying { get => _audioSource.isPlaying; }
    //################################################################################################################################
    public void PlayClip(AudioClip clip)
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
    #endregion
    //################################################################################################################################
}