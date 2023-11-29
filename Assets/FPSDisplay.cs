using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    void Start()
    {
        Application.targetFrameRate = 120;
    }
    void Update()
    {
       // deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

  
}