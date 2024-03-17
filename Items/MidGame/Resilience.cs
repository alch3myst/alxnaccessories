using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;

namespace alxnaccessories.Items.MidGame
{
	public class Resilience : ModItem {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Resilience");
			/* Tooltip.SetDefault(
				"+10 armor\n"
				+ "Bellow 50% health, take 5% less damage per 100 missing\n"
				+ "health"
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
			player.statDefense += 10;
			int halfLife = player.statLifeMax2 / 2;

			if (player.statLife < halfLife) {
				player.endurance += 0.1f * ((halfLife - player.statLife) / 100f);
                player.lifeRegen += 30 * ((halfLife - player.statLife) / 100);

				if (player.statMana > 10)
				{
					player.statMana -= 9;
					player.Heal(1);
				}
            }
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.TurtleShell)
				.AddIngredient(ItemID.SquireShield)
				.AddIngredient(ItemID.Gel, 5)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}