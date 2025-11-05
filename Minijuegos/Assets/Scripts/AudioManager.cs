using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fuentes de audio")]
    [SerializeField] private AudioSource musicSource; 
    [SerializeField] private AudioSource sfxSource;    

    [Header("Clips")]
    [SerializeField] private AudioClip musicJuego;
    [SerializeField] private AudioClip sfxPickupGasolina;
    [SerializeField] private AudioClip sfxGolpeRoca;
    [SerializeField] private AudioClip sfxGameOver;
    [SerializeField] private AudioClip sfxButtonClick;
    [SerializeField] private AudioClip sfxCoche;

    [Header("Volúmenes (0-1)")]
    [Range(0f, 1f)] public float musicVolume = 0.6f;
    [Range(0f, 1f)] public float sfxVolume = 0.9f;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!musicSource) musicSource = gameObject.AddComponent<AudioSource>();
        if (!sfxSource) sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.spatialBlend = 0f; 
        sfxSource.spatialBlend = 0f; 
        ApplyVolumes();
    }

    private void OnValidate() => ApplyVolumes();

    private void ApplyVolumes()
    {
        if (musicSource) musicSource.volume = musicVolume;
        if (sfxSource) sfxSource.volume = sfxVolume;
    }


    public void PlayMusicJuego()
    {
        if (musicJuego) SwapMusic(musicJuego);
    }

    public void PlayPickup() { PlaySFX(sfxPickupGasolina); }
    public void PlayHit() { PlaySFX(sfxGolpeRoca); }
    public void PlayGameOver() { PlaySFX(sfxGameOver); }
    public void PlayButton() { PlaySFX(sfxButtonClick); }

    public void PlayCoche() { PlaySFX(sfxCoche); }
    public void StopMusic() { musicSource.Stop(); }

    private void SwapMusic(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip) sfxSource.PlayOneShot(clip, sfxVolume);
    }
}
