using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSourse : MonoBehaviour, IDependency<Pauser>
{
   // [SerializeField] private bool IsPause = true;

    private new AudioSource audio;
    private Pauser pauser;
    public void Construct(Pauser obj) => pauser = obj;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool pause)
    {
        //if (IsPause == false) return;

        if (pause == true)
            audio.Pause();

        if (pause == false)
            audio.Play();
    }
}
