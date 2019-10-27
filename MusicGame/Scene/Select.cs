//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using MusicGame.Actor;
//using MusicGame.Device;

//namespace MusicGame.Scene
//{
//    class Select : IScene
//    {
//        private bool isEndFlag;
//        private Map2 map2;
//        private int select;
//        private GameObjectManager gameObjectManager;
//        private Player player;
//        private Player2 player2;

//        public Select()
//        {
//            isEndFlag = false;
//            gameObjectManager = new GameObjectManager();
//        }

//        public void Draw(Renderer renderer)
//        {
//            renderer.Begin();

//            map2.Draw(renderer);
//            gameObjectManager.Draw(renderer);

//            renderer.End();
//        }

//        public void Initialize()
//        {
//            isEndFlag = false;
//            gameObjectManager.Initialize();

//            map2 = new Map2(GameDevice.Instance());
//            map2.Load("worldSelect.csv", "./csv/");
//            gameObjectManager.Add(map2);

//            //最初に回っている
//            player = new Player(new Vector2(96 * 9 + 15, 96 * 3 + 15), GameDevice.Instance(), gameObjectManager);
//            gameObjectManager.Add(player);

//            //最初に止まっている
//            player2 = new Player2(new Vector2(96 * 10 + 18, 96 * 3 + 18), GameDevice.Instance(), gameObjectManager);
//            gameObjectManager.Add(player2);

//            player.SetPos(player2.GetPosition());
//        }

//        public bool IsEnd()
//        {
//            return isEndFlag;
//        }

//        public Scene Next()
//        {
//            Scene nextscene = Scene.Select1;
//            switch (select)
//            {
//                case 1:
//                    nextscene = Scene.Select1;
//                    break;
//                case 2:
//                    nextscene = Scene.Select2;
//                    break;
//                case 3:
//                    nextscene = Scene.Select3;
//                    break;
//            }
//            return nextscene;
//        }

//        public void Shutdown()
//        {

//        }

//        public void Update(GameTime gameTime)
//        {
//            gameObjectManager.Update(gameTime);
//            map2.Update(gameTime);

//            if (player.IsHit())
//            {
//                if (player.IsStop())
//                {
//                    player.SetPosition2(player2.GetPosition());
//                }
//                else
//                {
//                    player.SetPosition2(player2.GetPosition());
//                }
//            }
//            if (player2.IsHit())
//            {
//                if (player2.IsStop())
//                {
//                    player2.SetPosition2(player.GetPosition());
//                }
//                else
//                {
//                    player2.SetPosition2(player.GetPosition());
//                }
//            }

//            if (Input.GetKeyTrigger(Keys.D1))
//            {
//                select = 1;
//                isEndFlag = true;
//            }

//            if (Input.GetKeyTrigger(Keys.D2))
//            {
//                select = 2;
//                isEndFlag = true;
//            }
//            if (Input.GetKeyTrigger(Keys.D3))
//            {
//                select = 3;
//                isEndFlag = true;
//            }
//        }
//    }
//}
