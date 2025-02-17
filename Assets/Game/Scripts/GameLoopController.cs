using UnityEngine;
using YooE.Diploma;
using Zenject;

public class GameLoopController : MonoBehaviour
{
    private LifecycleManager _lifecycleManager;

    [Inject]
    public void Construct(LifecycleManager lifecycleManager)
    {
        _lifecycleManager = lifecycleManager;
    }

    private void Start()
    {
        _lifecycleManager.OnStart();
    }
}