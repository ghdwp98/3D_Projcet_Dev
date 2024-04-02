using System.Collections;
using UnityEngine;

namespace JJH
{
    public class PlayerHp : Entity
    {
        

        private void Awake()
        {
            // Entity�� ���ǵǾ� �ִ� Setup() �޼ҵ� ȣ��
            base.Setup();
        }

        private void Update()
        {
            // �⺻ ����
            if (Input.GetKeyDown("1"))
            {
                target.TakeDamage(20);
            }
            // ��ų ����
            else if (Input.GetKeyDown("2"))
            {
                MP -= 100;
                target.TakeDamage(55);
            }
        }

        // �⺻ ü�� + ���� ���ʽ� + ���� ��� ���� ���
        public override float MaxHP => MaxHPBasic + MaxHPAttrBonus;
        // 100 + ���緹�� * 30
        public float MaxHPBasic => 100 + 1 * 30;
        // �� * 10
        public float MaxHPAttrBonus => 10 * 10;

        public override float HPRecovery => 10;
        public override float MaxMP => 200;
        public override float MPRecovery => 25;

        public override void TakeDamage(float damage)
        {
            HP -= damage;
            
        }

      
    }
}


