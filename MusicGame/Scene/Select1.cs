using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor;
using MusicGame.Def;
using MusicGame.Device;
using MusicGame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Scene
{
    class Select1 : IScene
    {
        private bool isEndFlag;
        private Map2 map2;
        private GameObjectManager gameObjectManager;
        private Player player;
        private Player2 player2;
        private Camera camera;
        private Vector2 cameraPos;
        private Motion motion;
        private Motion motion2;
        private int cnt;
        private Sound sound;

        private enum CameraDirection
        {
            UP, DOWN, RIGHT, LEFT, IDLE
        }
        private CameraDirection cameraDirection;

        public Select1()
        {
            isEndFlag = false;
            gameObjectManager = new GameObjectManager();
            camera = new Camera(Screen.Width, Screen.Height);
            motion = new Motion();
            motion2 = new Motion();
            cnt = 0;
            sound = GameDevice.Instance().GetSound();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("world" + StageState.worldsStage, new Vector2(Screen.Width / 2 - 400, 700));
            //map2.Draw(renderer);
            //gameObjectManager.Draw(renderer);

            renderer.End();

            renderer.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                null,
                camera.GetMatrix());
            map2.Draw(renderer);
            gameObjectManager.Draw(renderer);
            renderer.DrawTexture("stagemark1", new Vector2(87, 5), motion.DrawingRange(), Color.LightSalmon);
            renderer.DrawTexture("stagemark2", new Vector2(375, 5), motion.DrawingRange(), Color.Coral);
            renderer.DrawTexture("stagemark3", new Vector2(663, 5), motion.DrawingRange(), Color.Tomato);
            renderer.DrawTexture("stagemark4", new Vector2(951, 5), motion.DrawingRange(), Color.OrangeRed);
            renderer.DrawTexture("stagemark5", new Vector2(1239, 5), motion.DrawingRange(), Color.Red);
            renderer.DrawTexture("selectmark", new Vector2(96 * 3 + 16, 96 * 4 + 16), motion2.DrawingRange(), Color.LightSalmon);
            renderer.DrawTexture("selectmark", new Vector2(96 * 6 + 16, 96 * 4 + 16), motion2.DrawingRange(), Color.Coral);
            renderer.DrawTexture("selectmark", new Vector2(96 * 9 + 16, 96 * 4 + 16), motion2.DrawingRange(), Color.Tomato);
            renderer.DrawTexture("selectmark", new Vector2(96 * 12 + 16, 96 * 4 + 16), motion2.DrawingRange(), Color.OrangeRed);
            renderer.DrawTexture("selectmark", new Vector2(96 * 15 + 16, 96 * 4 + 16), motion2.DrawingRange(), Color.Red);
            renderer.DrawTexture("selectmark", new Vector2(96 * 1 + 16, 96 * 8 + 16), motion2.DrawingRange(), Color.LightGreen);
            renderer.DrawTexture("titleanaunse", new Vector2(-45, 850));
            if (StageState.worldsStage != 3)
            {
                renderer.DrawTexture("selectmark", new Vector2(96 * 17 + 16, 96 * 8 + 16), motion2.DrawingRange(), Color.Blue);
                renderer.DrawTexture("nextanaunse", new Vector2(1480, 850));
            }
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            StageState.stageStage = 1;
            StageState.sceneNumber = 2;
            gameObjectManager.Initialize();
            map2 = new Map2(GameDevice.Instance());
            if (StageState.worldsStage <= 2)
            {
                map2.Load("StageSelect1.csv", "./csv/");
            }
            else
            {
                map2.Load("StageSelect2.csv", "./csv/");
            }
            gameObjectManager.Add(map2);
            sound.PlayBGM("WorldSelect");

            //最初に回っている
            player = new Player(new Vector2(96 * 2 + 15, 96 * 6 + 15), GameDevice.Instance(), gameObjectManager, 0.1f);
            gameObjectManager.Add(player);


            //最初に止まっている
            player2 = new Player2(new Vector2(96 * 1 + 15, 96 * 6 + 15), GameDevice.Instance(), gameObjectManager, player.AddRadian());
            gameObjectManager.Add(player2);
            player.SetPos(player2.GetPosition());
            camera.SetPosition(new Vector2(Screen.Width / 2 - 48, Screen.Height / 2));
            cameraPos = player2.GetPosition();
            cameraDirection = CameraDirection.IDLE;

            motion.Add(0, new Rectangle(500 * 0, 500 * 0, 500, 500));
            motion.Add(1, new Rectangle(500 * 1, 500 * 0, 500, 500));
            motion.Add(2, new Rectangle(500 * 2, 500 * 0, 500, 500));
            motion.Add(3, new Rectangle(500 * 3, 500 * 0, 500, 500));
            motion.Add(4, new Rectangle(500 * 0, 500 * 1, 500, 500));
            motion.Add(5, new Rectangle(500 * 1, 500 * 1, 500, 500));
            motion.Add(6, new Rectangle(500 * 2, 500 * 1, 500, 500));
            motion.Add(7, new Rectangle(500 * 3, 500 * 1, 500, 500));
            motion.Add(8, new Rectangle(500 * 2, 500 * 1, 500, 500));
            motion.Add(9, new Rectangle(500 * 1, 500 * 1, 500, 500));
            motion.Add(10, new Rectangle(500 * 0, 500 * 1, 500, 500));
            motion.Add(11, new Rectangle(500 * 3, 500 * 0, 500, 500));
            motion.Add(12, new Rectangle(500 * 2, 500 * 0, 500, 500));
            motion.Add(13, new Rectangle(500 * 1, 500 * 0, 500, 500));
           
            motion.Initialize(new Range(0, 13), new CountDownTimer(0.1f));

            motion2.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            motion2.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            motion2.Add(2, new Rectangle(64 * 0, 64 * 1, 64, 64));
            motion2.Add(3, new Rectangle(64 * 1, 64 * 1, 64, 64));
            motion2.Initialize(new Range(0, 3), new CountDownTimer(0.1f));
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            Scene nextscene = Scene.GamePlay;
            if (player == null)
            {
                player2.IsDead(true);
            }
            else if (player2 == null)
            {
                player.IsDead(true);
            }
            if (StageState.sceneNumber == 1)
            {
                nextscene = Scene.Title;
            }
            else if (StageState.sceneNumber == 2)
            {
                nextscene = Scene.Select1;
            }
            return nextscene;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            motion2.Update(gameTime);

            map2.Update(gameTime);
            StageState.isMusic = true;

            if (player.IsDead())
            {
                StageState.isMusic = false;
                isEndFlag = true;
            }
            if (player2.IsDead())
            {
                StageState.isMusic = false;
                player._dead = true;
                isEndFlag = true;
            }

            if (!player.IsStop() && !player2.IsStop())
            {
                player.SetPosition(new Vector2(96 * 2 + 15, 96 * 6 + 15));
                player2.SetPosition(new Vector2(96 * 1 + 15, 96 * 6 + 15));
                player2.stop = true;
                player.stop = false;
                player.SetPos(new Vector2(96 * 1 + 15, 96 * 6 + 15));
            }

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

            if (player.IsStop())//もしプレイヤーが止まってたら
            {

                if (cameraPos.X < player.GetPosition().X)
                {
                    cameraDirection = CameraDirection.RIGHT;
                }
                if (cameraPos.X > player.GetPosition().X)
                {
                    cameraDirection = CameraDirection.LEFT;
                }
                if (cameraPos.Y < player.GetPosition().Y)
                {
                    cameraDirection = CameraDirection.UP;
                }
                if (cameraPos.Y > player.GetPosition().Y)
                {
                    cameraDirection = CameraDirection.DOWN;
                }

                cameraPos = player.GetPosition();

            }
            if (player2.IsStop())//もしプレイヤーが止まってたら
            {

                if (cameraPos.X < player2.GetPosition().X)
                {
                    cameraDirection = CameraDirection.RIGHT;
                }
                if (cameraPos.X > player2.GetPosition().X)
                {
                    cameraDirection = CameraDirection.LEFT;
                }
                if (cameraPos.Y < player2.GetPosition().Y)
                {
                    cameraDirection = CameraDirection.UP;
                }
                if (cameraPos.Y > player2.GetPosition().Y)
                {
                    cameraDirection = CameraDirection.DOWN;
                }

                cameraPos = player2.GetPosition();
            }
        }

    }
}



