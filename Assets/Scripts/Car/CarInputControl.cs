using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour,IDependency<Car>
{
    private Car car;
    public void Construct(Car obj) => car = obj;

    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField] [Range(0.0f, 1.0f)] private float autoBrakeStregth = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbrake;

    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateTrottleAndBrake();
      //  UpdateBrake();
        UpdateSteer();

        UpdateAutoBrake();

        //Debug
        if (Input.GetKeyDown(KeyCode.E))
            car.UpGear();
        if (Input.GetKeyDown(KeyCode.Q))
            car.DowmGear();
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateTrottleAndBrake()
    {

        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

        //gears
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToReverseGear();
        }

        if (verticalAxis >  0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToFirstGear();
        }

    }
  
    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbrake = Input.GetAxis("Jump");
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbrake = 0;

        car.ThrottleControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 0;
    }

    public void Stop()
    {
        Reset();

        car.BrakeControl = 1;
    }

    private void UpdateAutoBrake()
    {
        if(Input.GetAxis("Vertical") == 0)
        {
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrakeStregth;
        }
    }
}
