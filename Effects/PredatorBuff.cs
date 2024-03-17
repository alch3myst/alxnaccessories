using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class PredatorBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetAttackSpeed(DamageClass.Melee) += 0.5f;
			player.moveSpeed += 0.3f;
        }
	}
}