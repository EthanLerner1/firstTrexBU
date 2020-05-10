using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrexMyVersion1.View;
using TrexMyVersion1.Physics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TrexMyVersion1.Objects;

namespace TrexMyVersion1
{
    public class Circle
    {
        public float radius { set; get; }
        public Vector2 position { set; get; }

        /// <summary>
        /// constructor for a circle
        /// </summary>
        /// <param name="rad"> float, radius sizr in pixels</param>
        /// <param name="pos">Vector2, middle of the circle</param>
        public Circle(float rad, Vector2 pos)
        {
            this.radius = rad;
            this.position = pos;
        }
        public Circle() { }
    }

    public static class Global
    {
        #region data
        //static Level[] levels;
        //public static Map map;
        public static Level currentLevel;
        public static float scale;
        public static float playerScale;
        public static float zoom;
        public static float fps;
        #endregion

        public static void Init()
        {
            scale = 0.2f;
            playerScale = 0.5f;
            zoom = 1.2f;
            fps = 60;
            Texture2D tmp = Tools.cm.Load<Texture2D>("levels/0");
            currentLevel = new Level(tmp, Tools.cm.Load<Texture2D>("levels/masks/0m"), Level.generateGroundSize(),
                new Vector2(-tmp.Width*scale, 0), null, Color.White, 0, new Vector2(0, 0),
                      new Vector2(Global.scale), 0, 0);

        }


    }

}
