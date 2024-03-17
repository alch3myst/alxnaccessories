using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using alxnaccessories.Effects;

namespace alxnaccessories.Items.MidGame
{
	public class JungleJuggernaut : ModItem {
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
			player.GetModPlayer<JungleJuggernautPlayer>().JungleJuggernautEquiped = true;
			player.statDefense += 20;
			player.endurance += 0.1f;
			player.GetDamage(DamageClass.Generic) *= 0.08f;

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

		public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (JungleJuggernautEquiped)
			{
				if (modifiers.DamageType == DamageClass.Melee || modifiers.DamageType == DamageClass.Ranged) {
					ApplyJugg(target, ref item.damage);
				}
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (JungleJuggernautEquiped) {
				if (modifiers.DamageType == DamageClass.Melee || modifiers.DamageType == DamageClass.Ranged)
				{
					ApplyJugg(target, ref proj.damage);
				}
			}
		}

		private void ApplyJugg(NPC target, ref int damage) {
			float lifeScaling = System.MathF.Log(Player.statLifeMax2) / 4;

			// Set the debuff crit chance to the held weapon drit
			target.GetGlobalNPC<JuggernautNPCDebuff>().critChance = Player.GetTotalCritChance<MeleeDamageClass>();
			
			// Set the debuff damage
			target.GetGlobalNPC<JuggernautNPCDebuff>().jdamage =
				(int)( Player.statDefense * (1 + lifeScaling) + Player.statLife * 0.12f);

			// Remove immunity to work on bosses
			target.buffImmune[ModContent.BuffType<JuggernautDebuff>()] = false;	

			// Add the buff to the enemy
			target.AddBuff(ModContent.BuffType<JuggernautDebuff>(), 250, true);
		}
	}

}