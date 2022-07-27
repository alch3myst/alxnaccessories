using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	public class Resilience : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Resilience");
			Tooltip.SetDefault(
				"+10 armour\n"
				+ "Bellow 50% health, take 5% less damage per 50 missing\n"
				+ "health"
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
			player.statDefense += 10;
			
			int halfLife = player.statLifeMax / 2;
			if (player.statLife < halfLife) {
				player.endurance += 0.05f * ((halfLife - player.statLife) / 50);
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