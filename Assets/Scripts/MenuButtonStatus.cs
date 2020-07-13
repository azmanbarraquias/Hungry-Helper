using UnityEngine;
using UnityEngine.UI;

public class MenuButtonStatus : MonoBehaviour
{
	public GameObject page;

	public Image[] spritesStatus;

	public int index;

	public void Check()
	{
		foreach (var sprite in spritesStatus)
		{
			sprite.gameObject.SetActive(false);
		}

		spritesStatus[index].gameObject.SetActive(true);
	}
}
