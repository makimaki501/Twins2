﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Device;
using MusicGame.Actor;

using Microsoft.Xna.Framework.Graphics;
using MusicGame.Util;

namespace MusicGame.Scene
{
    class Title : IScene
    {
        private bool isEndFlag;
        private Map map;
        private GameObjectManager gameObjectManager;
        private Camera camera;
        private Metoronome metoronome;
        private int select;
        private Player3 player3;
        private Player4 player4;
        private bool stop3, stop4;

        public Title()
        {
            isEndFlag = false;
            gameObjectManager = new GameObjectManager();
            camera = new Camera(10, 10);
            metoronome = new Metoronome();
            select = 0;

            
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                null,
                camera.GetMatrix());
            map.Draw(renderer);
            gameObjectManager.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
            gameObjectManager.Initialize();

            map = new Map(GameDevice.Instance());
            map.Load("Title.csv", "./csv/");
            gameObjectManager.Add(map);

            //最初に回っている
            player3 = new Player3(new Vector2(128 * 6 + 15, 128 * 4 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player3);

            //最初に止まっている
            player4 = new Player4(new Vector2(128 * 7 + 18, 128 * 4 + 18), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player4);

            player3.SetPos(player4.GetPosition());

            stop3 = player3.IsStop();
            stop4 = player4.IsStop();

            metoronome.Initialize();
            metoronome.SetBpm(60);
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {

            return Scene.Select;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            metoronome.Update(gameTime);
            map.Update(gameTime);

            gameObjectManager.Update(gameTime);

            if (player3.IsHit())
            {
                if (!player3.IsStop())
                {
                    player3.SetPosition2(player4.GetPosition());
                    //if (player3.IsPush())
                    //{
                    //    stop4 = false;
                    //    player4.stop = stop4;
                    //}
                }
                else
                {
                    player3.SetPosition2(player4.GetPosition());
                }
                
            }
            if (player4.IsHit())
            {

                if (!player4.IsStop())
                {
                    player4.SetPosition2(player3.GetPosition());
                    //if (player4.IsPush())
                    //{
                    //    stop3 = false;
                    //    player3.stop = stop3;
                    //}
                }
                else
                {
                    player4.SetPosition2(player3.GetPosition());
                }
               
            }

            if (!player3.IsStop() && !player4.IsStop())
            {
                isEndFlag = true;
            }

            if (Input.GetKeyState(Keys.D1))
            {
                isEndFlag = true;
            }

            if (Input.GetKeyState(Keys.Right))
            {
                camera.Move(1, 0);
            }
            if (Input.GetKeyState(Keys.Left))
            {
                camera.Move(-1, 0);
            }
            if (Input.GetKeyState(Keys.Up))
            {
                camera.Move(0, -1);
            }
            if (Input.GetKeyState(Keys.Down))
            {
                camera.Move(0, 1);
            }

        }
    }
}
