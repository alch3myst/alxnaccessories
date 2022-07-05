using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class MageAccDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cosmic Burn");
			Description.SetDefault("Losing life");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<MageAccNPCDebuff>().MageAcc = true;
		}
	}

	public class MageAccNPCDebuff : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool MageAcc;
		public int burnDamage;
		public int Tick = 0;

		public override void ResetEffects(NPC npc)
		{
			MageAcc = false;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (MageAcc) {
				Tick++;
				if (npc.lifeRegen > 0) { npc.lifeRegen = 0; }

				long currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
				npc.lifeRegen -= burnDamage;
				
				// if (Tick % 50 == 0) { npc.StrikeNPCNoInteraction(burnDamage,0,0,false,true); };
			}
		}
	}
}