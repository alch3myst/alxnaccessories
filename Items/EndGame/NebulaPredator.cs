using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;
using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame
{
	public class NebulaPredator : ModItem {
		public override void SetStaticDefaults() {}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(5, 0, 0, 0);
			Item.rare = ItemRarityID.Purple;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.GetModPlayer<AlxnGlobalPlayer>().GPredator) { return; }

			player.GetDamage(DamageClass.Melee) += 0.3f;
			player.GetCritChance(DamageClass.Melee) += 20f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;

			player.GetModPlayer<NebulaPredatorPlayer>().NebulaPredatorOn = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<Predator>())
				.AddIngredient(ItemID.LunarBar, 2)
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}

	public class NebulaPredatorPlayer : ModPlayer {
		public bool NebulaPredatorOn;
		public override void ResetEffects()
		{
			NebulaPredatorOn = false;
		}

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
			if (!NebulaPredatorOn) return;
            modifiers.IncomingDamageMultiplier *= 1.2f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!NebulaPredatorOn) return;
            if (hit.DamageType != DamageClass.Melee) return;
            Pred(target, (int)(damageDone * Player.GetDamage(DamageClass.Melee).Additive), hit.Crit);
        }

        private void Pred(NPC target, int damage, bool crit, Projectile proj = null) {
			if (crit) Main.LocalPlayer.AddBuff(ModContent.BuffType<PredatorBuff>(), 500, false);
			target.GetGlobalNPC<NebulaPredatorNPC>().stack(target, damage);
		}
	}

	public class NebulaPredatorNPC : GlobalNPC {
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

				target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = totalDamage / 3;
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;

				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 150);

				predatorStacks = 0;
				totalDamage = 0;
			}
		}
	}
}