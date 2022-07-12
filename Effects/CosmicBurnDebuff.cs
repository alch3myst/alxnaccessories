using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class CosmicBurnDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cosmic Burn");
			Description.SetDefault("Losing life");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) {
			npc.GetGlobalNPC<CosmicBurnNPCDebuff>().BurnOn = true;
		}
	}

	public class CosmicBurnNPCDebuff : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public int burnDamage;
		public bool StrongEffect;
		public int Tick = 0;

		public bool BurnOn;
		public override void ResetEffects(NPC npc) {
			BurnOn = false;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (BurnOn) {
				Tick++;
				if (npc.lifeRegen > 0) { npc.lifeRegen = 0; }

				long currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
				if (!StrongEffect) npc.lifeRegen -= burnDamage;
				
				if (StrongEffect) {
					if (Tick % 20 == 0) npc.StrikeNPCNoInteraction(burnDamage, 0, 0, false, true);
				}
			}
		}
	}
}