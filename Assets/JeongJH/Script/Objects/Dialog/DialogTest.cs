using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem	dialogSystem01;
	[SerializeField]
	private	Text textCountdown;
	[SerializeField]
	private	DialogSystem	dialogSystem02;

	private IEnumerator Start()
	{
		//textCountdown.gameObject.SetActive(false);

		 //ù ��° ��� �б� ����
		yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());

		//npc�� ��ġ�� ���� ��µǴ� system�� ������ ������ ����� (��ǥġ�� �����ϸ� ������ ��µǵ��� )




		 //��� �б� ���̿� ���ϴ� �ൿ�� �߰��� �� �ִ�.
		 //ĳ���͸� �����̰ų� �������� ȹ���ϴ� ����.. ����� 5-4-3-2-1 ī��Ʈ �ٿ� ����
		/*textCountdown.gameObject.SetActive(true);

		int count = 5;
		while ( count > 0 )
		{
			textCountdown.text = count.ToString();
			count --;

			yield return new WaitForSecondsRealtime(1);
		}
		textCountdown.gameObject.SetActive(false);*/

		// �� ��° ��� �б� ����
		yield return new WaitUntil(()=>dialogSystem02.UpdateDialog());

		/*textCountdown.gameObject.SetActive(true);
		textCountdown.text = "The End";*/

		//yield return new WaitForSecondsRealtime(2);

		//UnityEditor.EditorApplication.ExitPlaymode(); //-->��簡 ������ �����. 
	}
}

