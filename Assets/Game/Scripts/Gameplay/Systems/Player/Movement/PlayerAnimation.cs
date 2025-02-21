using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerAnimation
    {
        private readonly Animator _animator;
        public readonly AnimationEvents AnimationEvents;
        private readonly float _locomotionBlendSpeed = 0.2f;

        private static int _inputXHash = Animator.StringToHash("InputX");
        private static int _inputYHash = Animator.StringToHash("InputY");

        private Vector3 _currentBlendInput = Vector3.zero;

        public PlayerAnimation(Animator animator, AnimationEvents animationEvents, float locomotionBlendSpeed)
        {
            _animator = animator;
            AnimationEvents = animationEvents;
            _locomotionBlendSpeed = locomotionBlendSpeed;
        }

        public void UpdateAnimationViaInputValues(Vector2 inputTarget)
        {
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, _locomotionBlendSpeed * Time.deltaTime);

            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
        }

        public void SetAnimatorTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }
    }
}