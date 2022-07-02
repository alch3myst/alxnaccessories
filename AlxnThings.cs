using Terraria.ID;


namespace alxnaccessories.Things
{
	public class Loggers {
		public void Logger(string Literal) {
			Terraria.Chat.ChatHelper.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral (Literal), Colors.CoinCopper);
		}
	}
}