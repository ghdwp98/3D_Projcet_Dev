using System.Collections;
using UnityEngine;
using JJH;
using System;

namespace JJH
{
    public class PlayerHp : Entity

        //플레이어의 사망 처리도 여기서 진행하면 될 듯 함. EVENT 이용. 
    {
        public static Action<float> Player_Action;
        public static Action<float> Player_Stamina_Action;

        


        private void Awake()
        {
            // Entity에 정의되어 있는 Setup() 메소드 호출
            base.Setup();
            Player_Action += TakeDamage;
            Player_Stamina_Action += RunStaminaConsume;
        }

        private void Update() //확인용 임시 
        {
            // 기본 공격
            if (Input.GetKeyDown("1"))
            {
                target.TakeDamage(20);
            }
            // 스킬 공격
            else if (Input.GetKeyDown("2"))
            {
                MP -= 100;
                target.TakeDamage(55);
            }
        }

        // 기본 체력 + 스탯 보너스 + 버프 등과 같이 계산
        public override float MaxHP => MaxHPBasic + MaxHPAttrBonus;
        // 100 + 현재레벨 * 30
        public float MaxHPBasic => 100 + 1 * 30; //130
        // 힘 * 10
        public float MaxHPAttrBonus => 10 * 10; //100 -->총 hp 현재  230

        public override float HPRecovery => 10;
        public override float MaxMP => 200;
        public override float MPRecovery => 1; //0.1초당 스태미너 recovery 값 변경. 

        public override void TakeDamage(float damage)
        {
            HP -= damage;
            
        }

        public void RunStaminaConsume(float runStaminas)
        {
            MP -= runStaminas;
        }


      
    }
}


