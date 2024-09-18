using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource[] sound;
    [SerializeField] private AudioSource[] canciones;
    [SerializeField] private AudioSource[] general;

    [SerializeField] private UnityEngine.UI.Image slider;
    [SerializeField] private float maxVolume = 1f;
    private float currentVolume;

    void Start()
    {
        currentVolume = maxVolume;
    }

    void Update()
    {
    }

    void UpdateVolume()
    {
        float healthPercentage = (currentVolume / maxVolume) * 100f;
        slider.fillAmount = healthPercentage / 100f;
    }

    public void Volumen(int _input = 0)
    {
        foreach (AudioSource audio in sound) {
            audio.volume = slider.fillAmount;
        }
    }

    public void MuteCanciones()
    {
        foreach (AudioSource audio in canciones)
        {
            if (audio.mute == false) 
            {
                audio.mute = true;
            } else if (audio.mute == true) 
            {
                audio.mute = false;
            }
        }
    }

    public void MuteGeneral()
    {
        foreach (AudioSource audio in general)
        {
            if (audio.mute == false)
            {
                audio.mute = true;
            }
            else if (audio.mute == true)
            {
                audio.mute = false;
            }
        }
    }
}
