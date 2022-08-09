using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.EarlyGame {
	public class ArcherAcc : ModItem {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("One With Nature");
			Tooltip.SetDefault("Cycles between wind and fang\n"
			+ "Wind: 30% increased attack and 20% movement speed\n"
			+ "Fang: 15% increased ranged damage\n");
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 44;
			Item.accessory = true;

			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		private static long cycleTime = 5000;
		private long effectCycle = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + cycleTime;
		private long currentTime;
		private bool Mode = false;
		public override void UpdateAccessory(Player player, bool hideVisual) {

			currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
			if (effectCycle < currentTime) {
				Mode = !Mode;
				effectCycle = currentTime + cycleTime;
			}

			if (Mode) {
				// Wind mode
				player.moveSpeed += 0.2f;
				player.GetAttackSpeed(DamageClass.Ranged) += 0.3f;
			}
			else {
				// Fang mode
				player.GetDamage(DamageClass.Ranged).Base += 10f;
				player.GetDamage(DamageClass.Ranged) += 0.15f;
			}
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.BlueJay)
				.AddIngredient(ItemID.WoodenArrow)
				.AddIngredient(ItemID.Acorn)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

}