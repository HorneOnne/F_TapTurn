﻿using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace TapTurn
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager Instance { get; private set; }

        private static Dictionary<SoundType, float> soundTimerDictionary;
        public SoundAudioClip[] soundAudioClips;
        [SerializeField] private GameObject oneShotGameObject;
        private AudioSource oneShotAudioSource;
        [Range(0, 1)]
        public float sfxVolume = 1.0f;

        [Header("Background")]
        public AudioClip backgroundSoundClip;
        private AudioSource backgroundAudioSource;
        [Range(0, 1)]
        public float backgroundVolume = 1.0f;

        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;

            }
            Instance = this;
            Initialized();
            PlayBackgroundSound();
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);          
        }


       
        public void Initialized()
        {
            soundTimerDictionary = new Dictionary<SoundType, float>();
            soundTimerDictionary[SoundType.Tap] = 0.0f;
            soundTimerDictionary[SoundType.Destroyed] = 0.0f;
            soundTimerDictionary[SoundType.Win] = 0.0f;
            soundTimerDictionary[SoundType.GameOver] = 0.0f;
            soundTimerDictionary[SoundType.Button] = 0.0f;
        }

        public void PlaySound(SoundType soundType, bool playRandom, float pitch = 1.0f)
        {
            if (CanPlaySound(soundType) == false) return;
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.volume = sfxVolume;
                oneShotAudioSource.pitch = pitch;
                DontDestroyOnLoad(oneShotAudioSource.gameObject);
            }
            else
            {
                oneShotAudioSource = oneShotGameObject.GetComponent<AudioSource>();
                oneShotAudioSource.volume = sfxVolume;
                oneShotAudioSource.pitch = pitch;
            }

            if (playRandom)
            {
                oneShotAudioSource.PlayOneShot(GetRandomAudioClip(soundType));
            }
            else
            {
                oneShotAudioSource.PlayOneShot(GetFirstAudioClip(soundType));
            }

        }

        public void PlaySound(SoundType soundType, bool playRandom, Vector2 position)
        {
            if (CanPlaySound(soundType) == false) return;
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }

            if (playRandom)
            {
                oneShotAudioSource.clip = GetRandomAudioClip(soundType);
            }
            else
            {
                oneShotAudioSource.clip = GetFirstAudioClip(soundType);
            }

            oneShotAudioSource.Play();
        }

        public void PlaySound(SoundType soundType, AudioClip audioClip)
        {
            if (CanPlaySound(soundType) == false) return;
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(audioClip);
        }


        private AudioClip GetFirstAudioClip(SoundType soundType)
        {
            foreach (var soundAudioClip in soundAudioClips)
            {
                if (soundAudioClip.soundType.Equals(soundType))
                {
                    return soundAudioClip.audioClips[0];
                }
            }

            Debug.LogError($"Sound {soundType} not found!");
            return null;
        }

        private AudioClip GetRandomAudioClip(SoundType soundType)
        {
            foreach (var soundAudioClip in soundAudioClips)
            {
                if (soundAudioClip.soundType.Equals(soundType))
                {
                    return soundAudioClip.audioClips[Random.Range(0, soundAudioClip.audioClips.Count)];
                }
            }

            Debug.LogError($"Sound {soundType} not found!");
            return null;
        }


        private bool CanPlaySound(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Tap:
                    return CanSoundTypePlay(soundType, 0.05f);
                case SoundType.Destroyed:
                    return CanSoundTypePlay(soundType, 0.05f);
                case SoundType.Win:
                    return CanSoundTypePlay(soundType, 0.1f);
                case SoundType.Button:
                    return CanSoundTypePlay(soundType, 0.05f);
                default:
                    return true;
            }
        }

        private bool CanSoundTypePlay(SoundType soundType, float maxTimePlay)
        {
            if (soundTimerDictionary.ContainsKey(soundType))
            {
                float lastTimePlayed = soundTimerDictionary[soundType];
                if (lastTimePlayed + maxTimePlay < Time.time)
                {
                    soundTimerDictionary[soundType] = Time.time;
                    return true;
                }
                return false;
            }
            else
                return false;
        }



        #region Background
        private void PlayBackgroundSound()
        {
            backgroundAudioSource = this.gameObject.AddComponent<AudioSource>();
            backgroundAudioSource.clip = backgroundSoundClip;
            backgroundAudioSource.volume = backgroundVolume;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
        public void UpdateBackgroundVolume()
        {
            backgroundAudioSource.volume = backgroundVolume;
        }
        #endregion
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundType soundType;
        public List<AudioClip> audioClips;
    }
    public enum SoundType
    {
        Wait,
        Tap,
        Destroyed,
        Win,
        GameOver,
        Button,
    }
}
