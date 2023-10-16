using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Vector3 _initialPosition;
   private Vector3 _initialRotation ;
    private float _distanceMoved;
    private bool _swipeLeft;

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector2(transform.localPosition.x+eventData.delta.x,transform.localPosition.y);
        if (transform.localPosition.x - _initialPosition.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -10, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, 10, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localEulerAngles;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);
        if(_distanceMoved<0.4*Screen.width)
        {
            StartCoroutine(ReturnToInitialPosition());
        }
        else
        {
            if (transform.localPosition.x > _initialPosition.x)
            {
                _swipeLeft = false;

            }
            else
            {
                _swipeLeft = true;
            }
            StartCoroutine(MovedCard());
        }
    }

    private IEnumerator MovedCard()
    {
        float time = 0;
        while (GetComponent<Image>().color != new Color(1, 1, 1, 0))
        {
            time += Time.deltaTime;
            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x-Screen.width,time),transform.localPosition.y,0);
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x+Screen.width,time),transform.localPosition.y,0);
            }
            GetComponent<Image>().color = new Color(1,1,1,Mathf.SmoothStep(1,0,4*time));
            yield return null;
        }
        Destroy(gameObject);
    }
    private IEnumerator ReturnToInitialPosition()
    {
        float duration = 0.5f; // Adjust the duration as needed for the desired smoothness.
        float elapsedTime = 0f;

        Quaternion initialRotation = Quaternion.Euler(_initialRotation);

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition, elapsedTime / duration);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the card ends up exactly at the initial position and rotation.
        transform.localPosition = _initialPosition;
        transform.localRotation = initialRotation;
    }

}