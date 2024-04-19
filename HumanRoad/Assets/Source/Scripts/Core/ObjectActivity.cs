using UnityEngine;

public class ObjectActivity : MonoBehaviour
{
    public void DisableObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void EnableObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
