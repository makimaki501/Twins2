using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MusicGame.Device;

namespace MusicGame.Actor
{
    class Block : GameObject
    {
       
        public Block(string name,Vector2 position,GameDevice gameDevice):
            base(name,position,96,96,gameDevice)
        {

        }

        public Block(Block other) : this(other.name,other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Block(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
