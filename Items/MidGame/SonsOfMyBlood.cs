using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Utilities;

using alxnaccessories.Effects;

namespace alxnaccessories.Items.MidGame
{
	public class SonsOfMyBlood : ModItem {
		public override void SetStaticDefaults()
		{}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
            // If from dust is equuiped, this item should not work
            if (player.GetModPlayer<AlxnGlobalPlayer>().GFromDust) { return; }

			player.maxMinions += 1 * player.statLifeMax / 150;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.GetModPlayer<SonsPlayer>().sonsOn = true;

			player.GetModPlayer<AlxnGlobalPlayer>().GSons = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Silk)
				.AddIngredient(ItemID.HerculesBeetle)
				.AddIngredient(ItemID.OrangeTorch, 6)
				.AddIngredient(ItemID.GoldBar)
                .AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class SonsPlayer : ModPlayer {
		public bool sonsOn;

		public override void ResetEffects()
		{
			sonsOn = false;
			Player.GetModPlayer<AlxnGlobalPlayer>().GSons = false;
		}

		private UnifiedRandom random = new UnifiedRandom();
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
		{
			if (sonsOn && proj.DamageType == DamageClass.Summon) {
				// If from dust is equuiped, this item should not work
				if (Player.GetModPlayer<AlxnGlobalPlayer>().GFromDust) { return; }

				if (0.03f >= random.NextFloat()) {
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = false;
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = proj.damage * 2;

					target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 250);
				}
			}
		}
	}
}