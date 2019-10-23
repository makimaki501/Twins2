using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MusicGame.Device;

namespace MusicGame.Actor
{
    class TitleGorlBlock : GameObject
    {
        public TitleGorlBlock(Vector2 position, GameDevice gameDevice) :
           base("Idle128", position, 128, 128, gameDevice)
        {

        }

        public TitleGorlBlock(TitleGorlBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new TitleGorlBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
