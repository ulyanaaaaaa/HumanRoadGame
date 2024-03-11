using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KeyboardInput))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
   private KeyboardInput _keyboardInput;
   private Vector3 _currentPosition;
   private Animator _animator;

   private void Awake()
   {
       _keyboardInput = GetComponent<KeyboardInput>();
       _animator = GetComponent<Animator>();
      _currentPosition = transform.position;
      _keyboardInput.OnLeftCliked += LeftStep;
      _keyboardInput.OnRightCliked += RightStep;
      _keyboardInput.OnRunCliked += Run;
      _keyboardInput.OnBackCliKed += GoBack;
   }

   private void Run()
   {
       _animator.SetTrigger("IsJump");
       _currentPosition.x++;
       transform.position = _currentPosition;
   }

   private void LeftStep()
   {
       _animator.SetTrigger("IsJump");
       _currentPosition.z++;
       transform.position = _currentPosition;
   }

   private void RightStep()
   {
       _animator.SetTrigger("IsJump");
       _currentPosition.z--;
       transform.position = _currentPosition;
   }

   private void GoBack()
   {
       _animator.SetTrigger("IsJump");
       _currentPosition.x--;
       transform.position = _currentPosition;
   }
}
