using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;
using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame
{
	public class CosmicMotion : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("[c/f7073f:Cosmic Motion]");
			Tooltip.SetDefault("Gain cosmic stack on hit\n"
			+ "Loses 5 stacks if you take damage \n\n"
			+ "Each stack gives\n"
			+ "12% Increased Damage\n"
			+ "10% Increased Movement Speed\n"
			+ "7% Critical strike chance\n"
			+ "2% Damage reduction\n\n"
			+ "Inflict cosmic burn with 10 stacks"
			);

			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.rare = ItemRarityID.Purple;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}

		public int Stacks;
		public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.GetModPlayer<ArcherMotionPlayer>().amEquipped) {return;}

			player.GetModPlayer<CosmicMotionPlayer>().amEquipped = true;	
			player.GetModPlayer<CosmicMotionPlayer>().amRef = this;

			player.GetDamage(DamageClass.Ranged) += 0.12f * Stacks;
			player.moveSpeed += 0.1f * Stacks;
			player.endurance += 0.02f * Stacks;
			player.GetCritChance(DamageClass.Ranged) += 7f * Stacks;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ArcherMotion>())
				.AddIngredient(ItemID.LunarBar, 2)
				.AddIngredient(ItemID.WingsVortex)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

	public class CosmicMotionPlayer : ModPlayer {
		public bool amEquipped;
		public CosmicMotion amRef;

		public override void ResetEffects()
		{
			amEquipped = false;
		}


		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (amRef == null) { return; }
			if (!amEquipped || proj.DamageType != DamageClass.Ranged) {return;}

			if (amRef.Stacks < 10) {
				amRef.Stacks += 1;
			}

			if (amRef.Stacks >= 10) {
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = (int)(damage * 0.8f);
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;
				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 200);
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			LoseStacks();
		}
		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
		{
			LoseStacks();
		}

		private void LoseStacks() {
			if (amRef == null) { return; }
			if (!amEquipped) {return;}
			if (amRef.Stacks - 5 <= 0) { amRef.Stacks = 0; return; }
			amRef.Stacks -= 5;
		}

		public int GetMotionStacks() {
			if (amRef != null)
			{
				return amRef.Stacks;
			}

			return 0;
		}

		public bool GetEquippedItem()
		{
			if (amRef == null) return false;
			return Main.LocalPlayer.GetModPlayer<CosmicMotionPlayer>().amEquipped;
		}
	}
}