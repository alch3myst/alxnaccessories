using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class JuggernautDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("JuggernautDebuff");
			Description.SetDefault("Losing life");
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

		public override void ResetEffects(NPC npc)
		{
			JuggernautDebuff = false;
		}

		private static int deltaDamageInterval = 300;
		private long endTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds() + deltaDamageInterval;
		private static UnifiedRandom random = new UnifiedRandom();
		public float critChance;
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (JuggernautDebuff) {

				long currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
				npc.lifeRegen -= 30;

				if (endTime < currentTime) {
					if (critChance / 100 >= random.NextFloat()) {
						npc.StrikeNPC((int)(jdamage * 1.5f), 0, 0, true, true);
					}
					npc.StrikeNPC(jdamage, 0, 0, false, true);
					endTime = currentTime + deltaDamageInterval;
				}
			}
		}
	}
}