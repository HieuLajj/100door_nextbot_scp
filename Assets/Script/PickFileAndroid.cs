using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PickFileAndroid : MonoBehaviour
{
    // Start is called before the first frame update
    public string FinalPath;
    public Button ButtonImage;
	public Button ButtonVideo;
	public VideoPlayer videoPlayer;
	private Sprite mySprite;
	public SpriteRenderer image;
	public GameObject ImageEnm;
	public GameObject VideoEnm;
	private static PickFileAndroid instance;
	public static PickFileAndroid Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<PickFileAndroid>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    
	public void PickImage()
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
		{
			if( path != null )
			{
				Texture2D texture = NativeGallery.LoadImageAtPath( path);
				if( texture == null )
				{
					// Debug.Log( "Couldn't load texture from " + path );
					return;
				}
				//mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				GameControll.Instance.PathImageSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				GameControll.Instance.PathVideo="";
				
				
				Item item = new Item("image","hieu",GameControll.Instance.PathImageSprite,null);
				GameObject g = Instantiate(UISelectHero.Instance.PreNextbots,  UISelectHero.Instance.ContainerNextbotui);
				NextbotBtn nb = g.GetComponent<NextbotBtn>();
				nb.ItemData = item;
				nb.SetUpData();
				g.transform.SetSiblingIndex(0);
				if(UISelectHero.Instance.preSelectBtnNextbot!=null){
                	UISelectHero.Instance.preSelectBtnNextbot.GetComponent<NextbotBtn>().backgroundSelectNextbot.SetActive(false);
            	}	
				nb.backgroundSelectNextbot.SetActive(true);
				UISelectHero.Instance.preSelectBtnNextbot = g;
			}
		} );
	}

public void PickVideo()
{
	NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery( ( path ) =>
	{
		if( path != null )
		{
			//videoPlayer.url = path;
			GameControll.Instance.PathVideo=path;
			GameControll.Instance.PathImageSprite = null;
		}
	}, "Select a video" );
}
    // IEnumerator LoadTexture(string path2){
    //     WWW www = new WWW(path2);
    //     while (!www.isDone)
    //     {
    //         yield return null;
    //     }
    //     Cube.GetComponent<Renderer>().material.mainTexture = www.texture;
    // }

}
