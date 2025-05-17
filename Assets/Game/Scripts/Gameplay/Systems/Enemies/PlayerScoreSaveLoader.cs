using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class PlayerDataSaveLoader : DataSaveLoader<PlayerData, PlayerScoreAndMoneyContainer>
    {
        protected override PlayerData GetData(PlayerScoreAndMoneyContainer service)
        {
            return service.GetPlayerData();
        }

        protected override void SetData(PlayerScoreAndMoneyContainer service, PlayerData data)
        {
            service.SetData(data);
        }

        protected override void SetDefaultData(PlayerScoreAndMoneyContainer service)
        {
            service.SetupDefaultValues();
        }
    }

    public sealed class PlayerScoreAndMoneyContainer
    {
        public int CurrentMoney { get; private set; }
        public int LastScore { get; private set; }
        public int BestScore { get; private set; }

        public PlayerData GetPlayerData()
        {
            return new PlayerData(CurrentMoney, LastScore, BestScore);
        }

        public void SetLevelResults(int lastScore, int bestScore)
        {
            LastScore = lastScore;
            CurrentMoney += lastScore;

            BestScore = bestScore;
        }

        public void SetData(PlayerData newData)
        {
            BestScore = newData.BestScore;
            LastScore = newData.LastScore;
            CurrentMoney = newData.Money;
        }

        public void SetupDefaultValues()
        {
            CurrentMoney = 0;
            LastScore = 0;
            BestScore = 0;
        }
    }

    public class PlayerData
    {
        public readonly int Money;
        public readonly int LastScore;
        public readonly int BestScore;

        public PlayerData(int money, int lastScore, int bestScore)
        {
            Money = money;
            BestScore = bestScore;
            LastScore = lastScore;
        }
    }
}