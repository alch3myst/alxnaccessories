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
		{
			DisplayName.SetDefault("[c/f47113:Sons of my blood]");
			Tooltip.SetDefault(
				"+1 max minion per 150 life\n"
				+ "10% Increased summon damage\n"
				+ "minions have a 4% chance to inflict cosmic burn by 2x of damage\n"
			);

			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.LightRed;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.maxMinions += 1 * player.statLifeMax / 150;
			player.GetDamage(DamageClass.Summon) += 0.1f;
			player.GetModPlayer<SonsPlayer>().sonsOn = true;
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

	public class SonsPlayer : ModPlayer {
		public bool sonsOn;

		public override void ResetEffects()
		{
			sonsOn = false;
		}

		private AlxUtils.Loggers lg = new AlxUtils.Loggers();
		private UnifiedRandom random = new UnifiedRandom();
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (sonsOn && proj.DamageType == DamageClass.Summon) {
				if (0.04f >= random.NextFloat()) {
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;
					target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = damage * 2;

					target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 250);
				}
			}
		}
	}
}