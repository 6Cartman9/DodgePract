using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraFovCorrector : CarCameraComponent
{
    [SerializeField] private float minFieldofVeiw;
    [SerializeField] private float maxFieldofVeiw;

    private float defaultFov;

    private void Start()
    {
        camera.fieldOfView = defaultFov;
    }

    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldofVeiw, maxFieldofVeiw, car.NormalizeLinearVelocity);
    }
}
