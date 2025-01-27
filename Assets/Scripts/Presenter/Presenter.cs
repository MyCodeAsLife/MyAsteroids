using System;
using UnityEngine;

namespace Asteroids
{
    public class Presenter : MonoBehaviour
    {
        private Transformable _objectModel;
        private ViewMovement _viewMovement;
        private ModelMovement _objectMovement;

        public event Action<Presenter> Destroyed;
        public event Action<float> Updated;

        public int OverlapLayer { get; private set; }

        private void Start()
        {
            _viewMovement = new ViewMovement(transform);
            var _canvas = GetComponentInParent<Canvas>();
            var _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            _objectMovement.SetScreenAspectRatio(_displaySize);
            _viewMovement.SetDisplaySize(_displaySize);
            //------------------------------------------------------------------------------------------
            //MyBaseClass myBase = new MyBaseClass();
            //MyDerivedClass myDerived = new MyDerivedClass();
            //object o = myDerived;
            //MyBaseClass b = myDerived;

            //Debug.Log("mybase: Type is " + myBase.GetType().Name);
            //Debug.Log("myDerived: Type is " + myDerived.GetType().Name);
            //Debug.Log("object o = myDerived: Type is " + o.GetType().Name);
            //Debug.Log("MyBaseClass b = myDerived: Type is " + b.GetType().Name);
            //------------------------------------------------------------------------------------------
        }

        private void Update()
        {
            _objectMovement.Tick(Time.deltaTime);
            _viewMovement.Move(_objectModel.Position);
            _viewMovement.Rotate(_objectModel.RotationAngle);
            CollisionCheck();
        }

        public void SetModel(Transformable model) => _objectModel = model;
        public void SetModelMovement(ModelMovement movement) => _objectMovement = movement;
        public void SetOverlapLayer(int layer) => OverlapLayer = layer;

        public void CollisionCheck()
        {
            var hit = Physics2D.OverlapCircle(transform.position, 3f, 1 << OverlapLayer);               // Радиус передавать от дочернего объекта

            if (hit != null)
                Debug.Log(hit.gameObject.name);

            Destroyed?.Invoke(this);
        }
    }
}

//internal class MyDerivedClass : MyBaseClass
//{
//    public MyDerivedClass()
//    {
//    }
//}

//internal class MyBaseClass
//{
//    public MyBaseClass()
//    {
//    }
//}