using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicGame.Device;
using MusicGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor.StageBlock;
using MusicGame.Actor.Effect;
using MusicGame.Util;

namespace MusicGame.Actor
{
    class Player : GameObject
    {
        private IGameObjectMediator mediator;
        private float r = 96;//半径
        private float radian;
        private float prevRadian;
        private float addRadian;
        private Vector2 Pos;
        private bool reset;
        private Vector2 position2;
        public bool stop;
        private bool _hit;
        public int nextscene;
        public float alpha;
        public bool _dead;
        public ParticleManager particleManager;
        private Motion motion;
        private int size;

        public Player(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator, float addRadian)
            : base("player3", position, 48, 48, gameDevice)
        {
            size = 64;
            this.mediator = mediator;
            reset = false;
            stop = false;
            this.addRadian = addRadian;
            _hit = false;
            nextscene = 0;
            prevRadian = 0;
            alpha = 1;
            _dead = isDeadFlag;
            particleManager = new ParticleManager();
            motion = new Motion();

            motion.Add(0, new Rectangle(size * 0, size * 0, size, size));
            motion.Add(1, new Rectangle(size * 1, size * 0, size, size));
            motion.Add(2, new Rectangle(size * 0, size * 1, size, size));
            motion.Add(3, new Rectangle(size * 1, size * 1, size, size));
            motion.Initialize(new Range(0, 3), new CountDownTimer(0.1f));

        }
        public Player(Player other)
            : this(other.position, other.gameDevice, other.mediator, other.addRadian)
        {

        }
        public override object Clone()
        {
            return new Player(this);
        }

        public override void Draw(Renderer renderer)
        {
            particleManager.Draw(renderer);
            renderer.DrawTexturealpha(name, position,motion.DrawingRange(), alpha);
        }

