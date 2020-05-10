using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrexMyVersion1.Objects
{
    public interface IFocous
    {
        Vector2 Position { get; set; }
        Vector2 Origin { get; set; }
        float Rotation { get; }
        Texture2D CurrentFrame { get; }
    }
    public class DrawObj : IFocous
    {
        // this class draws everything you see on the screen 
        // all of the object kinds inherit this class
        #region data
        // IFocous
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Texture2D CurrentFrame { get; set; }
        public Vector2 Origin { get; set; }

        //other data
        public string message;
        private int amount;
        protected Rectangle? sourceRectangle;
        private Color color;

        public Vector2 scale { get; private set; }
        private SpriteEffects effects;
        private float layerDepth;
        #endregion

        /// <summary>
        /// creating a basic object that is just drWN ON THE SCREEN
        /// </summary>
        /// <param name="texture">tetxture2D picture/firstframe </param>
        /// <param name="position">Vector2, where to draw</param>
        /// <param name="sourceRectangle">which ractangl to draw</param>
        /// <param name="color">Color, which color</param>
        /// <param name="rotation">float, angle in radians</param>
        /// <param name="origin">Vector2, where to draw the Dmoot from</param>
        /// <param name="scale">Vector2, picture scale</param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        public DrawObj(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth)
        {
            this.CurrentFrame = texture;
            this.Position = position;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.Rotation = rotation;
            this.Origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            Game1.Event_Draw += draw;

        }

        public DrawObj(Texture2D texture, int amount, Vector2 position, Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth)
        {
            this.CurrentFrame = texture;
            this.Position = position;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.Rotation = rotation;
            this.Origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.amount = amount;
            Game1.Event_Draw += draw;

        }

        public DrawObj() { }
        public virtual void draw()
        {
            if (this.amount > 0)
                for (int i = 0; i < amount; i++)
                {
                    float posX = Position.X + CurrentFrame.Width * Global.scale * i;
                    Tools.sb.Draw(CurrentFrame, new Vector2(posX, Position.Y), sourceRectangle,
                          color, Rotation, Origin, scale,
                          effects, layerDepth);
                }
            else
                Tools.sb.Draw(CurrentFrame, Position, sourceRectangle,
                              color, Rotation, Origin, scale,
                              effects, layerDepth);


        }
    }
}