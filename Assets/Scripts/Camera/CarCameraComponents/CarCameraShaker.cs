using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class CarCameraShaker : CarCameraComponent
{
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizeSpeedShake;
    [SerializeField] private float shakeAmount;
    [SerializeField] private Vignette vignette;
    [SerializeField] private Kino.Motion motion;
    [SerializeField] private AudioSource windSound;

    private void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizeSpeedShake)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
            vignette.enabled = true;
            motion.enabled = true;
            if (windSound.isPlaying == false)
                windSound.Play();

        }
        else
        {
            vignette.enabled = false;
            motion.enabled = false;
            windSound.Stop();
        }
    }
}
