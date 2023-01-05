using UnityEngine;
using UnityEngine.UI;
using System.IO;
using MoilDll;
using TMPro;
public class show_cam : MonoBehaviour {
    public UnityEngine.UI.Image Rot_Image;
    // public Text Rot_Text;
    public TextMeshProUGUI Tmp_Text;
    Texture2D tex;
    byte[] ByteArray;
    private Sprite mySprite;
	MemoryStream memoryStream;
    float yaw, pitch;
    Moil moil = new Moil();
    float yaw_prev, pitch_prev;
    void Start () {

        float x = transform.eulerAngles.x;
        x = 360f - x;
        if (x == 360) x = 0;
        float y = transform.eulerAngles.y;
        float z = transform.eulerAngles.z;
        // Rot_Text.text = x.ToString("F3") + "    " + y.ToString("F3") + "    " + z.ToString("F3");
        Tmp_Text.text = x.ToString("F3") + "    " + y.ToString("F3") + "    " + z.ToString("F3");

        // 轉換
        if (x >= 0 && x <= 90) pitch = x;
        else if (x >= 270 && x <= 360) pitch = x - 360;
        else pitch = 0;
        if (y >= 0 && y <= 90) yaw = y;
        else if (y >= 270 && y <= 360) yaw = y - 360;
        else yaw = 0;

        ByteArray = moil.process(yaw, pitch, 4);
        yaw_prev = yaw;
        pitch_prev = pitch;

        memoryStream = new MemoryStream(5000000);
		memoryStream.Write(ByteArray, 0, ByteArray.Length);
		memoryStream.Position = 0;
        // tex = new Texture2D(1920, 1440);
        tex = new Texture2D(960, 720);
        tex.LoadImage(ByteArray);
        mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 50.0f);
        Rot_Image.sprite = mySprite;
        
    }
	

	void Update () {

        float x = transform.eulerAngles.x;
        x = 360f - x;
        if (x == 360) x = 0;
        float y = transform.eulerAngles.y;
        float z = transform.eulerAngles.z;
        // Rot_Text.text = x.ToString("F3") + "    " + y.ToString("F3") + "    " + z.ToString("F3");
        Tmp_Text.text = x.ToString("F3") + "    " + y.ToString("F3") + "    " + z.ToString("F3");

        // 轉換
        if (x >= 0 && x <= 90) pitch = x;
        else if (x >= 270 && x <= 360) pitch = x - 360;
        else pitch = 0;
        if (y >= 0 && y <= 90) yaw = y;
        else if (y >= 270 && y <= 360) yaw = y - 360;
        else yaw = 0;
        if (yaw != yaw_prev || pitch != pitch_prev)
        {
            ByteArray = moil.process(yaw, pitch, 4);
            yaw_prev = yaw;
            pitch_prev = pitch;
        }
        memoryStream.Write(ByteArray, 0, ByteArray.Length);
		memoryStream.Position = 0;		
		
        // tex = new Texture2D(640, 480);
        tex.LoadImage(ByteArray);
		mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, 960, 720), new Vector2(0.5f, 0.5f), 50.0f);
		Rot_Image.sprite = mySprite;
        

    }
 
}
