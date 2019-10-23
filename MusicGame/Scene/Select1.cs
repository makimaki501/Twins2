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
        private Player player;
        private Player2 player2;
        private GameObjectManager gameObjectManager;

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
            gameObjectManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            map2 = new Map2(GameDevice.Instance());
            map2.Load("StageSelect1.csv", "./csv/");
            camera = new Camera(10, 10);
            gameObjectManager = new GameObjectManager();
            gameObjectManager.Initialize();

            player = new Player(new Vector2(96 * 8 + 15, 96 * 4 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player);

            player2 = new Player2(new Vector2(96 * 9 + 18, 96 * 4 + 18), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player2);

            player.SetPos(player2.GetPosition());
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
            gameObjectManager.Update(gameTime);
            map2.Update(gameTime);
            if (Input.GetKeyTrigger(Keys.D1))
            {
                isEndFlag = true;
            }
            if (player.nextscene == 1)
            {
                isEndFlag = true;
            }
            if (player2.nextscene == 1)
            {
                isEndFlag = true;
            }

            if (player.IsHit())
            {
                if (player.IsStop())
                {
                    player.SetPosition2(player2.GetPosition());
                }
                else
                {
                    player.SetPosition2(player2.GetPosition());
                }
            }
            if (player2.IsHit())
            {
                if (player2.IsStop())
                {
                    player2.SetPosition2(player.GetPosition());
                }
                else
                {
                    player2.SetPosition2(player.GetPosition());
                }
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
