using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip hover;

    private new AudioSource audio;

    private UIButton[] uiButton;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uiButton = GetComponentsInChildren<UIButton>(true);

        for(int i = 0; i < uiButton.Length; i++)
        {
            uiButton[i].PointerEnter += OnPointerEnter;
            uiButton[i].PointerClick += OnPointerClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uiButton.Length; i++)
        {
            uiButton[i].PointerEnter -= OnPointerEnter;
            uiButton[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerEnter(UIButton arg0)
    {
        audio.PlayOneShot(hover);
    }

    private void OnPointerClicked(UIButton arg0)
    {
        audio.PlayOneShot(click);
    }

}
