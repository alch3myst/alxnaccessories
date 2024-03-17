using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

using alxnaccessories.CustomParticles;
using Terraria.WorldBuilding;


namespace alxnaccessories.Items.EarlyGame {
	public class WarriorNecklaces : ModItem {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Warrior Necklace");
			/* Tooltip.SetDefault("+10% Melee Damage"
			+"\nTrue melee hits also strike nearby enemies"
			+"\ndealing 50% of the original damage"
			); */
		}

		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 30;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 0, 30, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WarriorNecklacesPlayer>().WarriorNecklacesEquiped = true;
			player.GetDamage(DamageClass.Melee) += 0.1f;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.TungstenBar)
				.AddIngredient(ItemID.Chain)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

	public class WarriorNecklacesPlayer : ModPlayer {
		public bool WarriorNecklacesEquiped;
		public NPC.HitInfo WarrHitInfo;

        public override void ResetEffects()
		{
			WarriorNecklacesEquiped = false;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (WarriorNecklacesEquiped == true)
            {
                WarrHitInfo.Damage = damageDone / 2;
                WarrHitInfo.Crit = false;

                target.StrikeNPC(WarrHitInfo, true, false);
                Dust.NewDustDirect(target.Center, 18, 18, ModContent.DustType<WarriorNecklacesDust>());
                Dust.NewDustPerfect(target.Center, DustID.Blood);
            }
            base.OnHitNPC(target, hit, damageDone);
        }

        //async private void DamageNpc(NPC target, NPC.HitModifiers modifiers) {
        //          int[Main.npc.GetLength()] npcs = [];
        //          await foreach(int indexer in npcs)
        //          {
        //              Mod.Logger.Info(npc.FullName);
        //              float dist = Vector2.Distance(npc.Center, target.Center);
        //              if (dist / 16f <= 12 && !npc.friendly && npc.life >= 0 && (npc.HasValidTarget || npc.HasNPCTarget))
        //              {

        //                  WarrHitInfo.Damage = (int)(modifiers.FinalDamage.Flat / 2);
        //                  WarrHitInfo.Crit = false;

        //                  npc.StrikeNPC(WarrHitInfo, true, false);
        //                  Dust.NewDustDirect(npc.Center, 18, 18, ModContent.DustType<WarriorNecklacesDust>());
        //                  Dust.NewDustPerfect(npc.Center, DustID.Blood);

        //                  break;
        //              }
        //          }
        //      }
    }
}