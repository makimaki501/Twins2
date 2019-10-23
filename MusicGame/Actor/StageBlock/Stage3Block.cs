using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor.StageBlock
{
    class Stage3Block : GameObject
    {
        public Stage3Block(Vector2 position, GameDevice gameDevice) :
           base("Idle", position, 96, 96, gameDevice)
        {

        }

        public Stage3Block(Stage3Block other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Stage3Block(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}