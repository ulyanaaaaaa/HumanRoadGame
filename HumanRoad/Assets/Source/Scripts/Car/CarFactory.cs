using UnityEngine;

public class CarFactory : MonoBehaviour
{
    public Car CreateCar(Vector3 position, float speed)
    {
        Car car = Resources.Load<Car>(AssetsPath.LevelItemsPath.Car);
        return Instantiate(car, position, Quaternion.identity).
            SetSpeed(speed);
    }
}
