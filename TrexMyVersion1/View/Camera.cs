using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrexMyVersion1.Objects;

namespace TrexMyVersion1.View
{
    class Camera
    {
        public Matrix Mat { get; private set; }
        IFocous focus;
        Viewport vp;
        Vector2 pos;
        float zoom;
        public Camera(IFocous focus, Viewport vp, Vector2 pos, float zoom)
        {
            this.focus = focus;
            this.vp = vp;
            this.pos = pos;
            this.zoom = zoom;
            Game1.Event_Update += update;
        }
        void update()
        {
            Mat = Matrix.Identity*Matrix.CreateTranslation(-pos.X, -pos.Y, 0) *
                Matrix.CreateScale(zoom) * Matrix.CreateRotationZ(0) *
                Matrix.CreateTranslation(vp.X + vp.Width / 2, vp.Y + vp.Height / 2, 0);
            pos = Vector2.Lerp(pos, focus.Position, 0.09f);

        }
    }
}

