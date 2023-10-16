using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCard : MonoBehaviour
{
    private NewPlayerController _newPlayerController;

    private GameObject _firstCard;
    // Start is called before the first frame update
    void Start()
    {
        _newPlayerController = FindObjectOfType<NewPlayerController>();
        _firstCard = _newPlayerController.gameObject;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        _newPlayerController.cardMoved += CardMoveFont;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoce = _firstCard.transform.localPosition.x;
        if (Mathf.Abs(distanceMoce) > 0)
        {
            float step = Mathf.SmoothStep(0.8f, 1, Mathf.Abs(distanceMoce) / Screen.width / 2);
            transform.localScale = new Vector3(step, step, step);
        }
    }

    void CardMoveFont()
    {
        gameObject.AddComponent<NewPlayerController>();
        Destroy(this);
    }
}
