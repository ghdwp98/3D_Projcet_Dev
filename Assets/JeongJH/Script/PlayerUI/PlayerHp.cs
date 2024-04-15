using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

namespace JJH
{
    public class PlayerHp : Entity

    //�÷��̾��� ��� ó���� ���⼭ �����ϸ� �� �� ��. EVENT �̿�. 
    {
        public static Action<float> Player_Action;
        public static Action<float> Player_Stamina_Action;

        public static UnityEvent PlayerDeath;


        private void Awake()
        {
            // Entity�� ���ǵǾ� �ִ� Setup() �޼ҵ� ȣ��
            base.Setup();
            Player_Action += TakeDamage;
            Player_Stamina_Action += RunStaminaConsume;
        }

        private void Update() //Ȯ�ο� �ӽ� 
        {
            
           /* if (Input.GetKeyDown("1"))
            {
                target.TakeDamage(20);
            }
            
            else if (Input.GetKeyDown("2"))
            {
                MP -= 100;
                target.TakeDamage(55);
            }*/

            if(HP<=0)
            {
                Debug.Log("hp <=0");
                PlayerDie();

            }



        }

        // �⺻ ü�� + ���� ���ʽ� + ���� ��� ���� ���
        public override float MaxHP => MaxHPBasic + MaxHPAttrBonus;
        // 100 + ���緹�� * 30
        public float MaxHPBasic => 100 + 1 * 30; //130
        // �� * 10
        public float MaxHPAttrBonus => 10 * 10; //100 -->�� hp ����  230

        public override float HPRecovery => 10;
        public override float MaxMP => 200;
        public override float MPRecovery => 1; //0.1�ʴ� ���¹̳� recovery �� ����. 

        public override void TakeDamage(float damage)
        {
            
            HP -= damage;
            /*if (HP <= 0) //�÷��̾� �����. 
            {
                Debug.Log($"{HP} : �����������");
                PlayerDie();
            }*/

        }

        public void RunStaminaConsume(float runStaminas)
        {
            MP -= runStaminas;
        }

        public void PlayerDie()
        {           
            Manager.Scene.LoadScene(Manager.Scene.GetCurSceneName()); //�̰ɷ� �� ��ε� �Ϲ�ȭ ����
            // ������� ��ε� �ϴ� �����. --> �� ���̽��� ��� ������Ʈ���� 
            // gameMnager�� playerPos�� ���������. 
            Debug.Log("�÷��̾��������");

        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
        }

    }
}


