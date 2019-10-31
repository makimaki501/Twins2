using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Actor.Effect
{
    class ParticleManager
    {
        //パーティクルのリスト
        private List<Particle> particles = new List<Particle>();
        private Particle p;
        public string name;
        private int cnt;
        private bool iscount;

        public ParticleManager()
        {
            cnt = 0;
            iscount = false;
        }

        public void Clear(string name)
        {
            foreach (var p in particles)
            {
                if (p.name == name)
                {
                    p.isDeadFlag = true;
                }
            }
        }

        public void Clear()
        {
            particles.Clear();
        }

        public void Update(float delta)
        {
            cnt++;
            //一括更新
            foreach (var p in particles)
            {
                p.Update(delta);
            }

            //死亡しているものはリストから削除
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].IsDead())
                {
                    particles.Remove(particles[i]);
                }
            }

        }

        public void Shutdown()
        {
            Clear();
        }

        public void Draw(Renderer renderer)
        {
            //一括描画
            foreach (var p in particles)
            {
                p.Draw(renderer);
            }

        }

        public void Texture(string name, Vector2 position, float scale, float shrinkRate, float duration, int count)
        {
            p = new Particle(name, position, 0, 0, scale, shrinkRate, duration, 1, 0, count, Color.White);

            //パーティクルを足す
            particles.Add(p);
        }

        public void Backparticle(string name,Vector2 position,float speed,float angle, float scale, float shrinkRate,float alpha,float alphaamount,float amount, float duration)
        {
            for (int i = 0; i < amount; i++)
            {
                var rnd = GameDevice.Instance().GetRandom();
                p = new Particle(name, position,speed, angle, scale, shrinkRate, duration, alpha, alphaamount, 1, Color.White);

                //パーティクルを足す
                particles.Add(p);
            }
        }

        public void Playerparticle(string name,Vector2 position, float scale, float shrinkRate, float duration, int amount, int count)
        {
            for (int i = 0; i < amount; i++)
            {
                var rnd = GameDevice.Instance().GetRandom();

                p = new Particle(name, position, 0, 0, scale, shrinkRate, duration, 1, 0.5f, 1, Color.White);

                //パーティクルを足す
                particles.Add(p);
            }

        }
        public void Down(string name,float shrinkRate, float duration, float alpha, float alphaAmount, int amount, int maxSpeed, Color color)
        {
            var rnd = GameDevice.Instance().GetRandom();

            for (int i = 0; i < amount; i++)
            {
                float angle = 180;
                float scale = rnd.Next(1, 2);
                float speed = rnd.Next(10, maxSpeed);
                Vector2 position = new Vector2(rnd.Next(1920), rnd.Next(1080));

                //新しいパーティクルを作る
                p = new Particle(name, position, speed, angle, scale, shrinkRate, duration, alpha, alphaAmount, 1, color);

                particles.Add(p);
            }
        }


        public void TitleParticle(string name, Vector2 position)
        {
            var rnd = GameDevice.Instance().GetRandom();
            Color color = Color.White;
            p = new Particle(name, position, 0, 0, 1, 0, 1, 1, 0, 2, color);
            particles.Add(p);
        }

        public void Circle(string name, Vector2 position, float shrinkRate, float duration, float alpha, float alphaAmount, int amount, int maxSpeed, Color color)
        {
            var rnd = GameDevice.Instance().GetRandom();

            for (int i = 0; i < amount; i++)
            {
                int angle = rnd.Next(0, 360);//ランダムな角度取得
                float scale = rnd.Next(3, 5);
                float speed = rnd.Next(1, maxSpeed);//ランダムな速度取得

                //新しいパーティクルを作る
                p = new Particle(name, position, speed, angle, scale, shrinkRate, duration, alpha, alphaAmount, 1, color);

                //パーティクルを足す
                particles.Add(p);
            }
        }


        public void Circle2(string name, Vector2 position, int minAngle, int maxAangle, float scale, float shrinkRate, float duration, float alpha, float alphaAmount, int amount, int maxSpeed, Color color)
        {
            var rnd = GameDevice.Instance().GetRandom();

            this.name = name;
            for (int i = 0; i < amount; i++)
            {
                int angle = rnd.Next(minAngle, maxAangle);//ランダムな角度取得

                float speed = maxSpeed;//ランダムな速度取得


                //新しいパーティクルを作る
                p = new Particle(name, position, speed, angle, scale, shrinkRate, duration, alpha, alphaAmount, 1, color);

                //パーティクルを足す
                particles.Add(p);
            }
        }

        /// <summary>
        /// クラッカー左
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="shrinkRate"></param>
        /// <param name="duration"></param>
        /// <param name="amount"></param>
        /// <param name="maxSpeed"></param>
        public void LeftCraccar(string name, Vector2 position, float shrinkRate, float duration, int amount, int maxSpeed)
        {
            var rnd = GameDevice.Instance().GetRandom();

            this.name = name;
            for (int i = 0; i < amount; i++)
            {
                int angle = rnd.Next(-65, -15);//ランダムな角度取得

                float alpha = 1;
                float alphaAmount = 0;
                float scale = rnd.Next(1, 4);

                float speed = rnd.Next(100, maxSpeed);//ランダムな速度取得

                float r = 1;
                float g = 1;
                float b = 1;

                Color color = new Color(r + rnd.Next(3), g + rnd.Next(3), b + rnd.Next(3));
                if (r >= 255)
                {
                    r = 0;
                }
                if (g >= 255)
                {
                    g = 0;
                }
                if (b >= 255)
                {
                    b = 0;
                }

                //新しいパーティクルを作る
                p = new Particle(name, position, speed, angle, scale, shrinkRate, duration, alpha, alphaAmount, 1, color);

                //パーティクルを足す
                particles.Add(p);
            }
        }

        /// <summary>
        /// クラッカー右
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="shrinkRate"></param>
        /// <param name="duration"></param>
        /// <param name="amount"></param>
        /// <param name="maxSpeed"></param>
        public void RightCraccar(string name, Vector2 position, float shrinkRate, float duration, int amount, int maxSpeed)
        {
            var rnd = GameDevice.Instance().GetRandom();

            this.name = name;
            for (int i = 0; i < amount; i++)
            {
                int angle = rnd.Next(15, 65);//ランダムな角度取得

                float alpha = 1;
                float alphaAmount = 0;
                float scale = rnd.Next(1, 4);

                float speed = rnd.Next(100, maxSpeed);//ランダムな速度取得

                float r = 1;
                float g = 1;
                float b = 1;

                Color color = new Color(r + rnd.Next(3), g + rnd.Next(3), b + rnd.Next(3));
                if (r >= 255)
                {
                    r = 0;
                }
                if (g >= 255)
                {
                    g = 0;
                }
                if (b >= 255)
                {
                    b = 0;
                }

                //新しいパーティクルを作る
                p = new Particle(name, position, speed, angle, scale, shrinkRate, duration, alpha, alphaAmount, 1, color);

                //パーティクルを足す
                particles.Add(p);
            }
        }

        public bool IsCount(float number)
        {
            if (cnt % number == 0)
            {
                iscount = true;
            }
            else
            {
                iscount = false;
            }
            return iscount;
        }
    }
}
