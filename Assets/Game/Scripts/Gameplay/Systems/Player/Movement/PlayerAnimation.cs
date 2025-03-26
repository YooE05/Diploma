using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerAnimation
    {
        private readonly Animator _animator;
        public readonly AnimationEvents AnimationEvents;
        private readonly float _locomotionBlendSpeed;

        private static readonly int InputXHash = Animator.StringToHash("InputX");
        private static readonly int InputYHash = Animator.StringToHash("InputY");

        private Vector3 _currentBlendInput = Vector3.zero;

        public PlayerAnimation(Animator animator, AnimationEvents animationEvents, float locomotionBlendSpeed)
        {
            _animator = animator;
            AnimationEvents = animationEvents;
            _locomotionBlendSpeed = locomotionBlendSpeed;
        }

        public void SmoothlyUpdateAnimation(Vector2 inputTarget)
        {
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, _locomotionBlendSpeed * Time.deltaTime);
            SetMoveAnimationValues(_currentBlendInput);
        }

        private void SetMoveAnimationValues(Vector2 inputTarget)
        {
            _animator.SetFloat(InputXHash, inputTarget.x);
            _animator.SetFloat(InputYHash, inputTarget.y);
        }
        
        public void StopMoveAnimation()
        {
            SetMoveAnimationValues(Vector2.zero);
        }

        public void SetAnimatorTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        public void SetAnimatorBool(string valueName, bool value)
        {
            _animator.SetBool(valueName, value);
        }
    }
}