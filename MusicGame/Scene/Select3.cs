using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Scene
{
    class Select3:IScene
    {
        private bool isEndFlag;
        private Map2 map2;

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("3", Vector2.Zero);
            map2.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            map2 = new Map2(GameDevice.Instance());
            map2.Load("StageSelect3.csv", "./csv/");
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            map2.Update(gameTime);
            if (Input.GetKeyTrigger(Keys.D1))
            {
                isEndFlag = true;
            }
        }
    }
}
