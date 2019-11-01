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
        private bool playNow;
        private ParticleManager particlemanager;
        private Motion motion;
        private enum StartMotion { NULL, START }
        private StartMotion startmotion;
        private Dictionary<StartMotion, Range> startmotions;

        private Camera camera;
        int cnt = 0;
        private Vector2 cameraPos;
        private int bpm;
        private float motionbpm;
        private int a;

        private enum CameraDirection
        {
            UP, DOWN, RIGHT, LEFT, IDLE
        }
        private CameraDirection cameraDirection;

        private Sound sound;

        private float alpha;
        private List<int> firstpositions;
        private int[,] positions;
        private Vector2 playerposition;
        private float addradian;
        private bool isp;
        private int startcnt;
        private int cameracnt;

        private int end;
        private bool _end;
        private bool safe;

        public GamePlay()
        {
            isEndFlag = false;

            camera = new Camera(Screen.Width, Screen.Height);
            metoronome = new Metoronome();
            particlemanager = new ParticleManager();

            gameObjectManager = new GameObjectManager();


            sound = GameDevice.Instance().GetSound();

        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            if (StageState.stageStage == 5)
            {
                renderer.DrawTexture("backGround" + StageState.worldsStage, Vector2.Zero);
            }
            particlemanager.Draw(renderer);
            renderer.DrawTexture(StageState.worldsStage + "-" + StageState.stageStage, Vector2.Zero);

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
            renderer.DrawTexture("start", new Vector2(camera.Position.X - 50, player2.GetPosition().Y - 250), motion.DrawingRange(), Color.White);
            renderer.DrawTexturealpha("gameover", camera.GetPosition(), alpha);
            renderer.End();


        }

        public void Initialize()
        {
            StageState.isClear = false;
            StageState.isMusic = false;
            StageState.sceneNumber = 3;
            isEndFlag = false;
            playNow = false;
            isstart = false;
            _end = false;
            safe = false;
            cnt = 0;
            alpha = 0;
            end = 0;
            gameObjectManager.Initialize();
            firstpositions = new List<int>() { 5, 5, 7, 6, 6, 5, 13, 7, 10, 24, 5, 14, 5, 11, 14 };
            map2 = new Map2(GameDevice.Instance());

            map2.Load(StageState.worldsStage + "-" + StageState.stageStage + ".csv", "./csv/");
            gameObjectManager.Add(map2);

            if (StageState.worldsStage == 2 || (StageState.worldsStage == 1 && StageState.stageStage == 5))
            {
                bpm = 150;
                addradian = 0.125f;
                motionbpm = 0.4f;
            }
            else
            {
                bpm = 120;
                addradian = 0.1f;
                motionbpm = 0.5f;
            }

            positions = new int[,]
            {
                {5,5,7,6,6 },
                {5,13,7,10,24},
                {5,4,5,11,14},
            };
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    if (StageState.worldsStage == i)
                    {
                        if (StageState.stageStage == j)
                        {
                            playerposition = new Vector2(96 * 5 + 16, 96 * positions[i - 1, j - 1] + 16);
                        }
                    }
                }
            }

            player = new Player(new Vector2(playerposition.X + 96, playerposition.Y), GameDevice.Instance(), gameObjectManager, addradian);
            player.stop = true;
            player.alpha = 1;
            gameObjectManager.Add(player);

            //最初に止まっている
            player2 = new Player2(playerposition, GameDevice.Instance(), gameObjectManager, player.AddRadian());
            gameObjectManager.Add(player2);
            camera.SetPosition(player2.GetPosition());
            cameraPos = player2.GetPosition();
            cameraDirection = CameraDirection.IDLE;



            player.SetPos(player2.GetPosition());
            metoronome.Initialize();
            metoronome.SetBpm(bpm);
            motion = new Motion();
            motion.Add(0, new Rectangle(200 * 0, 200 * 0, 200, 200));
            motion.Add(1, new Rectangle(200 * 1, 200 * 0, 200, 200));
            motion.Add(2, new Rectangle(200 * 0, 200 * 1, 200, 200));
            motion.Add(3, new Rectangle(200 * 1, 200 * 1, 200, 200));
            motion.Add(4, new Rectangle(1, 1, 1, 1));
            motion.Add(5, new Rectangle(1, 1, 1, 1));
            motion.Initialize(new Range(4, 5), new CountDownTimer(motionbpm));
            startmotion = StartMotion.NULL;
            startmotions = new Dictionary<StartMotion, Range>()
            {
                {StartMotion.START,new Range(0,3) },
                {StartMotion.NULL,new Range(4,5) },
            };
            isp = false;

            startcnt = 0;
            cameracnt = 0;
            a = 60;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Menu;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            map2.Update(gameTime);
            gameObjectManager.Update(gameTime);
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            particlemanager.Update(delta);

            if (Input.GetKeyTrigger(Keys.M) || StageState.isClear)
            {
                cameraDirection = CameraDirection.IDLE;
                if (!isp)
                {
                    particlemanager.Texture("clear", new Vector2(Screen.Width / 2, 300), 1, 0, 2f, 3);
                    particlemanager.RightCraccar("star", new Vector2(Screen.Width / 2 - 900, 1000), 0.1f, 1, 500, 10000);
                    particlemanager.LeftCraccar("star", new Vector2(Screen.Width / 2 + 900, 1000), 0.1f, 1, 500, 10000);
                    isp = true;
                }

                cnt++;
                if (cnt > 120)
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

            if ((!player.IsStop() && !player2.IsStop() && !StageState.isClear) || _end)
            {
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

            if (!playNow && Input.GetKeyTrigger(Keys.Space))
            {
                isstart = true;
                ChangeMotion(StartMotion.START);
            }

            if (isstart)
            {

                metoronome.CountUpdate();
                player.stop = false;
                playNow = true;
                player.alpha = 1;
                sound.PlayBGM(StageState.worldsStage + "-" + StageState.stageStage);


                if (metoronome.IsCount(4))
                {


                    ChangeMotion(StartMotion.NULL);
                    StageState.isMusic = true;
                    isstart = false;
                    startcnt = 0;
                }
            }
            motion.Update(gameTime);
            //UpdateMotion();

            if (StageState.isMusic)
            {
                if (!_end && !safe && !StageState.isClear)
                {
                    end++;
                    if (Input.GetKeyTrigger(Keys.Space))
                    {
                        //safe = true;
                        end = 0;
                    }
                    if (bpm == 120)
                    {
                        if (end == 60)
                        {
                            _end = true;
                        }
                    }
                    else if (bpm == 150)
                    {
                        if (end == 48)
                        {
                            _end = true;
                        }
                    }
                }

                CameraMove(3);
            }

            if (StageState.stageStage == 5)
            {
                if (particlemanager.IsCount(60))
                {
                    var rnd = GameDevice.Instance().GetRandom();
                    switch (StageState.worldsStage)
                    {
                        case 1:
                            //particlemanager.Backparticle("onpu2", 0.1f, 0.01f, 3, 3);
                            break;
                        case 2:
                            particlemanager.Backparticle("star", new Vector2(rnd.Next(1920), rnd.Next(1080)), 0, 0, 2, 0, 0, 0.5f, 30, 5);
                            particlemanager.Backparticle("star", new Vector2(rnd.Next(3960), rnd.Next(600)), 200, 150, 1, 0, 0, 0.5f, 10, 2);
                            break;
                        case 3:
                            int namenumber = rnd.Next(50);
                            if (namenumber % 2 == 0)
                            {
                                namenumber = 1;
                            }
                            else
                            {
                                namenumber = 2;
                            }
                            if (particlemanager.IsCount(60))
                            {
                                particlemanager.Backparticle("snow" + namenumber, new Vector2(rnd.Next(1920), -100), 50, 180, 1, 0f, 1, 0.1f, 1, 15);
                            }

                            particlemanager.Backparticle("star", new Vector2(rnd.Next(1920), rnd.Next(1080)), 100, 180, 2, 0, 0, 0.5f, 30, 5);
                            break;
                    }
                }
            }

        }

        public void CameraMove(float speed)
        {

            cameracnt++;
            if (cameracnt >= a)
            {
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
                else if (player.IsStop())//もしプレイヤーが止まってたら
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
                cameracnt = 0;
            }

            if (bpm == 150)
            {
                if (StageState.worldsStage == 1)
                {
                    switch (cameraDirection)
                    {
                        case CameraDirection.IDLE:
                            camera.Move(0, 0);
                            a = 20;
                            break;
                        case CameraDirection.RIGHT:
                            camera.Move(3.8f, 0);
                            a = 60;
                            break;
                        case CameraDirection.LEFT:
                            camera.Move(-3, 0);
                            a = 60;
                            break;
                        case CameraDirection.UP:
                            camera.Move(0, 2);
                            a = 3;
                            break;
                        case CameraDirection.DOWN:
                            camera.Move(0, -2);
                            a = 3;
                            break;
                    }
                }
                else
                {
                    switch (cameraDirection)
                    {
                        case CameraDirection.IDLE:
                            camera.Move(0, 0);
                            a = 60;
                            break;
                        case CameraDirection.RIGHT:
                            camera.Move(4, 0);
                            a = 60;
                            break;
                        case CameraDirection.LEFT:
                            camera.Move(-3, 0);
                            break;
                        case CameraDirection.UP:
                            camera.Move(0, 3f);
                            a = 30;
                            break;
                        case CameraDirection.DOWN:
                            camera.Move(0, -3f);
                            a = 40;
                            break;
                    }
                }


            }
            if (bpm == 120)
            {
                if (StageState.worldsStage == 3 && StageState.stageStage == 5)
                {
                    switch (cameraDirection)
                    {
                        case CameraDirection.IDLE:
                            camera.Move(0, 0);
                            a = 20;
                            break;
                        case CameraDirection.RIGHT:
                            camera.Move(3, 0);
                            a = 20;
                            break;
                        case CameraDirection.LEFT:
                            camera.Move(-3, 0);
                            a = 20;
                            break;
                        case CameraDirection.UP:
                            camera.Move(0, 3.5f);
                            a = 10;
                            break;
                        case CameraDirection.DOWN:
                            camera.Move(0, -3.5f);
                            a = 10;
                            break;
                    }
                }
                else
                {
                    switch (cameraDirection)
                    {
                        case CameraDirection.IDLE:
                            camera.Move(0, 0);
                            a = 20;
                            break;
                        case CameraDirection.RIGHT:
                            camera.Move(3, 0);
                            a = 20;
                            break;
                        case CameraDirection.LEFT:
                            camera.Move(-3, 0);
                            a = 20;
                            break;
                        case CameraDirection.UP:
                            camera.Move(0, 2);
                            a = 3;
                            break;
                        case CameraDirection.DOWN:
                            camera.Move(0, -2);
                            a = 3;
                            break;
                    }
                }

            }
            //カメラの向きに合わせて動く

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

        private void ChangeMotion(StartMotion startmotion)
        {

            this.startmotion = startmotion;
            motion.Initialize(startmotions[startmotion],
                new CountDownTimer(motionbpm));
        }

        private void UpdateMotion()
        {
            if (isstart && startmotion != StartMotion.START)
            {
                ChangeMotion(StartMotion.START);
            }
            if (!isstart && startmotion != StartMotion.NULL)
            {
                ChangeMotion(StartMotion.NULL);
            }
        }

    }
}




