using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;

    public void ShowTilePanel()
    {
        panel1.SetActive(true);
        panel2.SetActive(true);
        panel3.SetActive(true);
        panel4.SetActive(true);
        panel5.SetActive(true);
    }
    
}
