using System;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparesion,
    Countdown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackpiontCircuit>
{
    public event UnityAction PreparesionStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    private TrackpiontCircuit trackpiontCircuit;
    public void Construct(TrackpiontCircuit trackpiontCircuit) => this.trackpiontCircuit = trackpiontCircuit;


    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplete;

    public Timer CountdownTimer => countdownTimer; 

    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    private void Start()
    {
        StartState(RaceState.Preparesion);

        countdownTimer.enabled = false;

        countdownTimer.Finished += OnCountdownTimerFinished;

        trackpiontCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackpiontCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownTimerFinished;


        trackpiontCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackpiontCircuit.LapCompleted -= OnLapCompleted;

    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnCountdownTimerFinished()
    {
        StartRace();
    }


    private void OnLapCompleted(int lapAmount)
    {
        if(trackpiontCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if(trackpiontCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }

    public void LauchPreparesionStarted()
    {
        if (state != RaceState.Preparesion) return;

        StartState(RaceState.Countdown);

        countdownTimer.enabled = true;

        PreparesionStarted?.Invoke(); 
    }

    private void StartRace()
    {
        if (state != RaceState.Countdown) return;

        StartState(RaceState.Race);

        Started?.Invoke();
    }

    public void CompleteRace()
    {
        if (state != RaceState.Race) return;

        StartState(RaceState.Passed);

        Completed?.Invoke();
    }

    public void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

}
