using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _audioSource.pitch = Random.Range(0.1f, 3f);
            _audioSource.Play();
        }
    }
}
