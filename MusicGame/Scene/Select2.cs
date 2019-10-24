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
    class Select2:IScene
    {
        private bool isEndFlag;
        private Map2 map2;
        private GameObjectManager gameObjectManager;
        private Player player;
        private Player2 player2;

        public Select2()
        {
            isEndFlag = false;
            gameObjectManager = new GameObjectManager();

        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("2", Vector2.Zero);
            map2.Draw(renderer);
            gameObjectManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            map2 = new Map2(GameDevice.Instance());
            map2.Load("StageSelect2.csv", "./csv/");
            gameObjectManager.Add(map2);

            //最初に回っている
            player = new Player(new Vector2(96 * 8 + 15, 96 * 4 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player);

            //最初に止まっている
            player2 = new Player2(new Vector2(96 * 9 + 18, 96 * 4 + 15), GameDevice.Instance(), gameObjectManager);
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
            map2.Update(gameTime);

            gameObjectManager.Update(gameTime);
            if (player.IsHit())
            {
                if (!player.IsStop())
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
                if (!player2.IsStop())
                {
                    player2.SetPosition2(player.GetPosition());
                }
                else
                {
                    player2.SetPosition2(player.GetPosition());
                }
            }
            if (Input.GetKeyTrigger(Keys.D1))
            {
                isEndFlag = true;
            }
        }
    }
}
