using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centreOfMass;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    [Header("Down Force")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    //Debug
    public float MotorTorque;
    public float BrakeTorque;
    public float SteerAngle;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f; 

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (centreOfMass != null)
            rigidbody.centerOfMass = centreOfMass.localPosition;

        for( int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].ConfigureVehelicSuvsteps(50, 50, 50);
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();

        UpdateDownForce();

        UpdateWheelAxles();
    }

    public float GetAverageRmp()
    {
        float sum = 0;

        for(int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAverageRpm();
        }

        return sum / wheelAxles.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRmp() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }

    private void UpdateDownForce()
    {
        float downforce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downforce);
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for(int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }

       for(int i = 0; i < wheelAxles.Length; i++)
       {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);


       }
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
