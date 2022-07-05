using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace alxnaccessories.Items.EarlyGame
{
	public class SummonAcc : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Pact");
			Tooltip.SetDefault("Every 7 seconds minions deal 70% more damage for 7 seconds.");
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
		}

		private static long cycleTime = 7000;
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
				player.GetDamage(DamageClass.Summon) += 0.8f;
			}
		}
		
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ObsidianSkull)
				.AddIngredient(ItemID.RedTorch, 3)
				.AddIngredient(ItemID.MudBlock)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}