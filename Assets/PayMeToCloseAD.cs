using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayMeToCloseAD : MonoBehaviour
{
    public GameObject Admanager;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paid ()
    {
        InterstitialAdExample.isPaid = true;
    }
    
}
