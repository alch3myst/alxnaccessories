using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using alxnaccessories.Effects.SysthesesEffects;

namespace alxnaccessories.Items.EndGame {
	public class Syntheses : ModItem {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Syntheses");
			Tooltip.SetDefault(
				"20% Increased magic damage\n"
				+ "Cycles between four elements\n"
				+ "Slowing, Poisoning, Bleeding and Igniting the enemy\n"
				+ "Hits deals more damage based on element.\n"
				+ "Syntheses can influence other items.\n"
			);
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;

			Item.value = Item.buyPrice(5, 0, 0, 0);
			Item.rare = ItemRarityID.Red;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}


		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<SynthesesPlayer>().SynthesesOn = true;
			player.GetDamage(DamageClass.Magic) +=  0.2f;

			player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.BlueJay)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

	public class SynthesesPlayer : ModPlayer {
		public int stage = 0;
		public bool SynthesesOn;

		public override void ResetEffects()
		{
			SynthesesOn = false;
			Player.GetModPlayer<AlxnGlobalPlayer>().GSyntheses = false;
		}

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (SynthesesOn && proj.DamageType == DamageClass.Magic) {
				SynthesesHit(target, ref damage, ref knockback, ref crit);
			}
		}

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { 
			if (SynthesesOn && item.DamageType == DamageClass.Magic) {
				SynthesesHit(target, ref damage, ref knockback, ref crit);
			}
		}

		private void SynthesesHit(NPC target, ref int damage, ref float knockback, ref bool crit) {
			switch (stage) {
				case 0: // Cold
					damage += damage/4;
					target.buffImmune[ModContent.BuffType<SynthesesCold>()] = false;
					target.AddBuff(ModContent.BuffType<SynthesesCold>(), 150);

					stage++;
					break;

				case 1: // Lightning
					damage += damage/3;
					target.buffImmune[BuffID.Bleeding] = false;
					target.AddBuff(BuffID.Bleeding, 300);

					stage++;
					break;

				case 2: // Cosmic
					damage += damage/2;
					target.buffImmune[BuffID.Poisoned] = false;
					target.AddBuff(BuffID.Poisoned, 200);

					stage++;
					break;

				case 3: // Fire
					damage += damage;
					target.buffImmune[BuffID.OnFire3] = false;
					target.AddBuff(BuffID.OnFire3, 300);

					stage = 0;
					break;

				default:
					break;
			}
		}
	}
}