using UnityEngine;

namespace GameUnity.Settings
{
    [CreateAssetMenu(fileName = "UnitsViewSettings", menuName = "Game/Settings/UnitsView")]
    public class UnitsViewSettings : ScriptableObject
    {
        public UnitViewData[] PlayerTeam;
        public UnitViewData[] EnemyTeam;
        public GameObject Prefab;
    }
}
