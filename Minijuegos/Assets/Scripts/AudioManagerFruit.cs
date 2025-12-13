using UnityEngine;

public class AudioManagerFruit : MonoBehaviour
{
    public static AudioManagerFruit Instance;

    public AudioSource audioSource;

    [Header("Sonidos")]
    public AudioClip startSound;
    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayStart()
    {
        audioSource.PlayOneShot(startSound);
    }

    public void PlaySuccess()
    {
        audioSource.PlayOneShot(successSound);
    }

    public void PlayFail()
    {
        audioSource.PlayOneShot(failSound);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
