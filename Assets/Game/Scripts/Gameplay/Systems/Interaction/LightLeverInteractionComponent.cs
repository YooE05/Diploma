using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Tutorial.Gameplay;
using UnityEngine;
using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class LightLeverInteractionComponent : InteractionComponent
    {
        [Inject] private Stage4TaskTracker _stage4TaskTracker;
        [Inject] private GardenViewController _gardenViewController;

        [SerializeField] private GameObject _lightGO;
        [SerializeField] private MeshRenderer _smallLightMeshRenderer;
        [SerializeField] private MeshRenderer _middleLightMeshRenderer;
        [SerializeField] private MeshRenderer _bigLightMeshRenderer;

        [SerializeField] private float _smallLightAlfa;
        [SerializeField] private float _middleLightAlfa;
        [SerializeField] private float _bigLightAlfa;

        [Inject] private NavigationManager _navigationManager;

        private void Awake()
        {
            _lightGO.SetActive(true);
            /*var tween = _lightGO.transform.DOScaleX(0, 0f).SetLink(_lightGO);
            tween.Play();*/
            SetLightAlfaToZero();
        }

        private void SetLightAlfaToZero()
        {
            var currentMaterialColor = _bigLightMeshRenderer.material.color;
            currentMaterialColor.a = 0;
            _bigLightMeshRenderer.material.color = currentMaterialColor;

            currentMaterialColor = _middleLightMeshRenderer.material.color;
            currentMaterialColor.a = 0;
            _middleLightMeshRenderer.material.color = currentMaterialColor;

            currentMaterialColor = _smallLightMeshRenderer.material.color;
            currentMaterialColor.a = 0;
            _smallLightMeshRenderer.material.color = currentMaterialColor;
        }

        public override void Interact()
        {
            _navigationManager.Stop();
            base.Interact();
            //Show Animation
            TurnOnLight().Forget();

            DisableInteractionAbility();
            DisableInteractView();
        }

        /*public void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                TurnOnLight().Forget();
            }
        }*/

        private async UniTaskVoid TurnOnLight()
        {
            await DurationTurnLight(_bigLightMeshRenderer, _bigLightAlfa, 1f);
            await DurationTurnLight(_middleLightMeshRenderer, _middleLightAlfa, 1f);
            await DurationTurnLight(_smallLightMeshRenderer, _smallLightAlfa, 1f);

            _stage4TaskTracker.AdjustGardenLight();
        }

        private async UniTaskVoid ScaleLight(GameObject gO, float startValue, float endValue, float duration)
        {
            var tween = gO.transform.DOScaleX(startValue, 0).SetLink(gO);
            tween.Play();
            tween = gO.transform.DOScaleX(endValue, duration).SetLink(gO);
            tween.Play();
            await UniTask.WaitForSeconds(duration);
        }

        private async UniTask DurationTurnLight(MeshRenderer materialRenderer, float endAlfa, float duration)
        {
            ScaleLight(materialRenderer.gameObject, 0f, 203.2123f, duration).Forget();

            var currentMaterialColor = materialRenderer.material.color;

            var time = 0f;
            while (time < duration)
            {
                currentMaterialColor.a = Mathf.Lerp(0, endAlfa, time / duration);
                materialRenderer.material.color = currentMaterialColor;
                time += Time.deltaTime;
                await UniTask.Yield();
            }

            currentMaterialColor.a = endAlfa;
            materialRenderer.material.color = currentMaterialColor;
        }
    }
}