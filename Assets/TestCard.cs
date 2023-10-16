using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestCard : MonoBehaviour
{
    public string TitleCard;
    public string ParagraphText;
    public TextMeshProUGUI titleCard;
    public Image imageCard;
    public TextMeshProUGUI paragraphText;

  

    void Start()
    {
        titleCard.text = TitleCard;
        paragraphText.text = ParagraphText;
    }

    void Update()
    {
        // Check the z-rotation of the object.
        float zRotation = transform.rotation.eulerAngles.z;

        // Determine whether to show info on the right or left based on the rotation.
        if (zRotation >= -6 )
        {
           Debug.LogWarning("right");
        }
        if (zRotation >= 7)
        {
            Debug.LogWarning("left");
        }
    }
}