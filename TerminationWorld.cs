using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Termination
{
    public class TerminationWorld : ModWorld
    {
        private const int saveVersion = 0;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Molexium", delegate (GenerationProgress progress)
                {
                    progress.Message = "Molexium";

                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), mod.TileType("Molexium"), false, 0f, 0f, false, true);
                    }
                }));

                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Araaxium", delegate (GenerationProgress progress)
                {
                    progress.Message = "Araaxium";

                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(6, 12), WorldGen.genRand.Next(6, 12), mod.TileType("Araaxium"), false, 0f, 0f, false, true);
                    }
                }));

                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Sirenium", delegate (GenerationProgress progress)
                {
                    progress.Message = "Sirenium";

                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), mod.TileType("Sirenium"), false, 0f, 0f, false, true);
                    }
                }));
            }
        }
    }
}