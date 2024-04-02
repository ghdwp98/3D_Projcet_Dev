using UnityEngine;

namespace JJH
{
    public class Player : MonoBehaviour
    {
        public float speed;

        public GameObject[] weapons;
        public bool[] hasWeapons;
        [SerializeField] PlayerHp playerhpmp; 



        public int key; // 갖고 있는 열쇠 수
        public int maxKey; // 최대 열쇠 소지 수

        float hAxis;
        float vAxis;

        bool jDown; // Jump
        bool iDown; // Interaction
        bool dDown; // Down

        bool isJump; // 점프

        GameObject nearObject;

        Vector3 moveVec;
        Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void Start()
        {

        }

        private void Update()
        {
            GetInput();
            Move();
            Turn();
            Jump();
            
            if(Input.GetKey(KeyCode.LeftShift))
            {
                playerhpmp.RunStaminaConsume(0.5f);
            }

           
        }

        void GetInput()
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
            jDown = Input.GetButtonDown("Jump");
            iDown = Input.GetButtonDown("Interaction");
            dDown = Input.GetButton("Down");
        }

        void Move()
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * speed * Time.deltaTime;

        }

        void Turn()
        {
            transform.LookAt(transform.position + moveVec);
        }

        void Jump()
        {
            if (jDown && !isJump)
            {
                rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
                isJump = true;
            }
        }

        void Interaction()
        {
            if (iDown && nearObject != null && !isJump)
            {
                if (nearObject.tag == "Weapon")
                {
                    Item item = nearObject.GetComponent<Item>();
                    int weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;

                    Destroy(nearObject);
                }
            }
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Item")
            {
                Item item = other.GetComponent<Item>();
                switch (item.type)
                {
                    case Item.Type.Key:
                        key += item.value;
                        if (key > maxKey)
                            key = maxKey;
                        break;
                }
                Destroy(other.gameObject);
            }
        }

        void OnDamegeLayer()
        {
            gameObject.layer = 0;
        }

        private void OnCollisionEnter(Collision collision) //지금 ㄷ미지 받는 상황 같은데 여기서 처리를 해보자. 
        {
            if (collision.gameObject.tag == "Floor")
                isJump = false;

            if (collision.gameObject.layer == 31)
            {
                Debug.Log("충돌진입");
                gameObject.layer = 6;
                PlayerHp.Player_Action?.Invoke(10);
                Invoke("OnDamegeLayer", 1f);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Weapon")
                nearObject = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Weapon")
                nearObject = null;
        }
    }
}