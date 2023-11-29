using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField FeedbackInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        
        
        FindObjectOfType<FirebaseManager>().PushFeedbackData(new FeedbackData(nameInput.text, FeedbackInput.text));
        
        nameInput.text = "";
        FeedbackInput.text = "";
        
    }
}

[System.Serializable]
public class FeedbackData
{
    public string name;
    public string detail;
    
    public FeedbackData(string Inname , string Indetail)
    {
        name = Inname;
        detail = Indetail;
    }
}
