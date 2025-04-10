using YooE.Diploma.Interaction;
using Zenject;

namespace YooE.Diploma
{
    public sealed class Stage6StartInitializer : StageInitializer
    {
        [Inject] private FightDoorInteractionComponent _fightZoneInteraction;

        public override void InitGameView()
        {
            _fightZoneInteraction.DisableInteractionAbility();
        }
    }
}