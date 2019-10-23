using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor
{
    class TitleBlock : GameObject
    {
        public TitleBlock(Vector2 position, GameDevice gameDevice) :
           base("Idle128", position, 128, 128, gameDevice)
        {

        }

        public TitleBlock(TitleBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new TitleBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}