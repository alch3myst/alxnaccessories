using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace alxnaccessories.CustomParticles {
    public class WarriorNecklacesDust : ModDust {
		private int lifeTime;

        public override void OnSpawn(Dust WnDust)
        {
            WnDust.frame = new Rectangle(0, 0, 18, 18);

            lifeTime = 50;
            WnDust.noGravity = true;
            WnDust.noLight = true;
            WnDust.alpha = 1;

            while (lifeTime % 5 != 0)
            {
                lifeTime++;
            }
        }

        public override bool Update(Dust WnDust)
        {

            lifeTime--;

            //Every 3rd tick change the frame of the dust
            if (lifeTime % 10 == 0)
            {
                WnDust.frame = new Rectangle(0, 0, 18 * Main.rand.Next(3), 18);
            }

            if (lifeTime % 2 == 0) {
                WnDust.scale -= 0.05f;
                WnDust.rotation += 0.01f;
            }

            if (WnDust.scale <= 0.1f) {
                WnDust.active = false;
            }


            return true;
        }
    }
}