using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject goldRecordObj;
    [SerializeField] private GameObject playerRcordObj;
    [SerializeField] private Text goldRecordTime;
    [SerializeField] private Text playerRecordTime;


    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordObj.SetActive(false);
        playerRcordObj.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        if(raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordObj.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if(raceResultTime.RecordWasSet == true)
        {
            playerRcordObj.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        goldRecordObj.SetActive(false); ;
        goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);

        playerRcordObj.SetActive(false);
        playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);

    }

}
