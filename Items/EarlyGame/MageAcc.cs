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
		}


		private Item it;
		public override void SetDefaults() {
			it = Item;
			
			Item.rare = ItemRarityID.Green;

			Item.width = 32;
			Item.height = 30;
			Item.accessory = true;

			Item.value = Item.buyPrice(0, 0, 10, 0);

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<MageAccPlayer>().mageAccOn = true;

			if (player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses) it.rare = ItemRarityID.Red; else it.rare = ItemRarityID.Green;
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
		public bool mageAccOn;
		public bool synth;

		public override void ResetEffects() {
			mageAccOn = false;
			synth = false;
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (mageAccOn && proj.DamageType == DamageClass.Magic) {

				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = Player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses;
				
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = (int)(damage * 0.8f);
				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 250);
			}
		}
	}
}