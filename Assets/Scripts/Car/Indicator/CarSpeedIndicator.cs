
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedIndicator : MonoBehaviour, IDependency<Car>
{

    private Car car;
    public void Construct(Car obj) => car = obj;
    [SerializeField] private Text text;

    private void Update()
    {
        text.text = car.LinearVelocity.ToString("F0");
    }
}
