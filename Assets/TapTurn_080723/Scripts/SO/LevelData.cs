using UnityEngine;

namespace TapTurn
{
    [CreateAssetMenu(fileName = "Level_", menuName = "TapTurn/LevelData", order = 50)]
    public class LevelData : ScriptableObject
    {
        public int level;
        public Sprite icon;
    }
}

