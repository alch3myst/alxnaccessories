using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace alxnaccessories.Items.MidGame
{
	public class ManaRage : ModItem {
		public override void SetStaticDefaults()
		{}

        public static readonly int additiveMana = 30;

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

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(additiveMana);

        public override void UpdateAccessory(Player player, bool hideVisual) {
			player.statManaMax2 += additiveMana;
			if (player.GetModPlayer<ManaRagePlayer>().EffectOn) {
				player.manaRegen += 50;
				player.lifeRegen += 10;
                player.GetDamage(DamageClass.Magic) += player.statManaMax2 / 260f;
			}

			if (1 - (player.statMana / player.statManaMax2) <= 0.05f || player.statMana <= 10) {
				player.GetModPlayer<ManaRagePlayer>().EffectOn = false;
				player.GetModPlayer<ManaRagePlayer>().hits = 0;
			}

			player.GetModPlayer<ManaRagePlayer>().manaRageOn = true;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.GoldBar)
				.AddIngredient(ItemID.StarCloak)
				.AddIngredient(ItemID.Ruby, 3)
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

		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
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
						player.statMana += 7;
					}
				}
			}
		}
	}
}