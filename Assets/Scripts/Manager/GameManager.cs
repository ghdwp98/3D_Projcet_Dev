using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool saved;
    public static Vector3 playerPos;




    public void Test()
    {
        Debug.Log(GetInstanceID());
    }
}
