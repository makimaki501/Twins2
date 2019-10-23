using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MusicGame.Actor;
using MusicGame.Device;

namespace MusicGame.Scene
{
    class GameObjectManager : IGameObjectMediator
    {
        private List<GameObject> gameObjectList;
        private List<GameObject> addGameObjects;

        private Map map;
        private Map2 map2;

        /// <summary>
        /// コンストラクタ 
        /// </summary>
        public GameObjectManager()
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //リストの実体があればクリアし、なければ実体生成
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<GameObject>();
            }

            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<GameObject>();
            }
        }

        /// <summary>
        /// ゲームオブジェクトの追加
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        /// <summary>
        /// マップの追加
        /// </summary>
        /// <param name="map"></param>
        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }
        public void Add(Map2 map2)
        {
            if (map2 == null)
            {
                return;
            }
            this.map2 = map2;
        }

        /// <summary>
        /// マップとの当たり判定
        /// </summary>
        private void hitToMap()
        {
            if (map == null)
            {
                return;
            }
            //全てのオブジェクトとマップとのヒット通知
            foreach (var obj in gameObjectList)
            {
                map.Hit(obj);
            }
        }

        private void HitToMap2()
        {
            if (map2 == null)
            {
                return;
            }
            //全てのオブジェクトとマップとのヒット通知
            foreach (var obj in gameObjectList)
            {
                map2.Hit(obj);
            }
        }

        /// <summary>
        /// ゲームオブジェクトリストとのヒット通知
        /// </summary>
        private void hitToGameObject()
        {
            //ゲームオブジェクトリストを繰り返し
            foreach (var c1 in gameObjectList)
            {
                //同じゲームオブジェクトリストを繰り返し
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        //同じキャラか、キャラが死んだら次へ
                        continue;
                    }

                    //衝突判定
                    if (c1.IsCollision(c2))
                    {
                        //ヒット通知 
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }

        /// <summary>
        /// 死亡キャラをリストから削除
        /// </summary>
        private void removeDeadCharacters()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //全キャラ更新
            foreach (var c in gameObjectList)
            {
                c.Update(gameTime);
            }

            //キャラクタの追加
            foreach (var c in addGameObjects)
            {
                gameObjectList.Add(c);
            }

            //追加終了後、追加リストはクリア
            addGameObjects.Clear();

            //当たり判定
            hitToMap();
            HitToMap2();
            hitToGameObject();

            //死亡フラグが立っているキャラをすべて削除
            removeDeadCharacters();
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }


        /// <summary>
        /// ゲームオブジェクトの追加
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        /// <summary>
        /// プレイヤーの取得
        /// </summary>
        /// <returns></returns>
        public GameObject GetPlayer()
        {
            GameObject find = gameObjectList.Find(c => c is Player);
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }

        /// <summary>
        /// プレイヤーが死んでいるか？
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerDead()
        {
            GameObject find = gameObjectList.Find(c => c is Player);

            return (find == null || find.IsDead());
        }

        public GameObject GetGameObject(GameObjectID id)
        {
            GameObject find = gameObjectList.Find(c => c.GetID() == id);

            //発見したオブジェクトがnull出ないときかつ死んでないとき
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }

        public List<GameObject> GetGameObjectList(GameObjectID id)
        {
            List<GameObject> list = gameObjectList.FindAll(c => c.GetID() == id);

            List<GameObject> aliveList = new List<GameObject>();
            foreach (var c in list)
            {
                if (!c.IsDead())
                {
                    aliveList.Add(c);
                }
            }
            return aliveList;
        }
    }
}
