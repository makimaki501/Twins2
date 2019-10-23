using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MusicGame.Device;

namespace MusicGame.Actor
{
    class YokoBlock : GameObject
    {
        public YokoBlock(Vector2 position, GameDevice gameDevice):
            base("Yoko", position, 96, 96, gameDevice)
        {

        }

        public YokoBlock(YokoBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new YokoBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
