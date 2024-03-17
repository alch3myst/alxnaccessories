using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace alxnaccessories.Items.MidGame
{
	public class ManaBlood : ModItem {
		public override void SetStaticDefaults()
		{}

		public static readonly int additiveMana = 30;

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(additiveMana);

        public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<ManabloodDamagePlayer>().ManaBloodEquiped = true;

			player.statManaMax2 += additiveMana;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ArcaneFlower)
				.AddIngredient(ItemID.ManaRegenerationBand)
				.AddIngredient(ItemID.LifeforcePotion)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	// More mana == more absorption
	public class ManabloodDamagePlayer : ModPlayer {
		public bool ManaBloodEquiped;

		// Commented cause i don't know if multiplayer will work as intended
		// public Player player;

		public override void ResetEffects() {
			ManaBloodEquiped = false;
		}

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			if (!ManaBloodEquiped) return;
            ManaBloodCalc(ref modifiers, npc, null);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
            if (!ManaBloodEquiped) return;
            ManaBloodCalc(ref modifiers, null, proj);
        }

		private void ManaBloodCalc(ref Player.HurtModifiers modifiers, NPC npc = null, Projectile proj = null) {
			int manaDamage = 0;

            if (proj != null)
			{
				manaDamage = Math.Clamp((int)(proj.damage * 0.5) - Player.statDefense, (int)(proj.damage * 0.2f), proj.damage);
			} else
			{
                manaDamage = Math.Clamp((int)(npc.damage * 0.5) - Player.statDefense, (int)(npc.damage * 0.2f), npc.damage);
            }

            if (Player.statMana - manaDamage <= 0)
            {
                modifiers.IncomingDamageMultiplier *= 1.2f;
                Player.statMana = 0;
            }
            else
            {
                modifiers.FinalDamage *= 0.5f;
                Player.statMana -= manaDamage;
            }
        }
	}
	
}