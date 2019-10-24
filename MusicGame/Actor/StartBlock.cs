using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor
{
    class StartBlock:GameObject
    {
        private Vector2 position;

        public StartBlock(Vector2 position, GameDevice gameDevice) :
            base("Yoko", position, 96, 96, gameDevice)
        {
            this.position = position;
        }

        public StartBlock(StartBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new StartBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
