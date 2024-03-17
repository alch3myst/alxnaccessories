using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class JuggernautDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<JuggernautNPCDebuff>().JuggernautDebuff = true;
		}
	}

	public class JuggernautNPCDebuff : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool JuggernautDebuff;
		public int jdamage;
		public NPC.HitInfo JuggHitInfo;

		public override void ResetEffects(NPC npc)
		{
			JuggernautDebuff = false;
		}

		private static int deltaDamageInterval = 400;
		private long endTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + deltaDamageInterval;
		private static UnifiedRandom random = new UnifiedRandom();
		public float critChance;
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (JuggernautDebuff) {

				npc.buffImmune[ModContent.BuffType<JuggernautDebuff>()] = false;

				long currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
				npc.lifeRegen -= jdamage / 4;

				if (endTime < currentTime) {
                    JuggHitInfo.Crit = false;
                    JuggHitInfo.Damage = jdamage;

					// Roll crit dice
					if (critChance / 100 >= random.NextFloat()) {
						JuggHitInfo.Crit = true;
						JuggHitInfo.Damage = (int)(jdamage * 1.5f);
					}


                    npc.StrikeNPC(JuggHitInfo, true, true);
					endTime = currentTime + deltaDamageInterval;
				}
			}
		}
	}
}