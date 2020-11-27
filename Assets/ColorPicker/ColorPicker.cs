using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{

	public Texture2D colorSpace;
	public GameObject receiver;
	public GameObject[] otherReceivers;
	public string colorSetFunctionName = "OnSetNewColor";
	public string colorGetFunctionName = "OnGetColor";
	public bool useExternalDrawer = false;
	//public int drawOrder = 0;

	private Color TempColor;
	private Color SelectedColor;

	public Color yellow;

	float sizeCurr = 300;

	public Button buyButton;

	void Start()
	{
		buyButton.interactable = false;
		receiver.GetComponent<Renderer>().material.color = yellow;
        for (int i = 0; i < otherReceivers.Length; i++)
        {
			otherReceivers[i].GetComponent<Renderer>().material.color = yellow;
		}
		receiver.SendMessage(colorGetFunctionName, this, SendMessageOptions.DontRequireReceiver);
	}

	public void NotifyColor(Color color)
	{
		SetColor(color);
		SelectedColor = color;
		for (int i = 0; i < otherReceivers.Length; i++)
		{
			otherReceivers[i].GetComponent<Renderer>().material.color = receiver.GetComponent<Renderer>().material.color;
		}
	}

	void OnGUI()
	{
		if (!useExternalDrawer)
		{
			DrawGUI();
		}
	}

	public void DrawGUI()
	{
		//draw color picker
		Rect rect = new Rect(50, 350, sizeCurr, sizeCurr);
		GUI.DrawTexture(rect, colorSpace);

		Vector2 mousePos = Event.current.mousePosition;
		Event e = Event.current;
		if (rect.Contains(e.mousePosition))
		{
			buyButton.interactable = true; 
			float coeffX = colorSpace.width / sizeCurr;
			float coeffY = colorSpace.height / sizeCurr;
			Vector2 localImagePos = (mousePos - new Vector2(50, 350));
			Color res = colorSpace.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y) - 1);
			SetColor(res);
			ApplyColor();
			NotifyColor(res);
		}
		else
		{
			SetColor(SelectedColor);
		}
		
		/*Rect rect = new Rect(50, 350, sizeCurr, sizeCurr);
		GUI.DrawTexture(rect, colorSpace);

		if (Input.touchCount > 0)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 pos = touch.position;
				if (rect.Contains(pos))
				{
					buyButton.interactable = true;
					float coeffX = colorSpace.width / sizeCurr;
					float coeffY = colorSpace.height / sizeCurr;
					Vector2 localImagePos = (Input.GetTouch(i).position - new Vector2(50, 400));
					Color res = colorSpace.GetPixel((int)(coeffX * localImagePos.x), colorSpace.height - (int)(coeffY * localImagePos.y) -1);
					SetColor(res);
					ApplyColor();
				}
				else
                {
					SetColor(SelectedColor);
				}
			}
		}*/
	}

	public void SetColor(Color color)
	{
		TempColor = color;
	}

	public Color GetColor()
	{
		return TempColor;
	}

	public void ApplyColor()
	{
		SelectedColor = TempColor;
		receiver.SendMessage(colorSetFunctionName, SelectedColor, SendMessageOptions.DontRequireReceiver);
	}

	public void Buy()
	{
		ApplyColor();
		SelectedColor = TempColor;
		receiver.SendMessage(colorSetFunctionName, SelectedColor, SendMessageOptions.DontRequireReceiver);
	}
}
