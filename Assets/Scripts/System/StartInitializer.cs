using UnityEngine;

namespace Asteroids
{
    public class StartInitializer : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _shipPresenter;
        [SerializeField] private UfoPresenter _ufoPresenter;
        [SerializeField] private AsteroidPresenter _asteroidPresenter;
        //[SerializeField] private AsteroidPartPresenter _asteroidPartPresenter;
        [SerializeField] private PresentersFactory _presenterFactory;

        private void Awake()
        {
            _presenterFactory.SetTargetToUfo(_shipPresenter);

        }
    }
}