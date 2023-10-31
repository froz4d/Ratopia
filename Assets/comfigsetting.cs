using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
public class comfigsetting : MonoBehaviour
{
    public static comfigsetting instance { get; private set; }
    public string environmentId;
    
    public struct userAttributes { }
    public struct appAttributes { }

    public async Task InitailizeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        RemoteConfigService.Instance.SetEnvironmentID(environmentId);
    }

    async void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (Utilities.CheckForInternetConnection())
        {
            await InitailizeRemoteConfigAsync();
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            RemoteConfigService.Instance.FetchConfigs<userAttributes,appAttributes>(new userAttributes(),new appAttributes());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
