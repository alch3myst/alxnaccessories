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
			Tooltip.SetDefault("+10% summon damage\n"
			+"Every 7 seconds minions deal 70% increased damage for 7 seconds.");
		}

		public override void SetDefaults() {
			Item.width = 31;
			Item.height = 31;
			Item.accessory = true;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Green;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
				player.GetDamage(DamageClass.Summon) += 0.7f;
			} else {
				player.GetDamage(DamageClass.Summon) += 0.1f;
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