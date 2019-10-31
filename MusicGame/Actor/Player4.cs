using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Device;
using MusicGame.Scene;
using MusicGame.Util;

namespace MusicGame.Actor
{
    class Player4 : GameObject
    {
        private IGameObjectMediator mediator;
        private float r = 128;//半径
        private float radian;
        private float addRadian;
        private Vector2 Pos;
        private Vector2 position2;
        private bool reset;
        public bool stop;
        private bool _hit;
  
        public Player4(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator)
            : base("player2", position, 48, 48, gameDevice)
        {
            this.mediator = mediator;
            reset = false;
            stop = true;
            addRadian = 0.1f;
            _hit = false;
        }
        public Player4(Player4 other)
            : this(other.position, other.gameDevice, other.mediator)
        {

        }
        public override object Clone()
        {
            return new Player4(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (!stop)
            {
                if (Input.GetKeyTrigger(Keys.Space))
                {
                    if (gameObject is TitleBlock || gameObject is TitleStartBlock)
                    {
                        switch (gameObject.dir)
                        {
                            case Direction.Top:
                                if (position.Y < Pos.Y && position.X > Pos.X && position.X < Pos.X + 48)
                                {
                                    reset = true;
                                    stop = !stop;
                                    _hit = true;
                                    position = new Vector2(gameObject.GetPosition().X + 16,
                                    gameObject.GetPosition().Y + 16);
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
                                }
                                break;
                            case Direction.Left:
                                if (position.X < Pos.X && position.Y > Pos.Y+10 && position.Y < Pos.Y + 96)
                                {
                                    reset = true;
                                    stop = !stop;
                                    _hit = true;
                                    position = new Vector2(gameObject.GetPosition().X + 16,
                                    gameObject.GetPosition().Y + 16);
                                }
                                break;
                            case Direction.Right:
                                if (position.X > Pos.X + 48 && position.Y > Pos.Y+10 && position.Y < Pos.Y + 48)
                                {
                                    reset = true;
                                    stop = !stop;
                                    _hit = true;
                                    position = new Vector2(gameObject.GetPosition().X + 16,
                                    gameObject.GetPosition().Y + 16);
                                }
                                break;

                            case Direction.Free:
                                reset = true;
                                stop = !stop;
                                _hit = true;
                                position = new Vector2(gameObject.GetPosition().X + 16,
                                gameObject.GetPosition().Y + 16);
                                break;

                        }
                    }
                }
            }
            else
            {
                if (gameObject is TitleStartBlock || gameObject is Block || gameObject is TitleGorlBlock)
                {
                    if (Input.GetKeyTrigger(Keys.Space))
                    {
                        reset = true;
                        _hit = true;
                        stop = !stop;
                    }
                }

            }

            if (gameObject is Select1Block && Input.GetKeyTrigger(Keys.Space))
            {
                if (gameObject.dir == Direction.Left)
                {
                    if (position.X < Pos.X && position.Y > Pos.Y + 10 && position.Y < Pos.Y + 96)
                    {
                        reset = true;
                        stop = !stop;
                        _hit = true;
                        position = new Vector2(gameObject.GetPosition().X + 16,
                        gameObject.GetPosition().Y + 16);
                        StageState.worldsStage = 1;
                        isDeadFlag = true;
                    }
                }
            }
            if (gameObject is Select2Block && Input.GetKeyTrigger(Keys.Space))
            {
                StageState.worldsStage = 2;
                isDeadFlag = true;
            }
            if (gameObject is Select3Block && Input.GetKeyTrigger(Keys.Space))
            {
                if (gameObject.dir == Direction.Right)
                {
                    if (position.X > Pos.X + 48 && position.Y > Pos.Y + 10 && position.Y < Pos.Y + 48)
                    {
                        reset = true;
                        stop = !stop;
                        _hit = true;
                        position = new Vector2(gameObject.GetPosition().X + 16,
                        gameObject.GetPosition().Y + 16);

                        StageState.worldsStage = 3;
                        isDeadFlag = true;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!stop)
            {
                if (reset)
                {
                    SetRadian((float)Math.Atan2(position2.Y - GetPosition().Y,
                        position2.X - GetPosition().X) + MathHelper.Pi);
                    SetPos(position2);
                    reset = false;
                    _hit = false;
                }
                radian -= addRadian;
                position.X = r * (float)Math.Cos(radian) + Pos.X;
                position.Y = r * (float)Math.Sin(radian) + Pos.Y;
            }


            if (Input.GetKeyState(Keys.Enter))
            {
                radian += 0.095f;
            }
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
    }
}