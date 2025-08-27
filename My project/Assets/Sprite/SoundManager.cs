using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource BGMSource;
    public AudioSource shortSource;

    public AudioClip[] BGMClips;
    public AudioClip[] shortClips;

    private AudioClip currentClips;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShortSound(int index)
    {
        
        BGMSource.PlayOneShot(shortClips[index]);
    }

    public void PlayBGMSound(int index)
    {
        if (currentClips != BGMClips[index])
        {
            BGMSource.clip = BGMClips[index];
            BGMSource.Play();
            currentClips = BGMClips[index];
        }
    }

    public void SetVolice(float value)
    {
        BGMSource.volume = value;
        shortSource.volume = value;
    }
}
