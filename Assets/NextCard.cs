using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCard : MonoBehaviour
{
    private NewCardController _newCardController;

    private GameObject _firstCard;
    // Start is called before the first frame update
    void Start()
    {
        _newCardController = FindObjectOfType<NewCardController>();
        _firstCard = _newCardController.gameObject;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        _newCardController.cardMoved += CardMoveFont;
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
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<NewCardController>();
        Destroy(this);
    }
}
