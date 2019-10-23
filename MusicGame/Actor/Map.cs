using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MusicGame.Device;
using MusicGame.Util;

namespace MusicGame.Actor
{
    class Map
    {
        private List<List<GameObject>> mapList;//ListのListで縦横の２次元配列を表現
        private GameDevice gameDevice;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice"></param>
        public Map(GameDevice gameDevice)
        {
            mapList = new List<List<GameObject>>();//マップの実体生成
            this.gameDevice = gameDevice;
        }

        /// <summary>
        /// ブロックの追加
        /// </summary>
        /// <param name="lineCnt"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private List<GameObject> addBlock(int lineCnt, string[] line)//privateメソッドなので戦闘小文字
        {
            //コピー元オブジェクト登録用でディクショナリ
            Dictionary<string, GameObject> objectList = new Dictionary<string, GameObject>();

            objectList.Add("0", new Space(Vector2.Zero, gameDevice));
            objectList.Add("1", new Block("Yoko",Vector2.Zero, gameDevice));
            objectList.Add("2", new TitleStartBlock(Vector2.Zero, gameDevice));
            objectList.Add("10", new TitleGorlBlock(Vector2.Zero, gameDevice));

            //作業用
            List<GameObject> workList = new List<GameObject>();

            int colCnt = 0;//列カウント用
            //渡された１行から１つずつ作業リストに登録
            foreach (var s in line)
            {
                try
                {
                    //ディクショナリから元データ取り出し、クローン機能で複製
                    GameObject work = (GameObject)objectList[s].Clone();
                    work.SetPosition(new Vector2(colCnt * work.GetHight(),
                        lineCnt * work.GetWidth()));
                    workList.Add(work);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //列カウンタを増やす
                colCnt += 1;
            }
            return workList;
        }

        /// <summary>
        /// CSVReaderを使ってMapの読み込み
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        public void Load(string filename, string path = "./")
        {
            CSVReader csvReader = new CSVReader();
            csvReader.Read(filename, path);

            var data = csvReader.GetData();//List<string[]>型で取得

            //１行ごとmapListに追加していく
            for (int lineCnt = 0; lineCnt < data.Count(); lineCnt++)
            {
                mapList.Add(addBlock(lineCnt, data[lineCnt]));
            }
        }

        /// <summary>
        /// マップリストのクリア
        /// </summary>
        public void Unload()
        {
            mapList.Clear();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (var list in mapList)//listはList<GameObject>型
            {
                foreach (var obj in list)
                {
                    //objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                    {
                        continue;
                    }

                    //更新
                    obj.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="gameObject"></param>
        public void Hit(GameObject gameObject)
        {
            Point work = gameObject.getRectangle().Location;//左上の座標を取得 
            //配列の何行何列目にいるかを計算
            int x = work.X / 128;
            int y = work.Y /128;

            //移動で食い込んでる時の修正
            if (x < 1)
            {
                x = 1;
            }
            if (y < 1)
            {
                y = 1;
            }

            Range yRange = new Range(0, mapList.Count() - 1);//行の範囲
            Range xRange = new Range(0, mapList[0].Count() - 1);//列の範囲


            for (int row = y - 1; row <= (y + 1); row++)
            {
                for (int col = x - 1; col <= (x + 1); col++)
                {
                    //配列外なら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row))
                    {
                        continue;
                    }

                    //その場所のオブジェクトを取得
                    GameObject obj = mapList[row][col];

                    //objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                    {
                        continue;
                    }

                    //衝突判定
                    if (obj.IsCollision(gameObject))
                    {
                        gameObject.Hit(obj);
                    }
                }
            }
        }


        public void Draw(Renderer renderer)
        {
            foreach (var list in mapList)
            {
                foreach (var obj in list)
                {
                    obj.Draw(renderer);
                }
            }
        }
    }
}
