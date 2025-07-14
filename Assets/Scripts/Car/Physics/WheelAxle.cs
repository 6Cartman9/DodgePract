using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWheelCollider;
    [SerializeField] private WheelCollider rightWheelCollider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    public bool IsMotor => isMotor;
    [SerializeField] private bool isSteer;
    public bool IsSteer => isSteer;

    [SerializeField] private float wheelWidth;

    [SerializeField] private float antiRollForce;
    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStiffness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;

    [SerializeField] private float baseSidewayStiffness = 2.0f;
    [SerializeField] private float stabilitySidewayFactor = 1.0f;

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;

    //Public APi
    public void Update()
    {
        UpdateWheelHit();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffness();

        SynsMeshTranform();
    }

    public void ConfigureVehelicSuvsteps(float speedThreshold, int speedBelowThreshold, int stepsAboveThreshold)
    {
        leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
        rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
    }

    private void UpdateWheelHit()
    {
        leftWheelCollider.GetGroundHit(out leftWheelHit);
        rightWheelCollider.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffness()
    {
        WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

        WheelFrictionCurve leftSideways = leftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

        leftForward.stiffness = baseSidewayStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilitySidewayFactor;
        rightForward.stiffness = baseSidewayStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilitySidewayFactor;

        leftWheelCollider.forwardFriction = leftForward;
        rightWheelCollider.forwardFriction = rightForward;

        leftWheelCollider.sidewaysFriction = leftSideways;
        rightWheelCollider.sidewaysFriction = rightSideways;

    }

    private void ApplyDownForce()
    {
        if (leftWheelCollider.isGrounded == true)
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce *
                leftWheelCollider.attachedRigidbody.velocity.magnitude, leftWheelCollider.transform.position);

        if (rightWheelCollider.isGrounded == true)
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce *
                rightWheelCollider.attachedRigidbody.velocity.magnitude, rightWheelCollider.transform.position);
    }

    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if(leftWheelCollider.isGrounded == true)
        {
            travelL = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;
        }

        if (rightWheelCollider.isGrounded == true)
        {
            travelR = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;
        }

        float forceDir = (travelL - travelR);

        if (leftWheelCollider.isGrounded == true)
        {
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * -forceDir * antiRollForce, leftWheelCollider.transform.position);
        }

        if (rightWheelCollider.isGrounded == true)
        {
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * forceDir * antiRollForce, rightWheelCollider.transform.position);
        }
    }

    public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
    {
        if (isSteer == false) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
        float angleSing = Mathf.Sign(steerAngle);

        if(steerAngle > 0)
        {
            leftWheelCollider.steerAngle = Mathf.Deg2Rad * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSing;
            rightWheelCollider.steerAngle = Mathf.Deg2Rad * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSing;
        }
        else if (steerAngle < 0)
        {
            leftWheelCollider.steerAngle = Mathf.Deg2Rad * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSing;
            rightWheelCollider.steerAngle = Mathf.Deg2Rad * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSing;
        }
        else
        {
            leftWheelCollider.steerAngle = 0;
            rightWheelCollider.steerAngle = 0;
        }

        leftWheelCollider.steerAngle = steerAngle;
        rightWheelCollider.steerAngle = steerAngle;
    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (isMotor == false) return;

        leftWheelCollider.motorTorque = motorTorque;
        rightWheelCollider.motorTorque = motorTorque;
    }

    public void ApplyBrakeTorque(float brakeTorque)
    {
        leftWheelCollider.brakeTorque = brakeTorque;
        rightWheelCollider.brakeTorque = brakeTorque;
    }

    public float GetAverageRpm()
    {
        return (leftWheelCollider.rpm + rightWheelCollider.rpm) * 0.5f;
    }

    public float GetRadius()
    {
        return leftWheelCollider.radius;
    }

    //Private
    private void SynsMeshTranform()
    {
        UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
        UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
