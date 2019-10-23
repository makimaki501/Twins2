using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor.StageBlock
{
    class Stage4Block : GameObject
    {
        public Stage4Block(Vector2 position, GameDevice gameDevice) :
           base("Idle", position, 96, 96, gameDevice)
        {

        }

        public Stage4Block(Stage4Block other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new Stage4Block(this);
        }

        public override void Hit(GameObject gameObject)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}