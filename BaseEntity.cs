using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber
{
    public abstract class BaseEntity
    {
        public EntityType Type { get; set; }

        public int Id { get; set; }

        public Point Point { get; set; }

        public int ExploisedRadius { get; set; }

        public override bool Equals(object obj)
        {
            if (this == null)
                throw new NullReferenceException();
            BaseEntity p = obj as BaseEntity;
            if (p != null)
                return this.Type == p.Type && this.Id == p.Id;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public enum EntityType
        {
            Player = 0,
            Bomb = 1
        }
    }
}
