using System;
using UnityEngine;

class PlayerWalkingSoundComponent : MonoBehaviour
{
    AudioSource source;

    public AudioClip[] steps;

    SimpleObserver<Vector2> observer;

    bool shouldPlaySounds;
    void Start()
    {
        source = GetComponent<AudioSource>();
        observer = new SimpleObserver<Vector2>(GetComponent<PlayerMovementController>(), (movement) =>
        {
            if(movement == GetComponent<PlayerMovementController>().MaxPossibleMovement) {
                StartPlaying();
            } else {
                StopPlaying();
            }
        });
    }

    private void StopPlaying()
    {
        shouldPlaySounds = false;
        source.Stop();
    }

    private void StartPlaying()
    {
        if(!shouldPlaySounds) {
            shouldPlaySounds = true;
            PlayNextSound();
        }
    }

    private void PlayNextSound() {
        var clip = steps[UnityEngine.Random.Range(0, steps.Length-1)];
        source.clip = clip;
        source.Play();
    }

    void Update()
    {
        if(shouldPlaySounds && !source.isPlaying) {
            PlayNextSound();
        }
    }
}