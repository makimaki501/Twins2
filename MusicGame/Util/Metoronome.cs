using Microsoft.Xna.Framework;
using MusicGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Util
{
    class Metoronome
    {
        public int cnt;
        public int beat;
        public float bpm;
        public float fpsNumber;
        private Sound sound;
        public float count;
        public bool isCount;
        public float countup;
        public bool isBeat;

        public Metoronome()
        {
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Initialize()
        {
            cnt = 0;
            beat = 0;
            count = 0;
            isCount = false;
            isBeat = false;
        }

        public void SetBpm(float bpm)
        {
            this.bpm = bpm;
            fpsNumber = (60 / bpm) * 60;//この式でBpmから1拍何fpsかを求められる
        }

        public void Update(GameTime gameTime)
        {
            isBeat = false;
            //1フレームごとにカウント
            cnt++;

            if (cnt % fpsNumber == 0)
            {
                beat++;//何拍たったかわかる
                sound.PlaySE("switch");
                isBeat = true;
            }
        }

        public bool IsBeat()
        {
            return isBeat;
        }

        public int GetBeat()
        {
            return beat;
        }

        public void SetCount()
        {
            count = 0;
            countup = 0;
        }

        public void CountUpdate()
        {
            countup++;
            if (countup % fpsNumber == 0)
            {
                count++;
            }
        }

        public void CountClear()
        {
            countup = 0;
        }

        public bool IsCount(float count)
        {
            if (this.count == count)
            {
                return true;
            }
            return false;
        }
        public bool IsCount(float count,float count2)
        {
            if (this.count >= count&&this.count<=count2)
            {
                return true;
            }
            return false;
        }

        public float GetCount()
        {
            return count;
        }
    }
}
