using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
{
    private Car car;
    public void Construct(Car obj) => car = obj;
    [SerializeField] private new Camera camera;
    [SerializeField] private CarCameraFollow follower;
    [SerializeField] private CarCameraShaker shaker;
    [SerializeField] private CarCameraFovCorrector fovCorrector;
    [SerializeField] private CameraPathFollower parhFollower;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Awake()
    {
        follower.SetProperties(car, camera);
        shaker.SetProperties(car, camera);
        fovCorrector.SetProperties(car, camera);
    }

    private void Start()
    {
        raceStateTracker.PreparesionStarted += OnPreparesionStarted;
        raceStateTracker.Completed += OnCompleted;

        follower.enabled = false;
        parhFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparesionStarted -= OnPreparesionStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnPreparesionStarted()
    {
        follower.enabled = true;
        parhFollower.enabled = false;
    }

    private void OnCompleted()
    {
        parhFollower.enabled = true;
        parhFollower.StartMoveToNearestPoint();
        parhFollower.SetLookTarget(car.transform);

        follower.enabled = false;
    }

}
