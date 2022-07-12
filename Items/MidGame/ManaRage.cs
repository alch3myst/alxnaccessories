using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	public class ManaRage : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[c/f47113:Under Pressure]");
			Tooltip.SetDefault(
				"10% Increased Magic Damage\n"
				+ "after 5 hit's gain Mana Rage\n"
				+ "witch gives you Mana regeneration\n"
				+ "and damage increases based on maximum mana\n"
				+ "Resets if mana is higher then 95%\n"
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
			player.GetDamage(DamageClass.Magic) += 0.1f;

			if (player.GetModPlayer<ManaRagePlayer>().EffectOn) {
				player.manaRegen += 20;
				player.GetDamage(DamageClass.Magic) += (player.statManaMax * 0.1f) / 100;
			}

			if (1 - (player.statMana / player.statManaMax) <= 0.05f) {
				player.GetModPlayer<ManaRagePlayer>().EffectOn = false;
				player.GetModPlayer<ManaRagePlayer>().hits = 0;
			}

			player.GetModPlayer<ManaRagePlayer>().manaRageOn = true;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ManaCrystal)
				.AddIngredient(ItemID.StarCloak)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class ManaRagePlayer : ModPlayer {
		public bool manaRageOn;
		public override void ResetEffects()
		{
			manaRageOn = false;
		}

		public bool EffectOn = false;

		public int hits;

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			ManaRageHit(proj, Main.LocalPlayer);
		}

		private void ManaRageHit(Projectile proj, Player player) {
			if (manaRageOn && proj.DamageType == DamageClass.Magic) {
				if (hits <= 6) hits +=1;
				if (hits >= 5) {
					EffectOn = true;
				}
			}
		}
	}
}