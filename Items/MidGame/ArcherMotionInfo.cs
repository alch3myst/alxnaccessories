using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace alxnaccessories.Items.MidGame
{
	// This example show how to create new informational display (like Radar, Watches, etc.)
	// Take a look at the ExampleInfoDisplayPlayer at the end of the file to see how to use it
	class ArcherMotionInfo : InfoDisplay
	{
		public override void SetStaticDefaults() {
			// This is the name that will show up when hovering over icon of this info display
			InfoName.SetDefault("Motion Stacks");
		}

		// This dictates whether or not this info display should be active
		public override bool Active() {
			return Main.LocalPlayer.GetModPlayer<ArcherMotionInfoPlayer>().showMotionStacks;
		}

		// Here we can change the value that will be displayed in the game
		public override string DisplayValue() {
			int motionStacks = Main.LocalPlayer.GetModPlayer<ArcherMotionPlayer>().GetMotionStacks();
			return motionStacks > 0 ? $"{motionStacks} Stacks." : "No Stacks";
		}
	}

	public class ArcherMotionInfoPlayer : ModPlayer
	{
		// Flag checking when information display should be activated
		public bool showMotionStacks;

		public override void ResetEffects() {
			showMotionStacks = false;
		}

		public override void UpdateEquips() {
			showMotionStacks = Main.LocalPlayer.GetModPlayer<ArcherMotionPlayer>().GetEquippedItem();
		}
	}
}