        public override void Hit(GameObject gameObject)
        {
            if (StageState.isMusic)
            {
                if (!stop)
                {
                    if (Input.GetKeyTrigger(Keys.Space))
                    {
                        if (gameObject is Block)
                        {
                            switch (gameObject.dir)
                            {
                                case Direction.Top:
                                    if (position.Y < Pos.Y && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.Bottom:
                                    if (position.Y > Pos.Y + 30 && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.Left:
                                    if (position.X < Pos.X && position.Y > Pos.Y - 48 && position.Y < Pos.Y + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        prevRadian = 0;
                                        radian = 0;
                                    }

                                    break;
                                case Direction.Right:
                                    if (position.X > Pos.X + 48 && position.Y > Pos.Y - 48 && position.Y < Pos.Y + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.RightD:
                                    if (position.X > Pos.X + 48 && position.Y > Pos.Y - 48 && position.Y < Pos.Y + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        gameObject.dir = Direction.Bottom;
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.DownL:
                                    if (position.Y > Pos.Y + 30 && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        gameObject.dir = Direction.Left;
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.UpR:
                                    if (position.Y < Pos.Y && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        gameObject.dir = Direction.Right;
                                        prevRadian = 0;
                                        radian = 0;
                                    }
                                    break;
                                case Direction.Free:
                                    reset = true;
                                    stop = !stop;
                                    _hit = true;
                                    position = new Vector2(gameObject.GetPosition().X + 16,
                                    gameObject.GetPosition().Y + 16);
                                    prevRadian = 0;
                                    radian = 0;
                                    break;
                            }
                        }
                        else if (gameObject is TitleGorlBlock || gameObject is TitleStartBlock/*||gameObject is Block*/)
                        {

                            reset = true;
                            stop = !stop;
                            _hit = true;
                            position = new Vector2(gameObject.GetPosition().X + 16,
                            gameObject.GetPosition().Y + 16);
                        }
                        else if (gameObject is GorlBlock)
                        {
                            switch (gameObject.dir)
                            {
                                case Direction.Top:
                                    if (position.Y < Pos.Y && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        StageState.isClear = true;
                                    }
                                    break;
                                case Direction.Bottom:
                                    if (position.Y > Pos.Y + 30 && position.X > Pos.X - 48 && position.X < Pos.X + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        StageState.isClear = true;
                                    }
                                    break;
                                case Direction.Left:
                                    if (position.X < Pos.X && position.Y > Pos.Y - 35 && position.Y < Pos.Y + 96)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        StageState.isClear = true;
                                    }

                                    break;
                                case Direction.Right:
                                    if (position.X > Pos.X + 48 && position.Y > Pos.Y - 48 && position.Y < Pos.Y + 48)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                        StageState.isClear = true;
                                    }
                                    break;
                            }

                        }
                    }
                }
                else
                {
                    if (Input.GetKeyTrigger(Keys.Space))
                    {
                        if (gameObject is TitleStartBlock || gameObject is Block
                           || gameObject is TitleGorlBlock
                           || gameObject is StartBlock)
                        {

                            reset = true;
                            _hit = true;
                            stop = !stop;
                        }
                    }
                }

                if (gameObject is BackBlock && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 1;
                    StageState.sceneNumber = 3;
                    StageState.isDead = true;
                }
                if (gameObject is Stage2Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 2;
                    StageState.sceneNumber = 3;
                    StageState.isDead = true;
                }
                if (gameObject is Stage3Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 3;
                    StageState.sceneNumber = 3;
                    StageState.isDead = true;
                }
                if (gameObject is Stage4Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 4;
                    StageState.sceneNumber = 3;
                    StageState.isDead = true;
                }
                if (gameObject is Stage5Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 5;
                    StageState.sceneNumber = 3;
                    StageState.isDead = true;
                }
                if (gameObject is NextBlock && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.worldsStage += 1;
                    if (StageState.worldsStage > 3)
                    {
                        StageState.worldsStage = 3;
                    }
                    StageState.sceneNumber = 2;
                    StageState.isDead = true;
                }
                if (gameObject is BackBlock && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.sceneNumber = 1;
                    StageState.isDead = true;
                }

            }
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            if (!stop)
            {
                if (reset)
                {
                    SetRadian((float)Math.Atan2(position2.Y - GetPosition().Y,
                        position2.X - GetPosition().X) + MathHelper.Pi);
                    SetPos(position2);
                    reset = false;
                }
                radian += addRadian;
                position.X = r * (float)Math.Cos(radian) + Pos.X;
                position.Y = r * (float)Math.Sin(radian) + Pos.Y;
                prevRadian = radian;
                var rnd = GameDevice.Instance().GetRandom();
                int x = rnd.Next((int)GetPosition().X +40, (int)GetPosition().X+50 );
                int y = rnd.Next((int)GetPosition().Y+40, (int)GetPosition().Y+40 );
                particleManager.Playerparticle("player1particle", new Vector2(x,y), 1f,0.5f, 0.5f, 100, 1);
                particleManager.Playerparticle("player1particle", new Vector2(x,y), 1f, 0.5f, 0.5f, 100, 1);
            }
            if (StageState.isMusic)
            {
                if (prevRadian >= 6.3f)
                {
                    //isDeadFlag = true;
                }
            }

            if (StageState.isDead)
            {
                isDeadFlag = true;
                StageState.isDead = false;
            }
            if (Input.GetKeyState(Keys.Enter))
            {
                radian -= 0.095f;
            }

            float delta =(float) gameTime.ElapsedGameTime.TotalSeconds;
            particleManager.Update(delta);



            Console.WriteLine(_hit);
        }

        public Vector2 GetPosition2()
        {
            return position2;
        }
        public void SetPosition2(Vector2 position2)
        {
            this.position2 = position2;
        }
        /// <summary>
        /// 中心位置の設定
        /// </summary>
        /// <param name="Pos"></param>
        public void SetPos(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        /// <summary>
        /// ラジアンの取得
        /// </summary>
        /// <returns></returns>
        public float GetRadian()
        {
            return radian;
        }
        /// <summary>
        /// 角度の取得
        /// </summary>
        /// <returns></returns>
        public float GetDegree()
        {
            float degree = radian / 2 * MathHelper.Pi * 360;
            return degree;
        }
        /// <summary>
        /// ラジアンの設定
        /// </summary>
        /// <param name="radian"></param>
        public void SetRadian(float radian)
        {
            this.radian = radian;
        }
        /// <summary>
        /// 角度の設定
        /// </summary>
        /// <param name="degree"></param>
        public void SetDegree(float degree)
        {

            this.radian = degree / 360 * 2 * MathHelper.Pi;
        }

        public bool IsStop()
        {
            return stop;
        }

        public bool IsHit()
        {
            return _hit;
        }

        public float AddRadian()
        {
            return addRadian;
        }

        public bool IsDead(bool _dead)
        {
            return isDeadFlag == _dead;
        }
    }
}
