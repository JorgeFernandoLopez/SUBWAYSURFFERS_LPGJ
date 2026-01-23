using UnityEngine;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{
    [SerializeField]

    private bool isActive = true;
    [SerializeField]

    private float minSwipeDistance = 50f;
    [SerializeField]

    private UnityEvent onSwipeUp;
    [SerializeField]

    private UnityEvent onSwipeDown;
    [SerializeField]

    private UnityEvent onSwipeLeft;
    [SerializeField]

      private UnityEvent onSwipeRight;

      private Vector2 startPosition;

      private void Update()
      
    {
        if (!isActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPosition = Input.mousePosition;
            Vector2 swipeVector = endPosition - startPosition;

            if (swipeVector.magnitude >= minSwipeDistance)
            {
                DetectSwipeDirection(swipeVector);
            }
        }
    }
    private void DetectSwipeDirection(Vector2 swipeVector)
    {
        
    }
}
