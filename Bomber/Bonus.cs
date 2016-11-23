using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber
{
    public class Bonus : BaseEntity
    {
        public BonusType BonusType { get; set; }

        public Bonus(string entityData)
        {
            string[] data = entityData.Split(' ');
            if (data.Length != 6 || data[0] != "0")
                throw new FormatException("Data error format!");
            this.Type = EntityType.Bomb;
            this.Id = int.Parse(data[1]);
            this.Point = new Point(int.Parse(data[2]), int.Parse(data[3]));
            this.BonusType = (BonusType)int.Parse(data[4]);
            this.ExploisedRadius = int.Parse(data[5]);
        }

        public override string ToString()
        {
            return $"Bonus {this.Id} on point {this.Point}";
        }
    }

    public enum BonusType
    {
        AddBomb = 1,
        AddRadius = 2
    }
}
