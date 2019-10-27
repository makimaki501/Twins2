using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MusicGame.Actor;
using MusicGame.Def;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Scene
{
    class Select2:IScene
    {
        private bool isEndFlag;
        private Map2 map2;
        private GameObjectManager gameObjectManager;
        private Player player;
        private Player2 player2;

        private Camera camera;
        private Vector2 cameraPos;

        private enum CameraDirection
        {
            UP, DOWN, RIGHT, LEFT, IDLE
        }
        private CameraDirection cameraDirection;

        public Select2()
        {
            isEndFlag = false;
            gameObjectManager = new GameObjectManager();
            camera = new Camera(Screen.Width, Screen.Height);
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("2", Vector2.Zero);
           
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
            map2 = new Map2(GameDevice.Instance());
            map2.Load("StageSelect1.csv", "./csv/");
            gameObjectManager.Add(map2);

            //最初に回っている
            player = new Player(new Vector2(96 * 8 + 15, 96 * 4 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player);
            //最初に止まっている
            player2 = new Player2(new Vector2(96 * 9 + 18, 96 * 4 + 15), GameDevice.Instance(), gameObjectManager);
            gameObjectManager.Add(player2);
            player.SetPos(player2.GetPosition());


            camera.SetPosition(player2.GetPosition());
            cameraPos = player2.GetPosition();
            cameraDirection = CameraDirection.IDLE;

        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
           
            switch (player.nextscene)
            {
                case 1:
                    StageState.gamePlayState = "2-1";
                    break;
                case 2:
                    StageState.gamePlayState = "2-2";
                    break;
                case 3:
                    StageState.gamePlayState = "2-3";
                    break;
                case 4:
                    StageState.gamePlayState = "2-4";
                    break;
                case 5:
                    StageState.gamePlayState = "2-5";
                    break;
            }
            switch (player2.nextscene)
            {
                case 1:
                    StageState.gamePlayState = "2-1";
                    break;
                case 2:
                    StageState.gamePlayState = "2-2";
                    break;
                case 3:
                    StageState.gamePlayState = "2-3";
                    break;
                case 4:
                    StageState.gamePlayState = "2-4";
                    break;
                case 5:
                    StageState.gamePlayState = "2-5";
                    break;
            }
            return Scene.GamePlay;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            map2.Update(gameTime);
            StageState.isMusic = true;
            if (player.nextscene !=0)
            {
                StageState.isMusic = false;
                isEndFlag = true;
            }
            if (player2.nextscene !=0)
            {
                StageState.isMusic = false;
                isEndFlag = true;
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
                    camera.Move(0, 2);
                    break;
                case CameraDirection.DOWN:
                    camera.Move(0, -2);
                    break;

            }
        }
    }
}
