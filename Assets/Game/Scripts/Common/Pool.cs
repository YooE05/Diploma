using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Pool<T> where T : Object
{
    public event Action<T> OnAddObject;

    protected Transform _initParent;
    protected T _prefab;
    protected int _initCount;

    private readonly Queue<T> _poolQueue = new();
    private readonly List<T> _activeObjects = new();

    public void OnInit()
    {
        InitObjects();
    }

    private void InitObjects()
    {
        for (var i = 0; i < _initCount; i++)
        {
            AddObject();
        }
    }

    private void AddObject()
    {
        T poolObject = Object.Instantiate(_prefab, _initParent);
        OnAddObject?.Invoke(poolObject);
        
        Return(poolObject);
    }

    protected void Return(T objectToReturn)
    {
        _poolQueue.Enqueue(objectToReturn);
        _activeObjects.Remove(objectToReturn);
    }

    protected T Get()
    {
        if (_poolQueue.TryDequeue(out var item))
        {
            _activeObjects.Add(item);
            return item;
        }
        else
        {
            AddObject();
            return Get();
        }
    }

    protected List<T> GetActive()
    {
        return _activeObjects;
    }
}