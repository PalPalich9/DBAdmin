using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DBAdmin.Model
{
    public class Order : User
    {
        public int id { get; set; }
        public int sumOrder;
        public string address;
        DateTime createdAt;
        List<int> legoSets;
        public int SumOrder
        {
            get { return sumOrder; }
            set
            {
                sumOrder = value;
                OnPropertyChanged(nameof(SumOrder));
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set
            {
                createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }
        public List<int> LegoSets
        {
            get { return legoSets; }
            set
            {
                legoSets = value;
                OnPropertyChanged(nameof(LegoSets));
            }
        }

        public Order(User user, List<int> legoSets, int sumOrder, string address, DateTime createdAt)
                :base(user.Name, user.Surname, user.Email, user.Phone)
        {
            this.LegoSets = legoSets;
            this.SumOrder = sumOrder;
            this.Address = address;
            this.CreatedAt = createdAt;
        }

    }
}
