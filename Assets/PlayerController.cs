using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float touchDragSpeed = 1.0f;
    [SerializeField] private Transform SpawnCardPosition;
    private InputAction touchPressAction;
    private InputAction touchPositionAction;
    private InputActionMap touchmaMap;

    private RectTransform canvasRectTransform;

    public GameObject currentDraggedObject;
    private Vector2 touchOffset;

    private void Awake()
    {
        touchPressAction = _playerInput.actions["Touchpress"];
        touchPositionAction = _playerInput.actions["TouchPosition"];
        touchmaMap = _playerInput.actions.FindActionMap("TouchSwapLR");
    }

    private void OnEnable()
    {
        touchmaMap.Enable();
        touchPressAction.performed += TouchPressed;
        touchPressAction.canceled += TouchReleased;
    }

    private void OnDisable()
    {
        touchmaMap.Disable();
        touchPressAction.performed -= TouchPressed;
        touchPressAction.canceled -= TouchReleased;
    }

    private void Start()
    {
        // Get the canvas's RectTransform component
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 position = touchPositionAction.ReadValue<Vector2>();

        // Check if we are clicking on a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = position;

            // Raycast into the UI system to find the clicked UI object
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            if (results.Count > 0)
            {
                GameObject clickedObject = results[0].gameObject;
                
                if (clickedObject.CompareTag("card") && LayerMask.LayerToName(clickedObject.layer) == "card")
                {
                    currentDraggedObject = clickedObject;

                    // Calculate the touch offset relative to the UI element's position
                    Vector2 localPoint;
                    Debug.LogWarning("kuy");
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, null, out localPoint);
                    touchOffset = (Vector2)currentDraggedObject.transform.localPosition - localPoint;
                }
            }
        }
    }

    private void TouchReleased(InputAction.CallbackContext context)
    {
        if (currentDraggedObject != null)
        {
            // Set the card's position to the original position
            currentDraggedObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            currentDraggedObject = null;
        }
    }


    private void Update()
    {
        if (currentDraggedObject != null)
        {
            Vector2 position = touchPositionAction.ReadValue<Vector2>();
         
            Debug.LogWarning(position);
            Debug.LogWarning(touchOffset);
            Vector2 newPosition = position;
            Vector2 deltaPosition = newPosition - (Vector2)currentDraggedObject.GetComponent<RectTransform>().anchoredPosition;
            float moveDistance = deltaPosition.x;  // Only consider the X component

            // Apply drag speed here
            float dragFactor = Mathf.Min(1.0f, touchDragSpeed * Time.deltaTime);
            Vector2 finalPosition = (Vector2)currentDraggedObject.GetComponent<RectTransform>().anchoredPosition; 
            finalPosition.x += moveDistance * dragFactor;  // Update only the X position
            currentDraggedObject.GetComponent<RectTransform>().anchoredPosition = finalPosition;
        }
    }

}
