using BepInEx.Configuration;

namespace ItemChoice
{
    public class _Config
    {
        public static ConfigEntry<bool> chestDrops;
        public static ConfigEntry<bool> totemDrops;
        public static ConfigEntry<bool> bossDrops;

        public static void Init(ConfigFile config)
        {
            chestDrops = config.Bind(
                "General",
                "Choose from Chest",
                true,
                "Whether or not ONLY the powerup-choosing object appears in loot chests. (true/false)"
                );
            totemDrops = config.Bind(
                "General",
                "Choose from Totem",
                true,
                "Whether or not ONLY the powerup-choosing object drops from battle totems. (true/false)"
                );
            bossDrops = config.Bind(
                "General",
                "Choose from Boss",
                true,
                "Whether or not ONLY the powerup-choosing object drops from bosses. (true/false)"
                );

        }

        public static bool LetItemChoiceDrop(string type)
        {
            switch (type) {
                case "chest":
                    return chestDrops.Value;
                case "totem":
                    return totemDrops.Value;
                case "boss":
                    return bossDrops.Value;
            }
            return false;
        }
    }

}
