using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;

namespace alxnaccessories.Items.MidGame
{
	public class Predator : ModItem {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Predator");
			/* Tooltip.SetDefault(
				"10% melee damage\n"
				+ "Melee critical hits grants attack speed\n"
				+ "Melee damage accumulates 10 times on enemies\n"
				+ "After 10 hits, the accumulated damage explode\n"
				+ "and strikes the enemy." take 30% more damage
			); */

		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<PredatorPlayer>().PredatorOn = true;
			player.GetModPlayer<AlxnGlobalPlayer>().GPredator = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.MechanicalGlove)
				.AddIngredient(ItemID.WarriorEmblem)
				.AddIngredient(ItemID.HallowedBar)
				.AddIngredient(ItemID.HellstoneBar, 3)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class PredatorPlayer : ModPlayer {
		public bool PredatorOn;
		public override void ResetEffects()
		{
			Player.GetModPlayer<AlxnGlobalPlayer>().GPredator = false;
			PredatorOn = false;
		}

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
			if (!PredatorOn) return;
			modifiers.IncomingDamageMultiplier *= 1.1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (!PredatorOn) return;
			if (hit.DamageType != DamageClass.Melee) return;
			Pred(target, damageDone, hit.Crit);
        }

		private void Pred(NPC target, int damage, bool crit, Projectile proj = null) {
			if (crit) Main.LocalPlayer.AddBuff(ModContent.BuffType<PredatorBuff>(), 500, false);
			target.GetGlobalNPC<PredatorNPC>().stack(target, damage);
		}
	}

	public class PredatorNPC : GlobalNPC {
		public override bool InstancePerEntity => true;
		public NPC.HitInfo HitInfo;

		private int predatorStacks = 0;
		private int totalDamage = 0;
		public void stack(NPC target, int damage) {
			predatorStacks += 1;
			totalDamage += damage;

			if (predatorStacks >= 10) {
				HitInfo.Damage = totalDamage;
				target.StrikeNPC(HitInfo, true, true);

				predatorStacks = 0;
				totalDamage = 0;
			}
		}
	}
}