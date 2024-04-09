using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Image fade;
    [SerializeField] Slider loadingBar;
    [SerializeField] float fadeTime;
    public static Vector3 playerPos;
    bool isRoading;

    private BaseScene curScene;

    public string GetCurSceneName()
    {
        return UnitySceneManager.GetActiveScene().name;
    }

    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene as T;
    }

    public void LoadScene(string sceneName) 
    {
        if (isRoading == true)
        {
            
            return;
        }
        else if (isRoading == false) //debug ��ü�� �� ���� �����µ� 
        {
            isRoading = true;
            Debug.Log("�� �ε� flase");
            StartCoroutine(LoadingRoutine(sceneName));
        }
        

    }

    IEnumerator LoadingRoutine(string sceneName)
    {

        fade.gameObject.SetActive(true);
        yield return FadeOut();

        Debug.Log("Ǯ ����");
        Manager.Pool.ClearPool();
        Manager.Sound.StopSFX();
        Manager.UI.ClearPopUpUI();
        Manager.UI.ClearWindowUI();
        Manager.UI.CloseInGameUI();

        Time.timeScale = 0f;
        loadingBar.gameObject.SetActive(true);

        //playerPos = CheckPoint.GetActiveCheckPointPosition();

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);

        while (oper.isDone == false)
        {
            loadingBar.value = oper.progress;
            yield return null;
        }

        Manager.UI.EnsureEventSystem();
        BaseScene curScene = GetCurScene();

        yield return curScene.LoadingRoutine();
        //���� ���� �ε� ��ƾ �۾� ����. -->�� ���� base���� �����ϸ� �Ǵ� ��? 

        loadingBar.gameObject.SetActive(false);
        Time.timeScale = 1f;

        yield return FadeIn();
        fade.gameObject.SetActive(false);
        isRoading = false;


    }

    IEnumerator FadeOut()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
            yield return null;
        }
    }
}
