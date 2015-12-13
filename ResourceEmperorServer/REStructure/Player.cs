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
        [JsonIgnore]
        private int _money;
        [JsonProperty("money")]
        public int money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                OnMoneyChange?.Invoke();
            }
        }
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
            return true;
        }
    }
}
