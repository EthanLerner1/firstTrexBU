using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrexMyVersion1.Physics;

namespace TrexMyVersion1.Objects
{
    public class MovingObj : DrawObj
    {
        #region data
        protected int engI;
        public abstractKeys keys;
        protected float speed;
        protected PhysicsManager pm;
        private Engine engCheck;
        private int pase;
        #endregion

        #region ctor
        /// <summary>
        /// constructor for moving object
        /// </summary>
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
        public MovingObj(abstractKeys keys, int engI, Engine eng, Texture2D texture, Vector2 position,
          Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth, PhysicsManager pm) : base(texture, position,
           sourceRectangle, color, rotation, origin, scale, effects, layerDepth)
        {
            speed = 0;
            this.engI = engI;
            this.engCheck = eng;
            this.keys = keys;
            this.pm = pm;
            if (eng != null)
                pm.createNewEngine(eng);
            if (keys!=null)
                keys.WhoAmI(this);
        }

        public MovingObj(int pase,Texture2D texture, Vector2 position,
          Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth, PhysicsManager pm) : base(texture, position,
           sourceRectangle, color, rotation, origin, scale, effects, layerDepth)
        {
            Game1.Event_Update += updateMO;
            speed = 0;
            this.pm = pm;
            this.pase = pase;
        }

        #endregion
        public virtual void updateMO()
        {
            if (this.engCheck==null)
            {
                pm.activateStaticEngine(DRC.left, this, pase);
            }
            

        }
    }
}
