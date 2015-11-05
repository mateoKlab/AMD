using UnityEngine;

namespace Bingo
{
    public class EMonoBehaviour : MonoBehaviour
    {

        /*private Transform _transform;
        public new Transform transform
        {
            get { return _transform ?? (_transform = base.transform); }
        }*/

        private Rigidbody _rigidbody;
        public new Rigidbody rigidbody
        {
            get { return _rigidbody ?? (_rigidbody = base.GetComponent<Rigidbody>()); }
        }

        private Rigidbody2D _rigidbody2D;
        public new Rigidbody2D rigidbody2D
        {
            get { return _rigidbody2D ?? (_rigidbody2D = base.GetComponent<Rigidbody2D>()); }
        }

        private Camera _camera;
        public new Camera camera
        {
            get { return _camera ?? (_camera = base.GetComponent<Camera>()); }
        }

        private Light _light;
        public new Light light
        {
            get { return _light ?? (_light = base.GetComponent<Light>()); }
        }

        private Animation _animation;
        public new Animation animation
        {
            get { return _animation ?? (_animation = base.GetComponent<Animation>()); }
        }

        private ConstantForce _constantForce;
        public new ConstantForce constantForce
        {
            get { return _constantForce ?? (_constantForce = base.GetComponent<ConstantForce>()); }
        }

        private Renderer _renderer;
        public new Renderer renderer
        {
            get { return _renderer ?? (_renderer = base.GetComponent<Renderer>()); }
        }

        private AudioSource _audio;
        public new AudioSource audio
        {
            get { return _audio ?? (_audio = base.GetComponent<AudioSource>()); }
        }

        private GUIText _guiText;
        public new GUIText guiText
        {
            get { return _guiText ?? (_guiText = base.GetComponent<GUIText>()); }
        }

        private GUITexture _guiTexture;
        public new GUITexture guiTexture
        {
            get { return _guiTexture ?? (_guiTexture = base.GetComponent<GUITexture>()); }
        }

        private NetworkView _networkView;
        public new NetworkView networkView
        {
            get { return _networkView ?? (_networkView = base.GetComponent<NetworkView>()); }
        }

        private Collider _collider;
        public new Collider collider
        {
            get { return _collider ?? (_collider = base.GetComponent<Collider>()); }
        }

        private Collider2D _collider2D;
        public new Collider2D collider2D
        {
            get { return _collider2D ?? (_collider2D = base.GetComponent<Collider2D>()); }
        }

        private HingeJoint _hingeJoint;
        public new HingeJoint hingeJoint
        {
            get { return _hingeJoint ?? (_hingeJoint = base.GetComponent<HingeJoint>()); }
        }

        private ParticleEmitter _particleEmitter;
        public new ParticleEmitter particleEmitter
        {
            get { return _particleEmitter ?? (_particleEmitter = base.GetComponent<ParticleEmitter>()); }
        }

        private ParticleSystem _particleSystem;
        public new ParticleSystem particleSystem
        {
            get { return _particleSystem ?? (_particleSystem = base.GetComponent<ParticleSystem>()); }
        }

        private GameObject _gameObject;
        public new GameObject gameObject
        {
            get { return _gameObject ?? (_gameObject = base.gameObject); }
        }

        private string _tag;
        public new string tag
        {
            get { return _tag ?? (_tag = base.tag); }
            set { _tag = value; base.tag = value; }
        }

        private string _name;
        public new string name
        {
            get { return _name ?? (_name = base.name); }
            set { _tag = value; base.tag = value; }
        }
    }
}
