using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;
    public AudioClip backgroundMusic, upgradeSound, buySound;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource && backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayBuySound() => audioSource?.PlayOneShot(buySound);
    public void PlayUpgradeSound() => audioSource?.PlayOneShot(upgradeSound);
}
