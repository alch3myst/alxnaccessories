using Terraria.ID;
using Terraria;
using Terraria.ModLoader;


namespace alxnaccessories.AlxUtils
{
	public class Loggers {
		public void Logger(string Literal) {
			Terraria.Chat.ChatHelper.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral (Literal), Colors.CoinCopper);
		}
	}
}