using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;
using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame
{
	public class NebulaPredator : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[c/f7073f:Nebula Predator]");
			Tooltip.SetDefault(
				"30% melee damage\n"
				+ "20% melee critical strike chance\n"
				+ "20% melee attack speed\n"
				+ "Melee critical hits grants attack speed\n"
				+ "Melee damage accumulates 10 times on enemies\n"
				+ "After 10 hits, the accumulated damage explode"
				+ "and strikes the enemy\n"
				+ "applying cosmic burn by half of this damage."
			);

			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Purple;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.GetModPlayer<PredatorPlayer>().PredatorOn) { return; }

			player.GetDamage(DamageClass.Melee) += 0.3f;
			player.GetCritChance(DamageClass.Melee) += 20f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;

			player.GetModPlayer<NebulaPredatorPlayer>().PredatorOn = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Predator>())
				.AddIngredient(ItemID.LunarBar, 2)
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

	public class NebulaPredatorPlayer : ModPlayer {
		public bool PredatorOn;
		public override void ResetEffects()
		{
			PredatorOn = false;
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			Pred(target, damage, crit);
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Pred(target, damage, crit, proj);
		}

		private void Pred(NPC target, int damage, bool crit, Projectile proj = null) {
			if (!PredatorOn) return;
			if (proj != null) {
				if (proj.DamageType != DamageClass.Melee) return;
			}

			if (crit) Main.LocalPlayer.AddBuff(ModContent.BuffType<PredatorBuff>(), 500, false);
			target.GetGlobalNPC<NebulaPredatorNPC>().stack(target, damage);
		}
	}

	public class NebulaPredatorNPC : GlobalNPC {
		public override bool InstancePerEntity => true;

		private int predatorStacks = 0;
		private int totalDamage = 0;
		public void stack(NPC target, int damage) {
			predatorStacks += 1;
			totalDamage += damage;

			if (predatorStacks >= 10) {
				target.StrikeNPC(totalDamage, 0, 0);

				target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = totalDamage / 2;
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;

				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 150);

				predatorStacks = 0;
				totalDamage = 0;
			}
		}
	}
}