using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrexMyVersion1.View;
using TrexMyVersion1.Physics;


namespace TrexMyVersion1.Objects
{


    public class Biker : Animation
    {
        #region data
        public Circle bWheel; // the back wheel of the biker
        public Circle fWheel; // the front wheel of the biker
        public Circle area;
        private List<Circle> coliisionPoints; // all the circles inside the Dmoot
        private String mask;
        private Vector2 origin;
        private float mass;
        public int ID;
        public Boolean collideWithObstacle;
        private int timeBetweenFrame = 5;
        private collisionState touchesGround;
        #endregion

        #region const
        /// <summary>
        /// the constructor of Biker class
        /// </summary>
        /// <param name="mask"> the location of the bikers mask in CONTENT (the way the mask is made would be explained in appendix</param>
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
        public Biker(String mask, Texture2D[] frames, abstractKeys keys, int engI, Engine eng, Texture2D FirstFrame, Vector2 position,
            Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale,
            SpriteEffects effects, float layerDepth, PhysicsManager pm, float mass) : base(frames, keys, engI, eng, FirstFrame, position, sourceRectangle, color,
          rotation, origin, scale, effects, layerDepth, pm)
        {
            this.fWheel = new Circle();
            this.bWheel = new Circle();
            this.coliisionPoints = new List<Circle>();
            this.mask = mask;
            this.origin = origin;
            this.mass = mass;
            this.ID = engI;
            this.collideWithObstacle = false;
            this.touchesGround = collisionState.noCollision;
            this.area = Tools.findArea(mask, new Point(2, 0), this.scale);
            setBikerMask();
            Game1.Event_Update += update;
        }
        #endregion

        #region funcs
        
        private void update()
        {
            this.touchesGround =  pm.roadPlayerInteraction(this);
            if (collideWithObstacle == true)
            {
                pm.bumped(this);
                collideWithObstacle = false;
            }
                
            this.switchingFrame();
            this.updateMO();
        }

        /// <summary>
        /// a private function for a Biker objject thats sets its mask data:
        /// wheels & collision circles
        /// </summary>
        private void setBikerMask()
        {
            int cntR = 0;
            Color[,] cm = Tools.getColorMap(mask);
            Color compBody = cm[0, 0];
            Color compWheel = cm[1, 0];
            Boolean didBack = false;
            for (int i = 0; i < cm.GetLength(1); i++)
            {
                if (cm[0, i] == compBody) //// need to check
                {
                    for (int t = 1; t < cm.GetLength(0); t++)
                    {
                        if (cm[t, i] == compBody)
                        {

                            Vector2 pos = new Vector2(t, i);
                            int tmpIndext = i;
                            while (true)
                            {
                                
                                cntR = cntR + 1;
                                if (cm[t, tmpIndext] == compBody)
                                {
                                    tmpIndext = cntR;
                                    cntR = 0;
                                    break;
                                }
                                tmpIndext--;
                            }
                            this.coliisionPoints.Add(new Circle((float)tmpIndext, pos));

                        }

                    }
                }
                if (cm[0, i] == compWheel)
                {
                    for (int t = 1; t < cm.GetLength(0); t++)
                    {
                        if (cm[t, i] == compWheel)
                        {
                            if (didBack == false)
                            {
                                this.bWheel.position = new Vector2(t, i);
                                int tmpIndext = i;
                                while (true)
                                {
                                    tmpIndext--;
                                    cntR = cntR + 1;
                                    if (cm[t, tmpIndext] == compWheel)
                                    {
                                        this.bWheel.radius = cntR;
                                        didBack = true;
                                        cntR = 0;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                this.fWheel.position = new Vector2(t, i);
                                int tmpIndext = i;
                                while (true)
                                {
                                    tmpIndext--;
                                    cntR++;
                                    if (cm[t, tmpIndext] == compWheel)
                                    {
                                        this.fWheel.radius = cntR;
                                        break;
                                    }
                                }
                            }

                        }

                    }
                }

            }
        }

        /// <summary>
        /// a function which is related to the animation class which determens when to switch a frame
        /// </summary>
        private void switchingFrame()
        {
            if (timeBetweenFrame < 0)
                timeBetweenFrame = 0;
            if (speed >= 0)
            { // regular animation
                if (speed != 0 && timeBetweenFrame == 0)
                {
                    this.switchFrame(order.regular);
                    this.timeBetweenFrame = 5;
                }
            }
            else
            {// reverse animation
                if (speed != 0 && timeBetweenFrame == 0)
                {
                    this.switchFrame(order.reverse);
                    this.timeBetweenFrame = 5;
                }
            }
            timeBetweenFrame--;
        }

        public override void updateMO()
        {
            if (touchesGround == collisionState.collision)
                pm.engineupdate(keys, engI, this);

            this.speed = pm.getEngine(engI).speed; //updating the speed
            Position += pm.getEngine(engI).velocity;

            //base.updateMO();
        }
        #endregion
    }
}
