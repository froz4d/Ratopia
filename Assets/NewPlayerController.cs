using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeThreshold = 100f;
    private RectTransform cardRectTransform;
    private InputAction swipeAction;

    private void Start()
    {
        cardRectTransform = GetComponent<RectTransform>();
        
        // Create a new InputAction for swipe
        swipeAction = new InputAction("Swipe", binding: "<Pointer>/position");
        swipeAction.Enable();
        swipeAction.performed += OnSwipePerformed;
    }

    private void OnSwipePerformed(InputAction.CallbackContext context)
    {
        Vector2 swipeDelta = context.ReadValue<Vector2>();
        float swipeDistance = swipeDelta.magnitude;

        if (swipeDistance > swipeThreshold)
        {
            float swipeDirection = Mathf.Sign(swipeDelta.x);
            if (swipeDirection > 0) // Right swipe
            {
                SwipeRight();
            }
            else // Left swipe
            {
                SwipeLeft();
            }
        }
    }

    private void SwipeRight()
    {
        // Handle right swipe action (like)
        // You can add animations and logic for a successful swipe.
        Debug.Log("Right Swipe");
    }

    private void SwipeLeft()
    {
        // Handle left swipe action (dislike)
        // You can add animations and logic for an unsuccessful swipe.
        Debug.Log("Left Swipe");
    }

    private void OnDisable()
    {
        swipeAction.Disable();
    }
}