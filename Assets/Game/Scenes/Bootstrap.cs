using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using YooE.Diploma;

public class Bootstrap : MonoBehaviour
{
    private AsyncOperationHandle<SceneInstance> _sceneHandle;

    [SerializeField] private LoadingScreen _loadingScreen;

    private void Awake()
    {
        _loadingScreen.Show();
    }

    private void Start()
    {
        LoadLabScene();
    }

    private void LoadLabScene()
    {
        if (_sceneHandle.IsValid() && _sceneHandle.IsDone)
        {
            return;
        }

        _sceneHandle = Addressables.LoadSceneAsync("ScienceBaseVisual", LoadSceneMode.Single);
    }
}