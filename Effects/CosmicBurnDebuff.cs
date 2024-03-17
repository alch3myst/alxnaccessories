using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Effects
{
	public class CosmicBurnDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
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
		public NPC.HitInfo CosmicHitInfo;

		public bool BurnOn;
		public override void ResetEffects(NPC npc) {
			BurnOn = false;
		}
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (BurnOn) {
                npc.buffImmune[ModContent.BuffType<CosmicBurnDebuff>()] = false;

                Tick++;
				if (npc.lifeRegen > 0) { npc.lifeRegen /= 10; }

				if (!StrongEffect) npc.lifeRegen -= burnDamage;
				
				if (StrongEffect) {
                    CosmicHitInfo.Crit = false;
                    CosmicHitInfo.Damage = burnDamage;

					if (Tick % 20 == 0)
					{
                        npc.StrikeNPC(CosmicHitInfo, true, true);
						NetMessage.SendStrikeNPC(npc, CosmicHitInfo);
					}
				}
			}
		}
	}
}