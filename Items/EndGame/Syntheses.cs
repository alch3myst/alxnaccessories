using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using alxnaccessories.Effects.SysthesesEffects;

using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame {
	public class Syntheses : ModItem {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Syntheses");
			/* Tooltip.SetDefault(
				"35% Increased magic damage\n"
				+ "Cycles between four elements\n"
				+ "Slowing, Poisoning, Bleeding and Igniting the enemy\n"
				+ "Hits deals more damage based on element.\n"
				+ "Syntheses can influence other items.\n"
			); */
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;

			Item.value = Item.buyPrice(5, 0, 0, 0);
			Item.rare = ItemRarityID.Red;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<SynthesesPlayer>().SynthesesOn = true;
			player.GetDamage(DamageClass.Magic) +=  0.35f;

			player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.FragmentStardust, 5)
				.AddIngredient(ItemID.FragmentSolar, 5)
				.AddIngredient(ItemID.FragmentVortex, 5)
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddIngredient(ItemID.LunarBar)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}

	public class SynthesesPlayer : ModPlayer {
		public int stage = 0;
		public bool SynthesesOn;

		public override void ResetEffects()
		{
			SynthesesOn = false;
			Player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses = false;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!SynthesesOn) return;
            if (hit.DamageType != DamageClass.Magic) return;
            SynthesesHit(ref target, hit.Damage, hit.Crit);
        }

        private void SynthesesHit(ref NPC target, int damage, bool crit) {
			target.buffImmune[ModContent.BuffType<SynthesesCold>()] = false;
			target.AddBuff(ModContent.BuffType<SynthesesCold>(), 150);
		}
	}
}