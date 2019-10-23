using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MusicGame.Device;

namespace MusicGame.Actor
{
    enum Direction
    {
        Top,//上
        Bottom,//下
        Left,//左
        Right,//右
    }


    abstract class GameObject : ICloneable//コピー機能を追加
    {
        protected string name;//アセット名
        protected Vector2 position;//位置
        protected int width;//幅
        protected int height;//高さ
        protected bool isDeadFlag = false;//死亡フラグ
        protected GameDevice gameDevice;//ゲームデバイス
        protected GameObjectID id = GameObjectID.NONE;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="gameDevice">ゲームデバイス</param>
        public GameObject(string name, Vector2 position, int width, int height, GameDevice gameDevice)
        {
            this.name = name;
            this.position = position;
            this.width = width;
            this.height = height;
            this.gameDevice = gameDevice;
        }

        /// <summary>
        /// 位置の設定
        /// </summary>
        /// <param name="position">設定した位置</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// 位置の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// オブジェクト幅の取得
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// オブジェクトの高さの取得
        /// </summary>
        /// <returns></returns>
        public int GetHight()
        {
            return height;
        }

        //抽象メソッド
        public abstract object Clone();//IConeableで必ず必要
        public abstract void Update(GameTime gameTime);//更新
        public abstract void Hit(GameObject gameObject);//ヒット通知

        //仮想メソッド
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position + gameDevice.GetDisplayModify());
        }

        /// <summary>
        /// 死んでいるか？
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }

        /// <summary>
        /// 当たり判定用、短形情報の取得
        /// </summary>
        /// <returns></returns>
        public Rectangle getRectangle()
        {
            //短径の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        /// <summary>
        /// 短径同士の当たり判定
        /// </summary>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public bool IsCollision(GameObject otherObj)
        {
            //RectangleクラスのInitializeメソッドで重なり判定
            return this.getRectangle().Intersects(otherObj.getRectangle());
        }

        public Direction CheckDirection(GameObject otherObj)
        {
            //中心位置の取得
            Point thisCenter = this.getRectangle().Center;//自分の中心位置
            Point otherCenter = otherObj.getRectangle().Center;//相手の中心位置

            //向きのベクトルを取得
            Vector2 dir =
                new Vector2(thisCenter.X, thisCenter.Y) -
                new Vector2(otherCenter.X, otherCenter.Y);

            //当たっている側面をリターンする
            //x成分とy成分でどちらのほうが量が多いか
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                //xの向きが正の時
                if (dir.X > 0)
                {
                    return Direction.Right;
                }
                return Direction.Left;
            }
            //y成分が大きく、正の値か？
            if (dir.Y > 0)
            {
                return Direction.Bottom;
            }
            return Direction.Top;//プレイヤーがブロックに乗った
        }

        public GameObjectID GetID()
        {
            return id;
        }

        public void SetID(GameObjectID id)
        {
            this.id = id;
        }

        public virtual void CorrectPosition(GameObject other)
        {
            //当たった面の取得
            Direction dir = this.CheckDirection(other);

            if (dir == Direction.Top)
            {
                position.Y = other.getRectangle().Top - this.height;
            }
            else if (dir == Direction.Right)
            {
                position.X = other.getRectangle().Right;
            }
            else if (dir == Direction.Left)
            {
                position.X = other.getRectangle().Left - this.width;
            }
            else if (dir == Direction.Bottom)
            {
                position.Y = other.getRectangle().Bottom;
            }
        }
    }

}
