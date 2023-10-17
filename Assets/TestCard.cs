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

        // If zRotation is negative, add 360 to bring it into the range [0, 360).
        if (zRotation < 0)
        {
            zRotation += 360;
        }

        Debug.LogWarning(zRotation.ToString());

        // Determine whether to show info on the right or left based on the rotation.
        if (zRotation <= 180 - 6)
        {
            Debug.LogWarning("left info");
            if (zRotation <= 180 - 20)
            {
                Debug.LogWarning("left choose");
            }
        }
        else if (zRotation >= 180 + 7)
        {
            Debug.LogWarning("right info");
            if (zRotation <= 180 + 20)
            {
                Debug.LogWarning("right choose");
            }
           
        }
    }


}