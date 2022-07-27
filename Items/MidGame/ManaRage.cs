using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	public class ManaRage : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Under Pressure");
			Tooltip.SetDefault(
				"10% Increased Magic Damage\n"
				+ "after 5 hit's gain Mana Rage\n"
				+ "witch gives you Mana regeneration\n"
				+ "and damage increases based on maximum mana\n"
				+ "Resets if mana is higher then 95% or lower then 10\n"
			);

		}

		private Item it;
		public override void SetDefaults() {
			it = Item;
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetDamage(DamageClass.Magic) += 0.1f;

			if (player.GetModPlayer<ManaRagePlayer>().EffectOn) {
				player.manaRegen += 20;
				player.GetDamage(DamageClass.Magic) += (player.statManaMax * 0.1f) / 100;
			}

			if (1 - (player.statMana / player.statManaMax) <= 0.05f || player.statMana <= 10) {
				player.GetModPlayer<ManaRagePlayer>().EffectOn = false;
				player.GetModPlayer<ManaRagePlayer>().hits = 0;
			}

			player.GetModPlayer<ManaRagePlayer>().manaRageOn = true;

			if (player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses) it.rare = ItemRarityID.Red; else it.rare = ItemRarityID.Orange;
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


		private int tick = 0;
		private void ManaRageHit(Projectile proj, Player player) {
			if (manaRageOn && proj.DamageType == DamageClass.Magic) {
				if (hits <= 6) hits +=1;
				if (hits >= 5) {
					EffectOn = true;

					if (!player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses) return;
					tick++;
					if (tick % 10 == 0) {
						tick = 0;
						player.statMana += 5;
					}
				}
			}
		}
	}
}