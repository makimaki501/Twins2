using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MusicGame.Device;
using MusicGame.Util;

namespace MusicGame.Actor
{
    class TitleStartBlock : GameObject
    {

        public TitleStartBlock(Vector2 position, GameDevice gameDevice)
            : base("Idle128", position, 128, 128, gameDevice)
        {

        }
        public TitleStartBlock(TitleStartBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new TitleStartBlock(this);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Hit(GameObject gameObject)
        {

        }
    }
}
