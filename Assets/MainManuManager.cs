using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManuManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void closePanel()
    {
        SettingGamePanel.SetActive(false);
    }

    public void OpenPanel()
    {
        SettingGamePanel.SetActive(true);
    }
}
