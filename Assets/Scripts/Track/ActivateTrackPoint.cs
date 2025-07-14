using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrackPoint : TrackPoint
{
    [SerializeField] private GameObject hint;

    private void Start()
    {
        hint.SetActive(IsTarget);
    }

    protected override void OnPassed()
    {
        hint.SetActive(false);
    }

    protected override void OnAssingnAsTarget()
    {
        hint.SetActive(true);
    }
}
