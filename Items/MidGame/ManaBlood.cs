using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	public class ManaBlood : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Blood");
			Tooltip.SetDefault("Take half of the damage from mana instead of life.\n"
			+"If you ran out of mana, you take 30% increased damage.");
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
			player.GetModPlayer<ManabloodDamagePlayer>().ManaBloodEquiped = true;
			// player.GetModPlayer<ManabloodDamagePlayer>().player = player;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ArcaneFlower)
				.AddIngredient(ItemID.ManaRegenerationBand)
				.AddIngredient(ItemID.LifeforcePotion)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}


	public class ManabloodDamagePlayer : ModPlayer {
		public bool ManaBloodEquiped;

		// Commented cause i don't know if multiplayer will work as intended
		// public Player player;

		public override void ResetEffects() {
			ManaBloodEquiped = false;
		}


		// TODO: Refactor to function
		public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
		{
			if (ManaBloodEquiped) {
				int halfDamage = damage / 2;
				int manaDamage = Utils.Clamp<int>( (int)( (damage - Player.statDefense) * (1f-Player.endurance)) , 1, damage) / 2;

				if (Player.statMana - manaDamage <= 0) {
					damage = (int)( (damage - Player.statMana) * 1.3f) ;
					Player.statMana = 0;
				} else {
					damage = halfDamage;
					Player.statMana -= manaDamage;
				}
			}
		}

		public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
		{
			if (ManaBloodEquiped) {
				int halfDamage = damage / 2;
				int manaDamage = Utils.Clamp<int>( (int)( (damage - Player.statDefense) * (1f-Player.endurance)) , 1, damage) / 2;

				if (Player.statMana - manaDamage <= 0) {
					damage = (int)( (damage - Player.statMana) * 1.3f) ;
					Player.statMana = 0;
				} else {
					damage = halfDamage;
					Player.statMana -= manaDamage;
				}
			}
		}
	}
	
}