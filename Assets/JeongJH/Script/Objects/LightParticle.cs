using UnityEngine;

namespace JJH
{
    public class LightParticle : MonoBehaviour
    {
        public bool playAura = true;
        public ParticleSystem ParticleSystem;
        public LayerMask playerLayer;
        public AudioSource audioSource;
        public AudioClip clip;

        void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            //audioSource.PlayOneShot(clip); 소리중복 처리 어떻게? 
            playAura = true;
            ParticleSystem.Play();
            //Destroy(gameObject, 2f); 어차피 자동 회수되니까.. 굳이 destroy 할 필요는 없지.

        }

       /* private void OnTriggerEnter(Collider other) //트리거로 가자. 차라리.. 
        {
            if(Extension.Contain(playerLayer,other.gameObject.layer)) //데미지 주기. 
            {
                if(PlayerHp.Player_Action!=null)
                {
                    PlayerHp.Player_Action(1000f);
                }
            }
        }*/


    }
}

