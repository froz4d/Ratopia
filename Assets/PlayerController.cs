using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float touchDragSpeed = 1.0f;
    

    private InputAction touchPressAction;
    private InputAction touchPositionAction;
    private InputActionMap touchmaMap;

    private Camera mainCamera;
    private RectTransform canvasRectTransform;

    private GameObject currentDraggedObject;
    private Vector2 touchOffset;

    private void Awake()
    {
        touchPressAction = _playerInput.actions["Touchpress"];
        touchPositionAction = _playerInput.actions["TouchPosition"];
        touchmaMap = _playerInput.actions.FindActionMap("TouchSwapLR");

        mainCamera = Camera.main;
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

                // Check if the clicked UI object has the "card" tag
                if (clickedObject.CompareTag("card"))
                {
                    currentDraggedObject = clickedObject;

                    // Calculate the touch offset relative to the UI element's position
                    Vector2 localPoint;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvasRectTransform, position, mainCamera, out localPoint);
                    touchOffset = (Vector2)currentDraggedObject.transform.localPosition - localPoint;
                }
            }
        }
    }

    private void TouchReleased(InputAction.CallbackContext context)
    {
        currentDraggedObject = null;
    }

    private void Update()
    {
        if (currentDraggedObject != null)
        {
            Vector2 position = touchPositionAction.ReadValue<Vector2>();

            // Convert the screen position to canvas space
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform, position, mainCamera, out Vector2 localPoint);

            // Set the anchored position of the UI element with the touch offset
            currentDraggedObject.GetComponent<RectTransform>().anchoredPosition = localPoint + touchOffset;
        }
    }
}
