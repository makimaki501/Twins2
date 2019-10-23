using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor;
using MusicGame.Device;

namespace MusicGame.Scene
{
    class Select1 : IScene
    {
        private bool isEndFlag;
        private Map2 map2;
        private Camera camera;

        public void Draw(Renderer renderer)
        {
            renderer.Begin(SpriteSortMode.Deferred,
                  BlendState.AlphaBlend,
                  SamplerState.LinearClamp,
                  DepthStencilState.None,
                  RasterizerState.CullCounterClockwise,
                   null,
                   camera.GetMatrix());
            renderer.DrawTexture("1", Vector2.Zero);
            map2.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            map2 = new Map2(GameDevice.Instance());
            map2.Load("1-1.csv", "./csv/");
            camera = new Camera(10, 10);
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


            if (Input.GetKeyState(Keys.Right))
            {
                camera.Move(3, 0);
            }
            if (Input.GetKeyState(Keys.Left))
            {
                camera.Move(-3, 0);
            }
            if (Input.GetKeyState(Keys.Up))
            {
                camera.Move(0, -3);
            }
            if (Input.GetKeyState(Keys.Down))
            {
                camera.Move(0, 3);
            }
        }
    }
}
