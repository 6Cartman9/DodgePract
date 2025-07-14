using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private float curretTime;

    public float CurretTime => curretTime;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        enabled = true;
        curretTime = 0;
    }

    private void OnRaceCompleted()
    {
        enabled = false;
    }

    private void Update()
    {
        curretTime += Time.deltaTime;
    }
}
