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
        private string csvname;


        private Scene nextscene;

        private int select;

        private Camera camera;
        int cnt = 0;
        private Vector2 playerpos;
        private int cameracnt;
        private Vector2 cameraPos;
        private enum CameraDirection
        {
            UP, DOWN, RIGHT, LEFT, IDLE
        }
        private CameraDirection cameraDirection;

        private Sound sound;

        private float alpha;

        public GamePlay()
        {
            isEndFlag = false;

            select = 0;

            camera = new Camera(Screen.Width, Screen.Height);
            metoronome = new Metoronome();
            particlemanager = new ParticleManager();
            isstart = false;
            gameObjectManager = new GameObjectManager();
            cameracnt = 0;

            sound = GameDevice.Instance().GetSound();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            particlemanager.Draw(renderer);
            renderer.DrawTexture(StageState.gamePlayState, new Vector2(Screen.Width / 2, 50));
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
            renderer.DrawTexturealpha("gameover", camera.GetPosition(), alpha);
            renderer.End();


        }

        public void Initialize()
        {
            isEndFlag = false;
            gameObjectManager.Initialize();
            alpha = 0;
            map2 = new Map2(GameDevice.Instance());

            map2.Load(StageState.gamePlayState + ".csv", "./csv/");
            //map2.Load("2-2.csv", "./csv/");
            gameObjectManager.Add(map2);

            //最初に回っている
            switch (StageState.gamePlayState)
            {
                case "1-1":
                    playerpos = new Vector2(96 * 5 + 15, 96 * 5 + 15);
                    break;
                case "1-2":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 5 + 15);
                    break;
                case "1-3":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 7 + 15);
                    break;
                case "1-4":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 6 + 15);
                    break;
                case "1-5":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 6 + 15);
                    break;
                case "2-1":
                    playerpos = new Vector2(96 * 5 + 15, 96 * 5 + 15);
                    break;
                case "2-2":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 13 + 15);
                    break;
                case "2-3":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 7 + 15);
                    break;
                case "2-4":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 10 + 15);
                    break;
                case "2-5":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 24 + 15);
                    break;
                case "3-1":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 5 + 15);
                    break;
                case "3-2":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 4 + 15);
                    break;
                case "3-3":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 5 + 15);
                    break;
                case "3-4":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 11 + 15);
                    break;
                case "3-5":
                    playerpos = new Vector2(96 * 6 + 15, 96 * 14 + 15);
                    break;
            }

            player = new Player(new Vector2(playerpos.X + 96, playerpos.Y), GameDevice.Instance(), gameObjectManager, 0.1f);
            player.stop = true;
            player.alpha = 0;
            gameObjectManager.Add(player);

            //最初に止まっている
            player2 = new Player2(playerpos, GameDevice.Instance(), gameObjectManager, 0.11f);
            gameObjectManager.Add(player2);
            camera.SetPosition(player2.GetPosition());
            cameraPos = player2.GetPosition();
            cameraDirection = CameraDirection.IDLE;

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

            if (Input.GetKeyTrigger(Keys.M) || player.nextscene == 10 || player2.nextscene == 10)
            {
                cameraDirection = CameraDirection.IDLE;
                nextscene = Scene.Menu;

                cnt++;
                if (cnt >= 120)
                {
                    sound.StopBGM();
                    isEndFlag = true;
                }
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

            if (!player.IsStop() && !player2.IsStop())
            {
                nextscene = Scene.Menu;
                cameraDirection = CameraDirection.IDLE;
                alpha += 0.05f;
                if (alpha >= 0.5f)
                {
                    alpha = 0.5f;
                }
                sound.StopBGM();
                cnt++;
                StageState.isMusic = false;
                if (cnt >= 120)
                {
                    isEndFlag = true;
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
                    player.stop = false;
                    player.alpha = 1;
                    particlemanager.DirectionTexture(new Vector2(Screen.Width / 2 - 400, Screen.Height / 2 - 200), 1, 0, 10f, 0);
                    sound.PlayBGM(StageState.gamePlayState);
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
                    StageState.isMusic = true;
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

            cameracnt++;
            if (StageState.isMusic)
            {
                //if (cameracnt >= 60)
                //{


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
            switch (cameraDirection)
            {
                case CameraDirection.IDLE:
                    camera.Move(0, 0);
                    break;
                case CameraDirection.RIGHT:
                    camera.Move(3, 0);
                    break;
                case CameraDirection.LEFT:
                    camera.Move(-3, 0);
                    break;
                case CameraDirection.UP:
                    camera.Move(0, 3);
                    break;
                case CameraDirection.DOWN:
                    camera.Move(0, -3);
                    break;

            }
        }

    }
}


