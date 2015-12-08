using Newtonsoft.Json;
using System;

namespace REStructure
{
    public class Player
    {
        [JsonProperty("uniqueID")]
        public int uniqueID { get; protected set; }
        [JsonProperty("account")]
        public string account { get; protected set; }
        [JsonProperty("money")]
        public int money { get; protected set; }
        public event Action OnMoneyChange;

        public Player() { }
        public Player(int uniqueID, string account, int money)
        {
            this.uniqueID = uniqueID;
            this.account = account;
            this.money = money;
        }

        public bool SpendMoney(int money)
        {
            if(this.money >= money)
            {
                this.money -= money;
                if (OnMoneyChange != null)
                    OnMoneyChange();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetMoney(int money)
        {
            this.money += money;
            if (OnMoneyChange != null)
                OnMoneyChange();
            return true;
        }
    }
}
