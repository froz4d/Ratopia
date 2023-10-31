using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;

public class EventConfig : MonoBehaviour
{

    [SerializeField] private GameObject DefaultCard;
    [SerializeField] private GameObject HalloweenCard;
    public struct userAttributes { }
    public struct appAttributes { }

    public bool IsHalloween;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            RemoteConfigService.Instance.FetchCompleted += ApplyHalloween;
            RemoteConfigService.Instance.FetchConfigs<userAttributes,appAttributes>(new userAttributes(),new appAttributes());
        }
    }

    void ApplyHalloween(ConfigResponse configResponse)
    {
        IsHalloween = RemoteConfigService.Instance.appConfig.GetBool("IsHalloween");
        if (IsHalloween)
        {
            setuphalloween();
        }
        else
        {
            setupDefault();
        }
    }

    void setuphalloween()
    {
        HalloweenCard.SetActive(true);
        DefaultCard.SetActive(false);
    }
    void setupDefault()
    {
        HalloweenCard.SetActive(false);
        DefaultCard.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
