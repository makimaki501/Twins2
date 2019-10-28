﻿using Microsoft.Xna.Framework;
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
            cnt = 0;
            sound = GameDevice.Instance().GetSound();
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("world" + StageState.worldsStage, Vector2.Zero);
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
            renderer.DrawTexture("stagemark1", new Vector2(950, -20), motion.DrawingRange(), Color.LightSalmon);
            renderer.DrawTexture("stagemark2", new Vector2(370, 180), motion.DrawingRange(), Color.Coral);
            renderer.DrawTexture("stagemark3", new Vector2(950, 370), motion.DrawingRange(), Color.Tomato);
            renderer.DrawTexture("stagemark4", new Vector2(370, 570), motion.DrawingRange(), Color.OrangeRed);
            renderer.DrawTexture("stagemark5", new Vector2(950, 750), motion.DrawingRange(), Color.Red);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            map2 = new Map2(GameDevice.Instance());
            map2.Load("StageSelect1.csv", "./csv/");
            gameObjectManager.Add(map2);
            sound.PlayBGM("WorldSelect");

            //最初に回っている
            player = new Player(new Vector2(96 * 9 + 15, 96 * 1 + 15), GameDevice.Instance(), gameObjectManager, 0.1f);
            gameObjectManager.Add(player);


            //最初に止まっている
            player2 = new Player2(new Vector2(96 * 9 + 18, 96 * 1 + 15), GameDevice.Instance(), gameObjectManager, player.AddRadian());
            gameObjectManager.Add(player2);
            player.SetPos(player2.GetPosition());
            camera.SetPosition(new Vector2(Screen.Width / 2 - 48, Screen.Height / 2 + 48));
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
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            if (player == null)
            {
                player2.IsDead(true);
            }
            else if (player2 == null)
            {
                player.IsDead(true);
            }
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
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
                player.SetPosition(new Vector2(96 * 9 + 15, 96 * 1 + 15));
                player2.SetPosition(new Vector2(96 * 9 + 18, 96 * 1 + 15));
                player2.stop = true;
                player.stop = false;
                player.SetPos(new Vector2(96 * 9 + 15, 96 * 1 + 15));
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



