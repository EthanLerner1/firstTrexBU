using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrexMyVersion1.Objects
{
    class SpriteRow
    {
        public List<Rectangle> rec { get;  set; }
        public List<Vector2> org { get;  set; }

        public SpriteRow() {
            this.rec = new List<Rectangle>();
            this.org = new List<Vector2>();
        }
    }
    class SpriteSheet
    {
        #region data
        public List<Rectangle> rec { get; private set; }
        public List<Vector2> org { get; private set; }
        public Texture2D tex;
        private Texture2D spriteSheet;
        #endregion

        #region ctor
        public SpriteSheet (Texture2D txt, Texture2D sheet)
        {
            this.rec = new List<Rectangle>();
            this.org = new List<Vector2>();
            this.spriteSheet = sheet;
            this.tex = txt;
            loadSheet();
        }
        #endregion

        #region funcs
        /// <summary>
        /// loading the data of a sprite sheet that has a gridline like specified in index
        /// </summary>
        private void loadSheet ()
        {
            List<SpriteRow> r = new List<SpriteRow>();
            SpriteRow CR = new SpriteRow();
            Color[,] color = new Color[spriteSheet.Width,spriteSheet.Height];
            color = Tools.getColorMap(spriteSheet);
            List<Point> pnt = new List<Point>();
            Color refC = color[0, 0];

            for (int w = 0; w < color.GetLength(0); w++)
            {
                for (int h = 1; h < color.GetLength(1); h++)
                {
                    if (color[w, h] == refC)
                        pnt.Add(new Point(w, h));
                }
            }

            int firstHight = pnt[0].Y;

            for (int i = 1; i < pnt.Count; i += 2)
            {
                if (firstHight*(r.Count+1) != pnt[i].Y)
                {
                    r.Add(CR);
                    CR = new SpriteRow();
                }
                    
                CR.org.Add(new Vector2(pnt[i].X - pnt[i - 1].X, pnt[i].Y));
                CR.rec.Add(new Rectangle(pnt[i - 1].X, firstHight* r.Count(), pnt[i + 1].X - pnt[i - 1].X, firstHight));

            }

            for (int i = 0; i < r.Count(); i++)
            {
                CR = r[i];
                for (int t = 0; t < CR.rec.Count(); t++)
                {
                    this.org.Add(CR.org[t]);
                    this.rec.Add(CR.rec[t]);
                }
            }
        }
        #endregion
    }
}
