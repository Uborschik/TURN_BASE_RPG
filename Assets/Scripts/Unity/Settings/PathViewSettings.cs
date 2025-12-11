using UnityEngine;

namespace GameUnity.Settings
{
    [CreateAssetMenu(fileName = "PathViewSettings", menuName = "Game/Settings/PathView")]
    public class PathViewSettings : ScriptableObject
    {
        public Color PathColor = Color.yellow;
        public float PathWidth = 0.1f;
    }
}