using UnityEngine;

public class LogFactory : MonoBehaviour
{
    public Log CreateLog(Vector3 position, float speed)
    {
        Log log = Resources.Load<Log>(AssetsPath.LevelItemsPath.Log);
        return Instantiate(log, position, Quaternion.Euler(90f,-180f,180f)).
            SetSpeed(speed);
    }
}
