using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Timer))]
public class TimerViewer : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _bar;
    private Timer _timer;
    private Player _player;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _timer.OnDurationChanged += UpdateView;
    }

    private void UpdateView(float currentDuration, float maxDuration)
    {
        float persent = currentDuration / maxDuration;
        _bar.fillAmount = persent;
        _bar.color = _gradient.Evaluate(persent);
    }
}
