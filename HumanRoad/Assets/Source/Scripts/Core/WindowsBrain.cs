using UnityEngine;

public class WindowsBrain : MonoBehaviour
{
    public void CloseWindow(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void OpenWindow(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
