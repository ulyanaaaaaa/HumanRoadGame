using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KeyboardInput))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
   private KeyboardInput _keyboardInput;
   private Animator _animator;

   private void Awake()
   {
       _keyboardInput = GetComponent<KeyboardInput>();
       _animator = GetComponent<Animator>();
       _keyboardInput.OnLeftCliked += LeftStep;
       _keyboardInput.OnRightCliked += RightStep;
       _keyboardInput.OnRunCliked += Run;
       _keyboardInput.OnBackCliKed += GoBack;
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
       if (transform.position.x % 1 == 0)
       {
           _animator.SetTrigger("IsJump");
           transform.DOJump(transform.position + difference, 1f, 1, 0.2f);
       }
   }
}
