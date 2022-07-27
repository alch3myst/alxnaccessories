using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Utilities;

using alxnaccessories.Effects;
using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame
{
	public class FromDust : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life From Dust");
			Tooltip.SetDefault(
				"+1 Minion per 120 life\n"
				+ "10% Increased summon damage per 120 life\n"
				+ "Hits have a 8% chance of inflicting cosmic burn\n"
				+ "Hits deals additional damage, witch scales with\n"
				+ "maximum minions.\n"
			);
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
			player.GetModPlayer<AlxnGlobalPlayer>().GFromDust = true;
			if (player.GetModPlayer<AlxnGlobalPlayer>().GSons) {return;}

			player.maxMinions += 1 * (player.statLifeMax / 120);
			player.GetDamage(DamageClass.Summon) += 0.1f * (player.statLifeMax / 120);
			player.GetModPlayer<FromDustPlayer>().fdustOn = true;
			player.GetModPlayer<FromDustPlayer>().maxMinions = player.maxMinions;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.TikiMask)
				.AddIngredient(ItemID.HerculesBeetle)
				.AddIngredient(ItemID.OrangeTorch, 5)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class FromDustPlayer : ModPlayer {
		public bool fdustOn;
		public float maxMinions;

		public override void ResetEffects()
		{
			Player.GetModPlayer<AlxnGlobalPlayer>().GFromDust = false;
			fdustOn = false;
		}

		private UnifiedRandom random = new UnifiedRandom();
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (fdustOn && proj.DamageType == DamageClass.Summon) {
			if (Player.GetModPlayer<AlxnGlobalPlayer>().GSons) { return; }

				damage += (int)(damage * (maxMinions / 50));

				if (0.08f >= random.NextFloat()) {
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = damage * 2;

					target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 250);
				}
			}
		}
	}
}