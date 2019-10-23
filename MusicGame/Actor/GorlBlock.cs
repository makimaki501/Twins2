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
    class GorlBlock : GameObject
    {
        private Motion motion;


        public GorlBlock(Vector2 position,GameDevice gameDevice):
            base("GorlMove", position, 96, 96, gameDevice)
        {
            motion = new Motion();
            motion.Add(0, new Rectangle(96 * 0, 96 * 0, 96, 96));
            motion.Add(1, new Rectangle(96 * 1, 96 * 0, 96, 96));
            motion.Add(2, new Rectangle(96 * 0, 96 * 1, 96, 96));
            motion.Add(3, new Rectangle(96 * 1, 96 * 1, 96, 96));
            motion.Initialize(new Range(0, 3), new CountDownTimer(0.1f));
        }

        public GorlBlock(GorlBlock other) : this(other.position, other.gameDevice)
        {

        }

        public override object Clone()
        {
            return new GorlBlock(this);
        }

        public override void Hit(GameObject gameObject)
        {
            
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify(),motion.DrawingRange());
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
        }
    }
}
