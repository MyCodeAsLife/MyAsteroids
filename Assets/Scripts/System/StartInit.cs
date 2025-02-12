using UnityEngine;

namespace Asteroids
{
    public class StartInit : MonoBehaviour
    {
        private void Awake()
        {
            var audioSystem = FindFirstObjectByType<RootAudioSystem>();

            if (audioSystem == null)
            {
                var prefab = Resources.Load<RootAudioSystem>("Prefabs/AudioSystem");
                Instantiate(prefab);
            }
        }
    }
}