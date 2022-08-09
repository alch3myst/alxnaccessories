using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using alxnaccessories.Effects;

namespace alxnaccessories.Items.MidGame
{
	public class JungleJuggernaut : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Juggernaut");
			Tooltip.SetDefault("Always deals 1 damage\n"
			+ "+20 Armour\n"
			+ "10% Damage Reduction\n"
			+ "All hits inflict Juggernaut Debbuf\n"
			+ "Juggernaut deal damage equals to your armor\n"
			+ "and scales with max life\n"
			+ "Crits deal a double strike\n"
			);
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
			player.GetModPlayer<JungleJuggernautPlayer>().JungleJuggernautEquiped = true;
			player.statDefense += 20;
			player.endurance += 0.1f;
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.PaladinsShield)
				.AddIngredient(ItemID.Vine, 4)
				.AddIngredient(ItemID.SporeSac)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}


	public class JungleJuggernautPlayer : ModPlayer {
		public bool JungleJuggernautEquiped;

		public override void ResetEffects() {
			JungleJuggernautEquiped = false;
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (JungleJuggernautEquiped) {
				ApplyJugg(target, ref damage);
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (JungleJuggernautEquiped) {
				ApplyJugg(target, ref damage);
			}
		}

		private void ApplyJugg(NPC target, ref int damage) {
			damage = 1;
			float lifeScaling = 1 + Utils.Clamp<float>(System.MathF.Log2(Player.statLifeMax2 / 100), 0f, 10f) * 0.5f;
			target.GetGlobalNPC<JuggernautNPCDebuff>().critChance = Player.GetTotalCritChance<MeleeDamageClass>();
			target.GetGlobalNPC<JuggernautNPCDebuff>().jdamage = (int)( (Player.statDefense * lifeScaling) + Player.statLife * 0.1f);
			target.buffImmune[ModContent.BuffType<JuggernautDebuff>()] = false;	
			target.AddBuff(ModContent.BuffType<JuggernautDebuff>(), 250, true);
		}
	}

}