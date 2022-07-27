using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects.SysthesesEffects
{
	public class SynthesesCold : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Syntheses Cold");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<SynthesesColdNPCDebuff>().ColdOn = true;
		}
	}

	public class SynthesesColdNPCDebuff : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool ColdOn;
		public override void ResetEffects(NPC npc) {
			ColdOn = false;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (ColdOn) {
				npc.velocity *= 0.933f;
			}
		}
	}
}