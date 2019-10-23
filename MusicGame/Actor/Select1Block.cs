using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor
{
    class Select1Block : GameObject
    {
        public Select1Block(Vector2 position, GameDevice gameDevice) :
           base("Idle128", position, 128, 128, gameDevice)
        {

        }

        public Select1Block(Select1Block other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Select1Block(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}