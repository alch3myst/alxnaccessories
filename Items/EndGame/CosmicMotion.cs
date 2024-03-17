using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Effects;
using alxnaccessories.Items.MidGame;

namespace alxnaccessories.Items.EndGame
{
	public class CosmicMotion : ModItem {
		public override void SetStaticDefaults() {}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(5, 0, 0, 0);
			Item.rare = ItemRarityID.Red;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public int Stacks;
		public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.GetModPlayer<AlxnGlobalPlayer>().GMotion) return;

            player.GetModPlayer<CosmicMotionPlayer>().amEquipped = true;	
			player.GetModPlayer<CosmicMotionPlayer>().amRef = this;

			player.GetDamage(DamageClass.Ranged) += 0.12f * Stacks;
			player.moveSpeed += 0.1f * Stacks;
			player.GetCritChance(DamageClass.Ranged) += 7f * Stacks;
			player.endurance += 0.02f * Stacks;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ArcherMotion>())
				.AddIngredient(ItemID.LunarBar, 2)
				.AddIngredient(ItemID.WingsVortex)
				.AddIngredient(ItemID.GoldBar)
                .AddTile(TileID.LunarCraftingStation)
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


		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (!amEquipped || amRef == null) return;
			if (Player.GetModPlayer<AlxnGlobalPlayer>().GMotion) { amRef.Stacks = 0; return; }

			if (!amEquipped || proj.DamageType != DamageClass.Ranged) {return;}

			if (amRef.Stacks < 10) {
				amRef.Stacks += 1;
			}

			if (amRef.Stacks >= 10) {

                target.GetGlobalNPC<CosmicBurnNPCDebuff>().burnDamage = proj.damage;
				target.GetGlobalNPC<CosmicBurnNPCDebuff>().StrongEffect = true;
				target.AddBuff(ModContent.BuffType<CosmicBurnDebuff>(), 200);
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			LoseStacks();
		}
		public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
			LoseStacks();
		}

		private void LoseStacks() {
            if(!amEquipped || amRef == null) return;
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