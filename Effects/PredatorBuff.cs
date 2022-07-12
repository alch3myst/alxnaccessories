using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class PredatorBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Predator");
			Description.SetDefault("Increased Attack Speed");
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		}
	}
}