using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGearboxIndicator : MonoBehaviour, IDependency<Car>
{
    private Car car;
    public void Construct(Car obj) => car = obj;
    [SerializeField] private Text text;

    private void Start()
    {
        car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        text.text = gearName;
    }

}
