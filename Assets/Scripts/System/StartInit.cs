using UnityEngine;

namespace Asteroids
{
    public class StartInit : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1612, 907, true);
            var audioSystem = FindFirstObjectByType<RootAudioSystem>();

            if (audioSystem == null)
            {
                var prefab = Resources.Load<RootAudioSystem>("Prefabs/AudioSystem");
                Instantiate(prefab);
            }
        }
    }
}