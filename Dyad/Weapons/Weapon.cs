using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;

namespace Dyad
{
    class Weapon : Moveable
    {
   
        protected float speed;        

        public Weapon()
            : base(1f , true, false)
        {                        
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void UpdateCamera(Vector2 camera)
        {
            base.UpdateCamera(camera);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public override void Restart()
        {            
        }
    }
}
