using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraFollow : CarCameraComponent
{
    [Header("Offset")]
    [SerializeField] private float veiwHeigth;
    [SerializeField] private float heigth;
    [SerializeField] private float distance;

    [Header("Daping")]
    [SerializeField] private float rotationDamping;
    [SerializeField] private float heigthDamping;
    [SerializeField] private float speedThreshold;

    private Transform target;
    private new Rigidbody rigidbody;

    private void FixedUpdate()
    {
        Vector3 velocity = rigidbody.velocity;
        Vector3 targetRotation = target.eulerAngles;

        if(velocity.magnitude > speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }

        //lerp
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
        float currentHeith = Mathf.Lerp(transform.position.y, target.position.y + heigth, heigthDamping * Time.fixedDeltaTime);

        //posision
        Vector3 positionOffset = Quaternion.Euler(0, targetRotation.y, 0) * Vector3.forward * distance;
        transform.position = target.position - positionOffset;
        transform.position = new Vector3(transform.position.x, target.position.y + heigth, transform.position.z);

        // rotation
        transform.LookAt(target.position + new Vector3(0, veiwHeigth, 0));
    }

    public override void SetProperties(Car car, Camera camera)
    {
        base.SetProperties(car, camera);

        target = car.transform;
        rigidbody = car.Rigidbody;
    }
}
