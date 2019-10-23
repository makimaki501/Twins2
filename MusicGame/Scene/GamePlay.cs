using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Device;
using MusicGame.Actor;
using MusicGame.Util;
using MusicGame.Def;
using MusicGame.Actor.Effect;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame.Scene
{
    class GamePlay : IScene
    {
        private bool isEndFlag;
        private GameObjectManager gameObjectManager;
        private Player player;
        private Player2 player2;
        private Map2 map2;
        private Metoronome metoronome;
        private bool isstart;
        private ParticleManager particlemanager;

        private Scene nextscene;

        private int select;

        private Camera camera;
        int cnt = 0;

        public GamePlay()
        {
            isEndFlag = false;

            select = 0;

            camera = new Camera(Screen.Width, Screen.Height);
            metoronome = new Metoronome();
            particlemanager = new ParticleManager();
            isstart = false;
            gameObjectManager = new GameObjectManager();
        }
        public void Draw(Renderer renderer)
        {
             renderer.Begin();
            particlemanager.Draw(renderer);
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
            renderer.End();
           

        }

        public void Initialize()
        {
            isEndFlag = false;
            gameObjectManager.Initialize();
            map2 = new Map2(GameDevice.Instance());
            map2.Load("1-1.csv", "./csv/");
            gameObjectManager.Add(map2);

            //最初に回っている
            player = new Player(new Vector2(96 * 4 + 15, 96 * 24 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player);

            //最初に止まっている
            player2 = new Player2(new Vector2(96 * 5 + 18, 96 * 24 + 18), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player2);

            player.SetPos(player2.GetPosition());
            metoronome.Initialize();
            metoronome.SetBpm(150);
            nextscene = Scene.Title;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return nextscene;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            map2.Update(gameTime);
            gameObjectManager.Update(gameTime);

            nextscene = Scene.Title;

            if (Input.GetKeyTrigger(Keys.M))
            {
                nextscene = Scene.Menu;
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

            if (particlemanager.IsCount(240))
            {
                particlemanager.Star("circle", 1, 0.1f, 20, 10, 1);
            }
            

            if (Input.GetKeyTrigger(Keys.D1))
            {
                isEndFlag = true;
            }
      
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            particlemanager.Update(delta);

            if (Input.GetKeyTrigger(Keys.Space))
            {
                isstart = true;
            }

            if (isstart)
            {

                metoronome.CountUpdate();

                if (metoronome.IsCount(0))
                {
                    particlemanager.DirectionTexture(new Vector2(Screen.Width / 2 - 400, Screen.Height / 2 - 200), 1, 0, 10f, 0);
                }
                if (metoronome.IsCount(1))
                {
                    particlemanager.Clear("3");
                    cnt++;
                    if (cnt >= 5)
                    {
                        particlemanager.DirectionTexture(new Vector2(Screen.Width / 2 - 400, Screen.Height / 2 - 200), 1, 0, 10f, 1);
                        cnt = 0;
                    }
                }
                if (metoronome.IsCount(2))
                {
                    particlemanager.Clear("2");
                    cnt++;
                    if (cnt >= 5)
                    {
                        particlemanager.DirectionTexture(new Vector2(Screen.Width / 2 - 400, Screen.Height / 2 - 200), 1, 0, 10f, 2);
                        cnt = 0;
                    }
                }
                if (metoronome.IsCount(3))
                {
                    particlemanager.Clear("1");
                    cnt++;
                    if (cnt >= 5)
                    {
                        particlemanager.DirectionTexture(new Vector2(Screen.Width / 2 - 400, Screen.Height / 2 - 200), 1, 0, 10f, 3);
                        cnt = 0;
                    }
                }
                if (metoronome.IsCount(4))
                {
                    particlemanager.Clear("go");
                    isstart = false;
                }

            }

            if (Input.GetKeyState(Keys.Right))
            {
                camera.Move(5, 0);
            }
            if (Input.GetKeyState(Keys.Left))
            {
                camera.Move(-5, 0);
            }
            if (Input.GetKeyState(Keys.Up))
            {
                camera.Move(0, -5);
            }
            if (Input.GetKeyState(Keys.Down))
            {
                camera.Move(0, 5);
            }
        }

    }
}

