using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	public class LastBreath : ModItem {
		public override void SetStaticDefaults()
		{}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Orange;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			if (player.statLife < (player.statLifeMax / 2) ) {
				if (player.statLife == 1) {
					player.GetDamage(DamageClass.Generic) += 5f;
					player.GetDamage(DamageClass.Generic) *= 1.2f;
					return;
				} else {
                    player.GetDamage(DamageClass.Generic) += 0.2f;
                }
			}
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Skull)
				.AddIngredient(ItemID.IronShortsword)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}