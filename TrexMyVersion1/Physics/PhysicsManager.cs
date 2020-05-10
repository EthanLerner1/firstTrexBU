using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrexMyVersion1.Objects;

namespace TrexMyVersion1.Physics
{
    public enum DRC
    {
        left, right
    } 
    public class PhysicsManager
    {
        #region data
        private List<Engine> eng = new List<Engine>();
        private float g = 9.8f;
       // public Collision collision;
       // private Gravity gravity;
        //private Vector2 accl = Vector2.Zero;
        private int delay = 0;
        private int collideState;

        #endregion

        #region ctor
        public PhysicsManager()
        {
            //this.collision = new Collision();
           // this.gravity = new Gravity();
        }
        #endregion

        #region funcs
        #region ////////////// ENGINE /////////////////
        #region ///// physics engine
        public void createNewEngine(Engine eng1)
        {//creating a new engine in the list with the same values as the given engine
            this.eng.Add(new Engine(eng1));
        }
        public void engineupdate(abstractKeys keys, int index, IFocous f)
        {//activates the update function of an engine by his index
            eng[index].engine_update(keys);
        }
        public Engine getEngine(int index)
        {//gets the index of the wanted engine and returns a copy of this engine
            return new Engine(eng[index]);
        }

        public static Engine createAndGetNewEngine(float maxpower, float maxsteer, float maxspeed, float maxbreak)
        {
            return new Engine(maxpower, maxsteer, maxspeed, maxbreak);
        }
        #endregion
        #region //// static engine
        public void activateStaticEngine (DRC drc , IFocous character, int pase)
        {
            switch (drc)
            {
                case DRC.right:
                    character.Position = new Vector2(character.Position.X + (pase / 50), character.Position.Y);
                    break;
                case DRC.left:
                    character.Position = new Vector2(character.Position.X - (pase / 50), character.Position.Y);
                    break;
            }
        }
        #endregion
        #endregion

        #region ////////////// ROAD MANAGMENT /////////////////

        /// <summary>
        /// activates the gravity effect between the road and the player
        /// activates gravity when he is on the air
        /// </summary>
        /// <param name="player">Biker, the player that have interaction with the road</param>
        public collisionState roadPlayerInteraction(Biker player)
        {
            if (Collision.roadBikerState(Global.currentLevel, player) == collisionState.collision)
            {//stoping hes movment on the Y axis
                this.eng[player.ID].velocity = new Vector2(this.eng[player.ID].speed, 0);
                if (player.keys.Space())
                    jump(player);
                // allowing the biker to paddle
                return collisionState.collision;
            }
            else // activate gravity
                this.eng[player.ID].velocity = new Vector2(this.eng[player.ID].speed, this.eng[player.ID].velocity.Y + g / Global.fps);
            return collisionState.noCollision;
        }

        public void jump (Biker player)
        {
            float newSpeed = this.eng[player.ID].velocity.Y + (g / Global.fps * -40);
            this.eng[player.ID].velocity = new Vector2(this.eng[player.ID].speed, newSpeed);
        }

        #endregion

        #region ///////////////// physics effects /////////////////
        public void bumped (Biker p)
        {
            jump(p);
            Vector2 tmp = new Vector2(-this.eng[p.ID].velocity.X, this.eng[p.ID].velocity.Y);
            this.eng[p.ID].speed = this.eng[p.ID].speed*-0.5f;
        }
        #endregion


        #endregion
    }
}
