using UnityEngine;
using System.Collections;

public class MeshAnime : MonoBehaviour {


public bool  onceFlg = false;
public bool  offsetAnimeYFlg = false;
public Material thisMaterial;
public float offsetSpd = 1;
public float animeSpeed = 30.0F;
public int uvX = 0;  
public int uvY = 0; 
public Vector2 initOffset;
public float delay = 0;
public float delayBck = 0;

private float timer;
private ParticleSystem thisPS;

private Vector2 size;
	private Vector2 mainTextureOffset;



void Start (){
	thisMaterial = this.GetComponent<Renderer>().material;
	delayBck = delay;
	thisPS = this.GetComponent<ParticleSystem>();
}

void Update (){ 
	if(delay <= 0){
		timer += Time.deltaTime;
	}else{
		delay -= Time.deltaTime;
	}
	
	if( offsetAnimeYFlg ){
			mainTextureOffset.y += Time.deltaTime * offsetSpd;
		//	thisMaterial.mainTextureOffset.y += Time.deltaTime * offsetSpd;
			thisMaterial.mainTextureOffset = mainTextureOffset;
		if( thisMaterial.mainTextureOffset.y > 1)thisMaterial.mainTextureOffset = Vector2.zero;
	}else{
		if( !thisPS.isPlaying ){
			thisMaterial.SetTextureOffset ("_MainTex", new Vector2(0 ,0) );
			thisMaterial.SetTextureScale ("_MainTex", new Vector2(0 ,0));
			
			timer = 0;
			initOffset = Vector2.zero;
			delay = delayBck;
		}else{
			float index = timer * animeSpeed;
		
			index = (int)(index % (uvX * uvY));
		
			size= new Vector2 (1.0f / uvX, 1.0f / uvY);
		
				int uIndex= (int)(index % uvX);
				int vIndex= (int)(index / uvX);
				Vector2 offset= new Vector2 (uIndex * size.x, 1.0f - size.y - vIndex * size.y);
		 	
		 	if( onceFlg ){
			 	if( initOffset.magnitude == 0 && index == (uvX * uvY)-1){
			 		initOffset = offset;
			 		//Debug.Log("index " + index);
			 	}else if(initOffset.magnitude != 0){
			 		offset = initOffset;
			 	}
		 	}
		 	
			thisMaterial.SetTextureOffset ("_MainTex", offset);
			thisMaterial.SetTextureScale ("_MainTex", size);
		}
	}
}
}