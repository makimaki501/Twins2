using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MusicGame.Def;
using MusicGame.Device;
using MusicGame.Util;

namespace MusicGame.Scene
{
    class Menu : IScene
    {
        private bool isEndFlag;
        private Scene nextscene;
        private Motion Tmotion;
        private Motion Smotion;
        private Motion Rmotion;
        private Dictionary<Direction, Range> directiondic;
        private Direction Tdirection;
        private Direction Sdirection;
        private Direction Rdirection;
        private int sentakucnt;
        private int cnt = 0;

        public enum Direction
        {
            NO, YES
        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("menuanaunse", new Vector2(Screen.Width / 2 - 400, 50));
            renderer.DrawTexture("menuanaunse2", new Vector2(Screen.Width / 2 - 400, 800));
            renderer.DrawTexture("titlebutton", new Vector2(1320, 400), Tmotion.DrawingRange(),Color.White);
            renderer.DrawTexture("selectbutton", new Vector2(835, 400), Smotion.DrawingRange(),Color.White);
            renderer.DrawTexture("retrybutton", new Vector2(350, 400), Rmotion.DrawingRange(),Color.White);
            renderer.End();
        }

        public void Initialize()
        {

            isEndFlag = false;
            Tmotion = new Motion();
            Tmotion.Add(0, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Tmotion.Add(1, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Tmotion.Add(2, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Tmotion.Add(3, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Tmotion.Initialize(new Range(0, 1), new CountDownTimer(1));

            Smotion = new Motion();
            Smotion.Add(0, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Smotion.Add(1, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Smotion.Add(2, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Smotion.Add(3, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Smotion.Initialize(new Range(0, 1), new CountDownTimer(1));

            Rmotion = new Motion();
            Rmotion.Add(0, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Rmotion.Add(1, new Rectangle(250 * 0, 250 * 0, 250, 250));
            Rmotion.Add(2, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Rmotion.Add(3, new Rectangle(250 * 1, 250 * 0, 250, 250));
            Rmotion.Initialize(new Range(0, 1), new CountDownTimer(1));

            Tdirection = Direction.NO;
            Sdirection = Direction.NO;
            Rdirection = Direction.NO;
            directiondic = new Dictionary<Direction, Range>()
            {
                {Direction.NO,new Range(0,1) },
                {Direction.YES,new Range(2,3) },
            };

            sentakucnt = 0;

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
            Tmotion.Update(gameTime);
            Smotion.Update(gameTime);
            Rmotion.Update(gameTime);
            MotionUpdate();
            if (Input.GetKeyState(Keys.Space))
            {

                cnt++;

                if (cnt >= 120)
                {
                    switch (sentakucnt)
                    {
                        case 0:
                            nextscene = Scene.GamePlay;
                            isEndFlag = true;
                            break;
                        case 1:
                            nextscene = Scene.Select1;
                            isEndFlag = true;
                            break;
                        case 2:
                            nextscene = Scene.Title;
                            isEndFlag = true;
                            break;
                    }
                    cnt = 0;
                }

            }
            if (Input.IsKeyUp(Keys.Space))
            {
                sentakucnt += 1;
                cnt = 0;
            }
            if (sentakucnt > 2)
            {
                sentakucnt = 0;
            }




        }

        public void ChangeMotion(Direction direction, string name)
        {
            switch (name)
            {
                case "Tmotion":
                    this.Tdirection = direction;
                    Tmotion.Initialize(directiondic[direction],
                        new CountDownTimer(1));
                    break;
                case "Smotion":
                    this.Sdirection = direction;
                    Smotion.Initialize(directiondic[direction],
                        new CountDownTimer(1));
                    break;
                case "Rmotion":
                    this.Rdirection = direction;
                    Rmotion.Initialize(directiondic[direction],
                        new CountDownTimer(1));
                    break;
            }

        }

        public void MotionUpdate()
        {
            if (sentakucnt == 0 && Rdirection == Direction.NO)
            {
                ChangeMotion(Direction.YES, "Rmotion");
            }
            if (sentakucnt == 1 && Sdirection == Direction.NO)
            {
                ChangeMotion(Direction.YES, "Smotion");
            }
            if (sentakucnt == 2 && Tdirection == Direction.NO)
            {
                ChangeMotion(Direction.YES, "Tmotion");
            }
            if (sentakucnt == 0)
            {
                ChangeMotion(Direction.NO, "Tmotion");
                ChangeMotion(Direction.NO, "Smotion");
            }
            if (sentakucnt == 1)
            {
                ChangeMotion(Direction.NO, "Tmotion");
                ChangeMotion(Direction.NO, "Rmotion");
            }
            if (sentakucnt == 2)
            {
                ChangeMotion(Direction.NO, "Rmotion");
                ChangeMotion(Direction.NO, "Smotion");
            }
        }
    }
}
