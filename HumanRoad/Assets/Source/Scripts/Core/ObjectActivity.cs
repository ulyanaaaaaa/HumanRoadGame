using UnityEngine;

public class ObjectActivity : MonoBehaviour
{
    protected void DisableObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    protected void EnableObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
