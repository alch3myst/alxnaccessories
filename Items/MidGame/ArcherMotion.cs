using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

using alxnaccessories.Items.EarlyGame;


namespace alxnaccessories.Items.MidGame {

	public class ArcherMotion : ModItem {

		public int Stacks;

		public override void SetStaticDefaults() {}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<ArcherMotionPlayer>().amEquipped = true;
			player.GetModPlayer<ArcherMotionPlayer>().amRef = this;

            player.GetModPlayer<AlxnGlobalPlayer>().GMotion = true;

            player.GetDamage(DamageClass.Ranged) += 0.09f * Stacks;
			player.moveSpeed += 0.05f * Stacks;
			player.GetCritChance(DamageClass.Ranged) += 5f * Stacks;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ArcherAcc>())
				.AddIngredient(ItemID.FishronWings)
				.AddIngredient(ItemID.HallowedBar)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}

	public class ArcherMotionPlayer : ModPlayer {
		public bool amEquipped;
		public ArcherMotion amRef;

		public override void ResetEffects()
		{
			Player.GetModPlayer<AlxnGlobalPlayer>().GMotion = false;
			amEquipped = false;
		}


		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
		{
			if (amRef == null) { return; }
			if (!amEquipped || proj.DamageType != DamageClass.Ranged) {return;}
			if (amRef.Stacks < 10) {
				amRef.Stacks += 1;
			}
		}

		public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			if (!amEquipped || amRef == null) return;
			amRef.Stacks = 0;
		}
		public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
            if (!amEquipped || amRef == null) return;
            amRef.Stacks = 0;
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
			return Main.LocalPlayer.GetModPlayer<ArcherMotionPlayer>().amEquipped;
		}
	}
}