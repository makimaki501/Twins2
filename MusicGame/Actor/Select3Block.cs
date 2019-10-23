using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor
{
    class Select3Block : GameObject
    {
        public Select3Block(Vector2 position, GameDevice gameDevice) :
           base("Idle128", position, 128, 128, gameDevice)
        {

        }

        public Select3Block(Select3Block other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Select3Block(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}