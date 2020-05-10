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
    public class Obstacle : Animation
    {
        #region data
        private Boolean switchFrame;
        public List<Vector2> frame; // vectors around the character, the first one is the offset
        public Circle region;
        #endregion

        /// <summary>
        /// the constructor of an obstacle
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
        public Obstacle(Texture2D[] frames, String mask,Texture2D spriteSheet, Texture2D spriteMask, abstractKeys keys, int engI, Engine eng, Texture2D firstFrame, Vector2 position,
          Rectangle? sourceRectangle, Color color,
          float rotation, Vector2 origin, Vector2 scale,
          SpriteEffects effects, float layerDepth, PhysicsManager pm) : base(frames,spriteSheet,spriteMask, keys, engI, eng, firstFrame, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth, pm)
        {
            this.frame = Collision.strechVectors(mask);
            this.region = Tools.findArea(mask, new Point(1,0), this.scale);
            Game1.Event_Update += update;

        }

        #region funcs

        private void update()
        {
            if (this.switchFrame == false)
            {
                bool tmp1 = Collision.checkArea(this.region, this, this.scale.X, Tools.game.player1, Tools.game.player1.area);
                if (tmp1 == true)
                {
                    if (Collision.BikerObstacle(this, Tools.game.player1) == collisionState.collision)
                    {
                        this.switchFrame = true;
                        Tools.game.player1.collideWithObstacle = true;
                        base.switchToSprite();
                    }

                }
            }

        }
        /// <summary>
        /// this function creates all the obsticale in a stage ang retrives a list that containes all of the obstacles
        /// </summary>
        /// <param name="frames">frames of the obstacle animation</param>
        /// <param name="origin">origin of the obstacle</param>
        /// <param name="pm">physicsManager og the game</param>
        public static List<Obstacle> randomObstacle(Texture2D[] frames, Vector2 origin, PhysicsManager pm, Vector2 scale, String mask)
        {

            float levelLength = Global.currentLevel.CurrentFrame.Width * Global.scale * Global.currentLevel.amount; // the ground length
            levelLength -= 1000; // giving the player 100 pixels before he needs to act
            int obstacleAmount = Tools.rnd.Next((int)levelLength / 2000, (int)levelLength / 1500);
            List<Obstacle> obstaclesList = new List<Obstacle>();

            for (int i = 0; i < obstacleAmount; i++)
            {
                int togather = Tools.rnd.Next(1, 3); // amount of obsticale that can be next to each other
                Vector2 pos = new Vector2(1000 + Tools.rnd.Next(1500, 2000) * i, Global.currentLevel.groundPos.Y * Global.scale);
                for (int t = 0; t < togather; t++)
                {
                    Vector2 tmp = new Vector2(frames[0].Width * scale.X * t, 0);
                    obstaclesList.Add(new Obstacle(frames, mask, Tools.cm.Load<Texture2D>("obstacle/explosion/explsionSprite")
                        , Tools.cm.Load<Texture2D>("obstacle/explosion/explsionSpriteMask"),  null, -999, null, frames[0], pos + tmp, null, Color.White, 0, origin, scale, SpriteEffects.None, 0, pm));
                }

            }
            return obstaclesList;

        }

        #endregion
    }
}
