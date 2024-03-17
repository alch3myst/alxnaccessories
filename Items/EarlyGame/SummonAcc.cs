using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.EarlyGame
{
	public class SummonAcc : ModItem {
		public override void SetStaticDefaults() {}

		public override void SetDefaults() {
			Item.width = 31;
			Item.height = 31;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 0, 6, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		private static long cycleTime = 6000;
		private long effectCycle = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + cycleTime;
		private long currentTime;
		private bool pactEnabled = false;
		public override void UpdateAccessory(Player player, bool hideVisual) {

			currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
			if (effectCycle < currentTime) {
				pactEnabled = !pactEnabled;
				effectCycle = currentTime + cycleTime;
			}

			if (pactEnabled) {
				player.GetDamage(DamageClass.Summon) += 0.66f;
			}
		}
		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ObsidianSkull)
				.AddIngredient(ItemID.RedTorch, 6)
				.AddIngredient(ItemID.MudBlock)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}