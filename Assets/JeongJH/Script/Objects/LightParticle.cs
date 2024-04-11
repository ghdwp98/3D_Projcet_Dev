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
            //audioSource.PlayOneShot(clip); �Ҹ��ߺ� ó�� ���? 
            playAura = true;
            ParticleSystem.Play();
            //Destroy(gameObject, 2f); ������ �ڵ� ȸ���Ǵϱ�.. ���� destroy �� �ʿ�� ����.

        }

       /* private void OnTriggerEnter(Collider other) //Ʈ���ŷ� ����. ����.. 
        {
            if(Extension.Contain(playerLayer,other.gameObject.layer)) //������ �ֱ�. 
            {
                if(PlayerHp.Player_Action!=null)
                {
                    PlayerHp.Player_Action(1000f);
                }
            }
        }*/


    }
}

