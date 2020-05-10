using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrexMyVersion1.Physics;

namespace TrexMyVersion1.Objects
{
    public enum order
    {
        regular, reverse
    }

    public class Animation : MovingObj
    {
        #region data


        protected Texture2D[] frames;
        private int index;
        private int indexS; // for sprite
        private SpriteSheet spriteSheet;
        protected Boolean spriteActive;
        float timeBetweenFrame;
        #endregion

        /// <summary>
        /// consructor for Animation
        /// </summary>
        /// <param name="frames">Texture2D[] that contains all the animation frames</param>
        /// <param name="keys">AbstructKeys, keys that control the character</param>
        /// <param name="engI">the index of the engine </param>
        /// <param name="eng">engine for the biker</param>
        /// <param name="texture">tetxture2D picture/firstframe</param>
        /// <param name="position">Vector2, where to draw</param>
        /// <param name="sourceRectangle">which ractangl to draw</param>
        /// <param name="color">Color, which color</param>
        /// <param name="rotation">float, angle in radians</param>
        /// <param name="origin">Vector2, where to draw the Dmoot from</param>
        /// <param name="scale">Vector2, picture scale</param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        /// <param name="pm">PhysicsManager, the physics manager for the game</param>
        public Animation(Texture2D[] frames, abstractKeys keys, int engI, Engine eng, Texture2D firstFrame, Vector2 position,
          Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth, PhysicsManager pm) : base(keys, engI, eng, firstFrame, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth, pm)
        {
            this.frames = frames;
            Game1.Event_Update += update;
            this.spriteActive = false;
            this.timeBetweenFrame = 2;
            index = 1;
        }
        public Animation(Texture2D[] frames, Texture2D spriteSheet, Texture2D spriteMask, abstractKeys keys, int engI, Engine eng, Texture2D firstFrame, Vector2 position,
           Rectangle? sourceRectangle, Color color,
           float rotation, Vector2 origin, Vector2 scale,
           SpriteEffects effects, float layerDepth, PhysicsManager pm) : base(keys, engI, eng, firstFrame, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth, pm)
        {
            this.frames = frames;
            this.spriteSheet = new SpriteSheet(spriteSheet,spriteMask
                );
            Game1.Event_Update += update;
            this.spriteActive = false;
            this.timeBetweenFrame = 2;
            index = 1;
        }

        public void update()
        {
            if (this.spriteActive && this.spriteSheet!=null)
            {
                if (timeBetweenFrame == 0)
                {
                    timeBetweenFrame = 2;
                    index++;
                    switchSpriteFrame();
                    
                }
                   
                else
                    timeBetweenFrame--;
            }



        }

        protected void switchFrame(order i)
        {
            if (i == order.regular)
            {
                if (index >= frames.Length - 1 || index < 0)
                    index = 0; // restarting index
                CurrentFrame = frames[index];
                index++;
            }
            else
            {
                if (index < 0)
                    index = frames.Length - 1; // restarting index
                CurrentFrame = frames[index];
                index--;
            }

        }
        protected void switchToSprite()
        {
            this.spriteActive = true;
            index = 0;
        }
        private void switchSpriteFrame()
        {

            index %= spriteSheet.rec.Count;
            if (index != 0)
                index = index;
            Origin = spriteSheet.org[index];



            CurrentFrame = spriteSheet.tex;
            base.sourceRectangle = spriteSheet.rec[index];
            

        }


        public static Texture2D[] loadTextures(String library, int amount)
        {//gets a library name that containts all ths states of the character animation
            Texture2D[] frames = new Texture2D[amount];
            for (int i = 0; i < amount; i++)
            {
                frames[i] = Tools.cm.Load<Texture2D>(library + "/" + i);
                Tools.makeTransparent(frames[i]);
            }
            return frames;
        }

    }
}
