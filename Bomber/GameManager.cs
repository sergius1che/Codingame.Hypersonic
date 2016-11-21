using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomber
{
    public class GameManager
    {
        private List<Player> _players;
        private List<Bomb> _bombs;
        private Player _iam;

        public Map Map { get; set; }
        public int MyId { get; set; }
        public List<Player> Players { get { return _players; } }
        public List<Bomb> Bombs { get { return _bombs; } }
        
        public GameManager(int myId)
        {
            this._players = new List<Player>();
            this._bombs = new List<Bomb>();
            this.MyId = myId;
        }

        public void InitMap(int width, int height)
        {
            this.Map = new Map(width, height);
        }

        public void SetEntity(string entityData)
        {
            this._players = new List<Player>();
            this._bombs = new List<Bomb>();

            if (entityData.StartsWith("0"))
                SetPlayer(entityData);
            else if (entityData.StartsWith("1"))
                SetBomb(entityData);
        }

        public void SetPlayer(string entityData)
        {
            Player player = new Player(entityData);
            if (player.Id == this.MyId)
                this._iam = player;
            this._players.Add(player);
        }

        public void SetBomb(string entityData)
        {
            this._bombs.Add(new Bomb(entityData));
        }

        public void Turn()
        {

        }
    }
}
