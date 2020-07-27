using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Termination
{
    internal class Termination : Mod
    {
        public Termination()
        {
            // By default, all Autoload properties are True. You only need to change this if you know what you are doing.
            //Properties = new ModProperties()
            //{
            //	Autoload = true,
            //	AutoloadGores = true,
            //	AutoloadSounds = true,
            //	AutoloadBackgrounds = true
            //}
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            var player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }

        public override void Load()
        {
            // All of this loading needs to be client-side.

            if (Main.netMode != NetmodeID.Server)
            {
                // First, you load in your shader file.
                // You'll have to do this regardless of what kind of shader it is,
                // and you'll have to do it for every shader file.
                // This example assumes you have both armour and screen shaders.

                // Ref<Effect> dyeRef = new Ref<Effect>(GetEffect("Effects/MyDyes"));
                // Ref<Effect> specialRef = new Ref<Effect>(GetEffect("Effects/MySpecials"));
                Ref<Effect> filterRef = new Ref<Effect>(GetEffect("Effects/ElectronicEyeEffect"));

                // To add a dye, simply add this for every dye you want to add.
                // "PassName" should correspond to the name of your pass within the *technique*,
                // so if you get an error here, make sure you've spelled it right across your effect file.

                // GameShaders.Armor.BindShader(ItemType<MyDyeItem>(), new ArmorShaderData(dyeRef, "PassName"));

                // If your dye takes specific parameters such as colour, you can append them after binding the shader.
                // IntelliSense should be able to help you out here.

                // GameShaders.Armor.BindShader(ItemType<MyColourDyeItem>(), new ArmorShaderData(dyeRef, "ColourPass")).UseColor(1.5f, 0.15f, 0f);
                // GameShaders.Armor.BindShader(ItemType<MyNoiseDyeItem>(), new ArmorShaderData(dyeRef, "NoisePass")).UseImage("Images/Misc/noise"); // Uses the default Terraria noise map.

                // To bind a miscellaneous, non-filter effect, use this.
                // If you're actually using this, you probably already know what you're doing anyway.

                // GameShaders.Misc["EffectName"] = new MiscShaderData(specialref, "PassName");

                // To bind a screen shader, use this.
                // EffectPriority should be set to whatever you think is reasonable.

                Filters.Scene["ElectronicEyeEffect"] = new Filter(new ScreenShaderData(filterRef, "ElectronicEyeEffect"), EffectPriority.Medium);
            }
        }
    }
}