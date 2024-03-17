using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;

namespace alxnaccessories.Items.EarlyGame
{
	public class MageAcc : ModItem {
		public override void SetStaticDefaults() {}

		public override void SetDefaults() {
			
			Item.rare = ItemRarityID.Green;

			Item.width = 32;
			Item.height = 30;
			Item.accessory = true;

			Item.value = Item.buyPrice(0, 0, 10, 0);

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
		public bool mageAccOn;
		public bool synth;

		public override void ResetEffects() {
			mageAccOn = false;
			synth = false;
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
		{
			if (mageAccOn && proj.DamageType == DamageClass.Magic) {

				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = Player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses;
				
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = (int)(proj.damage * 0.5f);
				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 250);
			}
		}
	}
}