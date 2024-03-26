using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KeyboardInput))]
public class PlayerMovement : MonoBehaviour
{
   private KeyboardInput _keyboardInput;
   private SwipeDetection _swipeDetection;
   private Animator _animator;
   
   private void Awake()
   {
       _keyboardInput = GetComponent<KeyboardInput>();
       _swipeDetection = GetComponent<SwipeDetection>();
       _animator = GetComponentInChildren<Animator>();
   }

   private void OnEnable()
   {
       _keyboardInput.OnLeftCliked += LeftStep;
       _keyboardInput.OnRightCliked += RightStep;
       _keyboardInput.OnRunCliked += Run;
       _keyboardInput.OnBackCliKed += GoBack;

       _swipeDetection.OnBackSwipe += GoBack;
       _swipeDetection.OnRunSwipe += Run;
       _swipeDetection.OnLeftSwipe += LeftStep;
       _swipeDetection.OnRightSwipe += RightStep;
   }
   
   private void Run()
   {
       Move(new Vector3(1,0,0));
   }

   private void LeftStep()
   {
       Move(new Vector3(0,0,1));
   }

   private void RightStep()
   {
       Move(new Vector3(0,0,-1));
   }

   private void GoBack()
   {
       Move(new Vector3(-1,0,0));
   }

   private void Move(Vector3 difference)
   {
       _animator.SetTrigger("IsJump");
       transform.DOJump(transform.position + difference, 1f, 1, 0.2f);
   }
}
