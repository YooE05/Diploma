using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace YooE.Diploma
{
    public enum GameState
    {
        None = 0,
        Initialized = 1,
        Playing = 2,
        Paused = 3,
        Finished = 4
    }

    public sealed class LifecycleManager :
        IInitializable,
        ITickable,
        IFixedTickable
    {
        private GameState _currentGameState = GameState.None;
        private GameState _lastGameState = GameState.None;

        private readonly List<Listeners.IGameListener> _listeners = new();
        private readonly List<Listeners.IUpdateListener> _updateListeners = new();
        private readonly List<Listeners.IFixUpdaterListener> _fixUpdaterListeners = new();

        private readonly List<Listeners.IPrestartUpdateListener> _prestartUpdatelisteners = new();

        public LifecycleManager()
        {
            AddListenersFromScene(SceneManager.GetActiveScene().GetRootGameObjects());
        }

        [Inject]
        public void Construct(List<Listeners.IGameListener> listeners)
        {
            foreach (var listener in listeners)
            {
                AddListener(listener);
            }
        }

        #region GameListeners Collecting

        private void AddListenersFromScene(GameObject[] gameObjects)
        {
            foreach (var root in gameObjects)
            {
                AddListeners(root);
            }
        }

        public void AddListeners(GameObject root)
        {
            var listeners = root.GetComponentsInChildren<Listeners.IGameListener>();

            for (int i = 0; i < listeners.Length; i++)
            {
                AddListener(listeners[i]);
            }
        }

        public void AddListeners(Object obj)
        {
            if (obj is GameObject)
            {
                AddListeners((GameObject)obj);
            }
            else if (obj.GetType().GetInterface(nameof(Listeners.IGameListener)) != null)
            {
                AddListener(obj as Listeners.IGameListener);
            }
        }

        public void AddListener(Listeners.IGameListener newListener)
        {
            _listeners.Add(newListener);

            if (newListener is Listeners.IUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (newListener is Listeners.IFixUpdaterListener fixUpdateListener)
            {
                _fixUpdaterListeners.Add(fixUpdateListener);
            }

            if (newListener is Listeners.IPrestartUpdateListener lateUpdatelistener)
            {
                _prestartUpdatelisteners.Add(lateUpdatelistener);
            }
        }

        public void RemoveListener(Listeners.IGameListener removingListener)
        {
            _listeners.Remove(removingListener);

            if (removingListener is Listeners.IUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (removingListener is Listeners.IFixUpdaterListener fixUpdateListener)
            {
                _fixUpdaterListeners.Add(fixUpdateListener);
            }

            if (removingListener is Listeners.IPrestartUpdateListener lateUpdatelistener)
            {
                _prestartUpdatelisteners.Add(lateUpdatelistener);
            }
        }

        #endregion

        #region GameStates Depending Actions

        internal GameState GetCurrentState()
        {
            return _currentGameState;
        }

        public void OnStart()
        {
            if (_currentGameState != GameState.Initialized)
            {
                return;
            }

            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is Listeners.IStartListener startListener)
                {
                    startListener.OnStart();
                }
            }

            _currentGameState = GameState.Playing;
        }

        public void OnFinish()
        {
            Debug.Log("Game Stopped");

            if (_currentGameState != GameState.Playing)
            {
                return;
            }

            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is Listeners.IFinishListener finishListener)
                {
                    finishListener.OnFinish();
                }
            }

            _currentGameState = GameState.Finished;
        }

        public void OnPause()
        {
            if (_currentGameState == GameState.Playing || _currentGameState == GameState.Initialized)
            {
                for (int i = 0; i < _listeners.Count; i++)
                {
                    if (_listeners[i] is Listeners.IPauseListener pausListener)
                    {
                        pausListener.OnPause();
                    }
                }

                _lastGameState = _currentGameState;
                _currentGameState = GameState.Paused;
            }
        }

        public void OnResume()
        {
            if (_currentGameState != GameState.Paused)
            {
                return;
            }

            if (_lastGameState == GameState.Initialized)
            {
                _currentGameState = GameState.Initialized;
            }
            else
            {
                for (int i = 0; i < _listeners.Count; i++)
                {
                    if (_listeners[i] is Listeners.IResumeListener resumeListener)
                    {
                        resumeListener.OnResume();
                    }
                }

                _currentGameState = GameState.Playing;
            }
        }

        #endregion

        #region Zenject Interfaces Implementation

        public void Initialize()
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is Listeners.IInitListener initListener)
                {
                    initListener.OnInit();
                }
            }

            _currentGameState = GameState.Initialized;
        }

        public void Tick()
        {
            if (_currentGameState == GameState.Initialized)
            {
                PrestartUpdate();
            }

            if (_currentGameState != GameState.Playing)
            {
                return;
            }

            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(Time.deltaTime);
            }
        }

        public void FixedTick()
        {
            if (_currentGameState != GameState.Playing)
            {
                return;
            }

            for (int i = 0; i < _fixUpdaterListeners.Count; i++)
            {
                _fixUpdaterListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        #endregion

        private void PrestartUpdate()
        {
            for (int i = 0; i < _prestartUpdatelisteners.Count; i++)
            {
                _prestartUpdatelisteners[i].OnPrestartUpdate(Time.deltaTime);
            }
        }
    }
}