using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;

namespace alxnaccessories.Items.EarlyGame
{
	public class MageAcc : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Burn");
			Tooltip.SetDefault("Magic hits deals 80% of the damage as burn.");
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<MageAccPlayer>().mageAccOn = true;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Obsidian)
				.AddIngredient(ItemID.Sapphire)
				.AddIngredient(ItemID.ManaCrystal)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}


	public class MageAccPlayer : ModPlayer {
		// Things.Loggers lg = new Things.Loggers();
		public bool mageAccOn;

		public override void ResetEffects() {
			mageAccOn = false;
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (mageAccOn && proj.DamageType == DamageClass.Magic) {
				target.GetGlobalNPC<MageAccNPCDebuff>().burnDamage = (int)(damage * 0.8f);
				target.AddBuff(ModContent.BuffType<MageAccDebuff>(), 250);
			}
		}
	}
}