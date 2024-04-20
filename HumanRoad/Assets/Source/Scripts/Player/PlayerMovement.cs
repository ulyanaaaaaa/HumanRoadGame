using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KeyboardInput))]
public class PlayerMovement : MonoBehaviour, IPause
{
   private KeyboardInput _keyboardInput;
   private SwipeDetection _swipeDetection;
   private PauseService _pauseService;
   private bool _isPause;
   
   public Action<float> OnScoreChanged;
   
   [Inject]
   public void Constructor(PauseService pauseService)
   {
       _pauseService = pauseService;
   }

   private void Awake()
   {
       _keyboardInput = GetComponent<KeyboardInput>();
       _swipeDetection = GetComponent<SwipeDetection>();   
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

   private void Start()
   {
       _pauseService.AddPause(this);
   }
   
   public void Pause()
   {
       _isPause = true;
   }

   public void Resume()
   {
       _isPause = false;
   }
   
   private void Run()
   {
       Move(new Vector3(1,0,0));
       OnScoreChanged?.Invoke(transform.position.x);
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
       if (_isPause)
           return;
       
       Animator[] animators = GetComponentsInChildren<Animator>();
       foreach (Animator animator in animators)
       {
           if (!animator.GetComponent<Camera>())
               animator.SetTrigger("IsJump");
       }
       transform.DOJump(transform.position + difference, 1f, 1, 0.2f);
   }
   
   private void OnDisable()
   {
       _keyboardInput.OnLeftCliked -= LeftStep;
       _keyboardInput.OnRightCliked -= RightStep;
       _keyboardInput.OnRunCliked -= Run;
       _keyboardInput.OnBackCliKed -= GoBack;

       _swipeDetection.OnBackSwipe -= GoBack;
       _swipeDetection.OnRunSwipe -= Run;
       _swipeDetection.OnLeftSwipe -= LeftStep;
       _swipeDetection.OnRightSwipe -= RightStep;
   }
}
