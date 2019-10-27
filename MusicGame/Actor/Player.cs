﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicGame.Device;
using MusicGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor.StageBlock;

namespace MusicGame.Actor
{
    class Player : GameObject
    {
        private IGameObjectMediator mediator;
        private float r = 96;//半径
        private float radian;
        private float addRadian;
        private Vector2 Pos;
        private bool reset;
        private Vector2 position2;
        public bool stop;
        private bool _hit;
        public int nextscene;
        public float alpha;

        public Player(Vector2 position, GameDevice gameDevice, IGameObjectMediator mediator,float addRadian)
            : base("player3", position, 24, 24, gameDevice)
        {
            this.mediator = mediator;
            reset = false;
            stop = false;
           this.addRadian= addRadian;
            _hit = false;
            nextscene = 0;
            alpha = 1;
        }
        public Player(Player other)
            : this(other.position, other.gameDevice, other.mediator,other.addRadian)
        {

        }
        public override object Clone()
        {
            return new Player(this);
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexturealpha(name, position, alpha);
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
                                    if (position.Y < Pos.Y && position.X > Pos.X - 35 && position.X < Pos.X + 126)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                    }
                                    break;
                                case Direction.Bottom:
                                    if (position.Y > Pos.Y + 30 && position.X > Pos.X && position.X - 35 < Pos.X + 126)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
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
                                    }

                                    break;
                                case Direction.Right:
                                    if (position.X > Pos.X + 30 && position.Y > Pos.Y - 35 && position.Y < Pos.Y + 126)
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
                            Console.WriteLine(gameObject.dir);
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
                                    if (position.Y < Pos.Y && position.X > Pos.X - 35 && position.X < Pos.X + 126)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                    }
                                    break;
                                case Direction.Bottom:
                                    if (position.Y > Pos.Y + 30 && position.X > Pos.X && position.X - 35 < Pos.X + 126)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                    }
                                    break;
                                case Direction.Left:
                                    if (position.X < Pos.X && position.Y > Pos.Y - 35 && position.Y < Pos.Y + 126)
                                    {
                                        reset = true;
                                        stop = !stop;
                                        _hit = true;
                                        position = new Vector2(gameObject.GetPosition().X + 16,
                                        gameObject.GetPosition().Y + 16);
                                    }

                                    break;
                                case Direction.Right:
                                    if (position.X > Pos.X + 30 && position.Y > Pos.Y - 35 && position.Y < Pos.Y + 126)
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

                if (gameObject is Stage1Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 1;
                    isDeadFlag = true;
                }
                if (gameObject is Stage2Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 2;
                    isDeadFlag = true;
                }
                if (gameObject is Stage3Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 3;
                    isDeadFlag = true;
                }
                if (gameObject is Stage4Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 4;
                    isDeadFlag = true;
                }
                if (gameObject is Stage5Block && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.stageStage = 5;
                    isDeadFlag = true;
                }

                if(gameObject is GorlBlock && Input.GetKeyTrigger(Keys.Space))
                {
                    StageState.isClear = true;
                    isDeadFlag = true;
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
                }
                radian += addRadian;
                position.X = r * (float)Math.Cos(radian) + Pos.X;
                position.Y = r * (float)Math.Sin(radian) + Pos.Y;
            }



            if (Input.GetKeyState(Keys.Enter))
            {
                radian -= 0.095f;
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

        public float AddRadian()
        {
            return addRadian;
        }
    }
}
