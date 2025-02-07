using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    public class ExplosionPresenter : Presenter
    {
        public override event Action<Presenter> Deactivated;

        public void Explode(Transform obj)
        {
            transform.position = obj.position;
            transform.localScale = obj.localScale;
            StartCoroutine(PlayEffect());
        }

        public override void OnPauseSwith()
        {
            //throw new NotImplementedException();
        }

        private IEnumerator PlayEffect()
        {
            const float Duration = 0.6f;
            yield return new WaitForSeconds(Duration);
            Deactivated?.Invoke(this);
        }
    }
}