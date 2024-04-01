using UnityEngine;

namespace JJH
{
    public class LightParticle : MonoBehaviour
    {
        public bool playAura = true;
        public ParticleSystem ParticleSystem;

        void Start()
        {
            playAura = true;
            ParticleSystem.Play();
            Destroy(gameObject, 2f);
        }

        void Update()
        {

        }
    }
}

