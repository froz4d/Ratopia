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
    private GameObject tempDraggedObject;
    private float resetDuration = 1.0f; // The duration for the reset animation
    private float resetTimer = 0.0f; 
    
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
        
        Debug.LogWarning("TP");
        Vector3 position = touchPositionAction.ReadValue<Vector2>();

        // Check if we are clicking on a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.LogWarning("TP2");
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = position;
            Debug.LogWarning(position.ToString());
            Debug.LogWarning(pointerEventData.ToString());

            // Raycast into the UI system to find the clicked UI object
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            
            Debug.LogWarning("TP3");
            if (results.Count > 0)
            {
                GameObject clickedObject = results[0].gameObject;
                Debug.LogWarning("TP4");
                Debug.LogWarning(clickedObject.ToString());
                if (clickedObject.CompareTag("card") && LayerMask.LayerToName(clickedObject.layer) == "card")
                {
                    Debug.LogWarning("TP5");
                    currentDraggedObject = clickedObject;
                    // Calculate the touch offset relative to the UI element's position
                    Vector2 localPoint;
                    Debug.LogWarning("kuy");
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, null, out localPoint);
                    touchOffset = (Vector2)currentDraggedObject.transform.localPosition - localPoint;
                }
                else
                {
                    Debug.LogWarning("TP6");
                }
            }
        }
    }

    private void TouchReleased(InputAction.CallbackContext context)
    {
        if (currentDraggedObject != null)
        {
            currentDraggedObject = null;
        }
    }



    private void Update()
    {
        if (currentDraggedObject != null)
        {
            tempDraggedObject = currentDraggedObject;
            Vector2 position = touchPositionAction.ReadValue<Vector2>();
            Vector2 newPosition = position;
            Vector2 deltaPosition = newPosition - (Vector2)currentDraggedObject.GetComponent<RectTransform>().anchoredPosition;

           
            float pivotWidth = 20f;
            float rotateAmount = deltaPosition.x * touchDragSpeed * Time.deltaTime;
            
            Vector3 pivotPosition = new Vector3(
                currentDraggedObject.transform.localPosition.x - pivotWidth, currentDraggedObject.transform.localPosition.y, currentDraggedObject.transform.localPosition.z
            );
            
            currentDraggedObject.transform.RotateAround(pivotPosition, Vector3.back, rotateAmount);
            resetTimer = 0.0f; 
        }
        else if (tempDraggedObject != null)
        {
            // Calculate the interpolation factor based on time
            float t = resetTimer / resetDuration;
            
            tempDraggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(tempDraggedObject.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, t);
            
            tempDraggedObject.transform.localRotation = Quaternion.Slerp(tempDraggedObject.transform.localRotation, Quaternion.identity, t);
            
            resetTimer += Time.deltaTime;
            
            if (resetTimer >= resetDuration)
            {
                tempDraggedObject = null;
            }
        }
    }


}
