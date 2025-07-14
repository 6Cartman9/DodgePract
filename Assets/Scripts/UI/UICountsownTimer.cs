using UnityEngine;
using UnityEngine.UI;

public class UICountsownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Text text;
    [SerializeField] private Text textToStart;
    private Timer countDownTimer;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    private void Start()
    {
        raceStateTracker.PreparesionStarted += OnPreparesionStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
        textToStart.enabled = true;
        
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparesionStarted -= OnPreparesionStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPreparesionStarted()
    {
        text.enabled = true;
        textToStart.enabled = false;

        enabled = true;
    }


    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }    


    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO!";
    }

  
}
