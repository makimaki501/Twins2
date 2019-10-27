using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGame.Device
{
    class Camera
    {
        Vector2 position=Vector2.Zero;
        Vector2 zoom=Vector2.One;
        Rectangle visibleArea;
        float rotation = 0.0f;
        Vector2 screenPosition = Vector2.Zero;

        public Camera( int width,int height)
        {
            visibleArea = new Rectangle(0, 0, width, height);
            position = new Vector2(width / 2, height / 2);
  
            screenPosition = new Vector2(width / 2, height / 2);
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                visibleArea.X = (int)(position.X - visibleArea.Width / 2);
                visibleArea.Y = (int)(position.Y - visibleArea.Height / 2);
            }
        }

        public Vector2 SetPosition(Vector2 position)
        {
            this.position = position;
            return position;
        }

        public void Move(float x,float y)
        {
            Position = new Vector2(position.X + x, position.Y + y);
        }

        public virtual Matrix GetMatrix()
        {
            Vector3 pos = new Vector3(position, 0);
            Vector3 screenPos = new Vector3(screenPosition, 0.0f);

            return Matrix.CreateTranslation(-pos) *
            Matrix.CreateScale(zoom.X, zoom.Y, 1.0f) *
            Matrix.CreateRotationZ(rotation) *
            Matrix.CreateTranslation(screenPos);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(position.X - 960, position.Y - 540);
        }
    }
}
