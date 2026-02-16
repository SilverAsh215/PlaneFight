using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    
    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindAnyObjectByType<AudioManager>();
            }

            return _instance;
        }
    }
    
    private AudioSource _audioSource;
    public AudioClip buttonClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayButtonClip()
    {
        _audioSource.PlayOneShot(buttonClip, 0.4f);
    }

}
