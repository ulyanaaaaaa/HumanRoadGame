using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
   public Action OnRightSwipe;
   public Action OnLeftSwipe;
   public Action OnBackSwipe;
   public Action OnRunSwipe;

   private Vector2 _fingerDownPosition;
   private Vector2 _fingerUpPosition;
   
   private bool _isTouch;
   private float _swipeThreshold = 20f;

   private void Update()
   {
      DetectTouch();
   }

   private void DetectTouch()
   {
      foreach (Touch touch in Input.touches)
      {
         _isTouch = false;
         
         if (touch.phase == TouchPhase.Began) 
         {
            if (!_isTouch)
            {
               _fingerUpPosition = touch.position;
               _fingerDownPosition = touch.position;
               _isTouch = true;
            }
         }

         if (touch.phase == TouchPhase.Moved)
         {
            if (_isTouch)
            {
               _fingerDownPosition = touch.position;
               DetectSwipe();
            }
         }

         if (touch.phase == TouchPhase.Ended) 
         {
            _fingerDownPosition = touch.position;
               DetectSwipe();
         }
      }
   }

   private void DetectSwipe()
   {
      if (VerticalMoveValue() > _swipeThreshold && 
          VerticalMoveValue() > HorizontalMoveValue()) 
      {
         if (_fingerDownPosition.y - _fingerUpPosition.y > 0) 
            OnRunSwipe?.Invoke();
         
         else if (_fingerDownPosition.y - _fingerUpPosition.y < 0) 
            OnBackSwipe?.Invoke();
         
         _fingerUpPosition = _fingerDownPosition;

      } 
      else if (HorizontalMoveValue() > _swipeThreshold && 
               HorizontalMoveValue() > VerticalMoveValue()) 
      {
         if (_fingerDownPosition.x - _fingerUpPosition.x > 0) 
            OnRightSwipe?.Invoke();
         
         else if (_fingerDownPosition.x - _fingerUpPosition.x < 0) 
            OnLeftSwipe?.Invoke();
         
         _fingerUpPosition = _fingerDownPosition;
      }
   }

   private float VerticalMoveValue()
   {
      return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
   }

   private float HorizontalMoveValue()
   {
      return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
   }
}
