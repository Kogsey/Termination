using Terraria.ModLoader;

namespace Termination.Projectiles.Summon.BaseClasses
{
	public abstract class Minion : ModProjectile
	{
		public override void AI() {
			CheckActive();
			Behavior();
		}

		public abstract void CheckActive();

		public abstract void Behavior();
	}
}