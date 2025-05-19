using System.Collections.Generic;
using YooE.SaveLoad;

namespace YooE.Diploma
{
    public sealed class PlayerDataSaveLoader : DataSaveLoader<PlayerData, PlayerDataContainer>
    {
        protected override PlayerData GetData(PlayerDataContainer service)
        {
            return service.GetPlayerData();
        }

        protected override void SetData(PlayerDataContainer service, PlayerData data)
        {
            service.SetData(data);
            //  service.SetupDefaultValues();
        }

        protected override void SetDefaultData(PlayerDataContainer service)
        {
            service.SetupDefaultValues();
        }
    }

    public sealed class PlayerDataContainer
    {
        public bool IsGameCompleted;

        public int CurrentMoney { get; set; }
        public int LastScore { get; private set; }
        public int BestScore { get; private set; }

        public List<string> UnlockedStoreItemsID = new();
        public List<string> EnabledStoreItemsID = new();

        public PlayerData GetPlayerData()
        {
            return new PlayerData(IsGameCompleted, CurrentMoney, LastScore, BestScore, UnlockedStoreItemsID.ToArray(),
                EnabledStoreItemsID.ToArray());
        }

        public void SetLevelResults(int lastScore, int bestScore)
        {
            if (!IsGameCompleted) return;

            LastScore = lastScore;
            CurrentMoney += lastScore;

            BestScore = bestScore;
        }

        public void UnlockStoreItem(string id)
        {
            UnlockedStoreItemsID.Add(id);
            EnabledStoreItemsID.Add(id);
        }

        public void EnableStoreItem(string id)
        {
            if (UnlockedStoreItemsID.Contains(id))
                EnabledStoreItemsID.Add(id);
        }

        public void DisableStoreItem(string id)
        {
            if (EnabledStoreItemsID.Contains(id))
                EnabledStoreItemsID.Remove(id);
        }

        public void SetData(PlayerData newData)
        {
            IsGameCompleted = newData.IsGameCompleted;

            BestScore = newData.BestScore;
            LastScore = newData.LastScore;
            CurrentMoney = newData.Money;

            UnlockedStoreItemsID.Clear();
            UnlockedStoreItemsID.AddRange(newData.UnlockedStoreItems);

            EnabledStoreItemsID.Clear();
            EnabledStoreItemsID.AddRange(newData.EnabledStoreItems);
        }

        public void SetupDefaultValues()
        {
            CurrentMoney = 0;
            LastScore = 0;
            BestScore = 0;
            UnlockedStoreItemsID = new List<string>();
            EnabledStoreItemsID = new List<string>();
        }
    }

    public class PlayerData
    {
        public readonly bool IsGameCompleted;

        public readonly int Money;
        public readonly int LastScore;
        public readonly int BestScore;

        public readonly string[] UnlockedStoreItems;
        public readonly string[] EnabledStoreItems;

        public PlayerData(bool isGameCompleted, int money, int lastScore, int bestScore, string[] unlockedStoreItems,
            string[] enabledStoreItems)
        {
            IsGameCompleted = isGameCompleted;

            Money = money;
            LastScore = lastScore;
            BestScore = bestScore;

            UnlockedStoreItems = unlockedStoreItems;
            EnabledStoreItems = enabledStoreItems;
        }
    }
}