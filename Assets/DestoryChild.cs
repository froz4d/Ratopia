using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryChild : MonoBehaviour
{
    public Transform dadada;

    public GameObject mainmenu;

    public GameObject endingpanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestoryEiEi()
    {
        if (dadada.childCount > 0) // Check if there are any children
        {
            Destroy(dadada.GetChild(dadada.childCount-1).gameObject); // Destroy the front child
        }
        else
        {
            mainmenu.SetActive(true);
            
        }
    }
}
