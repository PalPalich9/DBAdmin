using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAdmin.Model
{
    public class BacketSet : LegoSet
    {
        public int inBacket;
        public int InBacket
        {
            get { return inBacket; }
            set
            {
                inBacket = value;
                OnPropertyChanged(nameof(InBacket));
            }
        }
        public BacketSet(int article, string name, int price, int pieceCount, int count, string collection, byte[] image, int inBacket)
                        : base(article, name, price, pieceCount, count, collection, image)
        {
            this.inBacket = inBacket;
        }
        public BacketSet(LegoSet legoSet, int inBacket)
                        : base(legoSet.article, legoSet.Name, legoSet.Price, legoSet.PieceCount, legoSet.Count, legoSet.Collection, legoSet.Image)
        {
            this.inBacket = inBacket;
        }
    }
}
