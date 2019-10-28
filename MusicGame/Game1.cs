using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MusicGame.Device;
using MusicGame.Def;
using MusicGame.Scene;

namespace MusicGame
{
    /// <summary>
    /// This is the main type for your game.aaa
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト

        private GameDevice gameDevice; //ゲームデバイスオブジェクト
        private Renderer renderer; //描画オブジェクト

        private SceneManager sceneManager;

        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";

            //画面サイズ設定
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //ゲームデバイスの実体を取得
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);

            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Scene.Title, new Title());
            sceneManager.Add(Scene.Scene.Select1, new Select1());
            sceneManager.Add(Scene.Scene.GamePlay, new GamePlay());
            sceneManager.Add(Scene.Scene.Menu, new Menu());

            sceneManager.Change(Scene.Scene.Title);
            
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            renderer = gameDevice.GetRenderer();

            renderer.LoadContent("player1", "./Texture/");
            renderer.LoadContent("player2", "./Texture/");
            renderer.LoadContent("player3", "./Texture/");
            renderer.LoadContent("player4", "./Texture/");
            renderer.LoadContent("persona", "./Texture/");
            renderer.LoadContent("persona1", "./Texture/");
            renderer.LoadContent("Idle", "./Texture/");
            renderer.LoadContent("Idle128", "./Texture/");
            renderer.LoadContent("Tate", "./Texture/");
            renderer.LoadContent("LeftUp", "./Texture/");
            renderer.LoadContent("LeftDown", "./Texture/");
            renderer.LoadContent("RightUp", "./Texture/");
            renderer.LoadContent("RightDown", "./Texture/");
            renderer.LoadContent("YokoTate", "./Texture/");
            renderer.LoadContent("TateYoko", "./Texture/");
            renderer.LoadContent("Yoko", "./Texture/");
            renderer.LoadContent("GorlMove", "./Texture/");
            renderer.LoadContent("start", "./Texture/");
            //renderer.LoadContent("1", "./Texture/");
            //renderer.LoadContent("2", "./Texture/");
            //renderer.LoadContent("3", "./Texture/");
            //renderer.LoadContent("go", "./Texture/");
            renderer.LoadContent("onpu1", "./Texture/");
            renderer.LoadContent("onpu2", "./Texture/");
            renderer.LoadContent("onpu3", "./Texture/");
            renderer.LoadContent("title", "./Texture/");
            renderer.LoadContent("star", "./Texture/");
            renderer.LoadContent("circle", "./Texture/");
            renderer.LoadContent("clear", "./Texture/");
            renderer.LoadContent("titlebutton", "./Texture/");
            renderer.LoadContent("retrybutton", "./Texture/");
            renderer.LoadContent("selectbutton", "./Texture/");
            renderer.LoadContent("nextbutton", "./Texture/");
            renderer.LoadContent("menuanaunse", "./Texture/");
            renderer.LoadContent("menuanaunse2", "./Texture/");
            renderer.LoadContent("1-1", "./Texture/");
            renderer.LoadContent("1-2", "./Texture/");
            renderer.LoadContent("1-3", "./Texture/");
            renderer.LoadContent("1-4", "./Texture/");
            renderer.LoadContent("1-5", "./Texture/");
            renderer.LoadContent("2-1", "./Texture/");
            renderer.LoadContent("2-2", "./Texture/");
            renderer.LoadContent("2-3", "./Texture/");
            renderer.LoadContent("2-4", "./Texture/");
            renderer.LoadContent("2-5", "./Texture/");
            renderer.LoadContent("3-1", "./Texture/");
            renderer.LoadContent("3-2", "./Texture/");
            renderer.LoadContent("3-3", "./Texture/");
            renderer.LoadContent("3-4", "./Texture/");
            renderer.LoadContent("3-5", "./Texture/");
            renderer.LoadContent("world1", "./Texture/");
            renderer.LoadContent("world2", "./Texture/");
            renderer.LoadContent("world3", "./Texture/");
            renderer.LoadContent("stagemark1", "./Texture/");
            renderer.LoadContent("stagemark2", "./Texture/");
            renderer.LoadContent("stagemark3", "./Texture/");
            renderer.LoadContent("stagemark4", "./Texture/");
            renderer.LoadContent("stagemark5", "./Texture/");
            renderer.LoadContent("gameover", "./Texture/");
            renderer.LoadContent("backGround1", "./Texture/");
            renderer.LoadContent("backGround2", "./Texture/");
            renderer.LoadContent("backGround3", "./Texture/");
            renderer.LoadContent("titleselect1", "./Texture/");
            renderer.LoadContent("titleselect2", "./Texture/");
            renderer.LoadContent("titleselect3", "./Texture/");
            renderer.LoadContent("snow1", "./Texture/");
            renderer.LoadContent("snow2", "./Texture/");
           



            Sound sound = gameDevice.GetSound();
            string filepath = "./";
            sound.LoadSE("kettei", filepath);
            sound.LoadSE("sentaku", filepath);
           
            

            sound.LoadBGM("WorldSelect", filepath);
            sound.LoadBGM("Title", filepath);
         
            sound.LoadBGM("1-1", filepath);
            sound.LoadBGM("1-2", filepath);
            sound.LoadBGM("1-3", filepath);
            sound.LoadBGM("1-4", filepath);
            sound.LoadBGM("1-5", filepath);
            sound.LoadBGM("2-1", filepath);
            sound.LoadBGM("2-2", filepath);
            sound.LoadBGM("2-3", filepath);
            sound.LoadBGM("2-4", filepath);
            sound.LoadBGM("2-5", filepath);
            sound.LoadBGM("3-1", filepath);
            sound.LoadBGM("3-2", filepath);
            sound.LoadBGM("3-3", filepath);
            sound.LoadBGM("3-4", filepath);
            sound.LoadBGM("3-5", filepath);
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            gameDevice.Update(gameTime); //他のところでこれをやると入力処理がおかしくなる
            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            sceneManager.Draw(renderer);

            base.Draw(gameTime);
        }
    }
}
