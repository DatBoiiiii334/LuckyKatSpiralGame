using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("____Audio Sources____")]
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;
    
    [Header("____Audio Clips____")]
    public AudioClip backgroundMusic;
    public AudioClip death;
    public AudioClip win;
    public AudioClip ballBounce;
    public AudioClip powerupTouch;
    public AudioClip whoosh;

    [Header("___Volume Sliders___")]
    public Slider bgMusicSlider;
    public Slider sfxSlider;

    private void Start() {
        PlayBgMusic();
        bgMusicSlider.value = 0.15f;
        sfxSlider.value = 0.3f;
    }

    private void Update() {
        musicSource.volume = bgMusicSlider.value;
        SFXSource.volume = sfxSlider.value;
    }
    
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBgMusic(){
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
