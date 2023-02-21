using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextEffect : MonoBehaviour
{
    public TMP_Text TextMesh;
    Mesh mesh;
    Vector3[] vertices;
    // Update is called once per frame
    public GameObject ResetBtn;
    public GameObject BackBtn;
    void Update()
    {
        TextMesh.ForceMeshUpdate();
        mesh = TextMesh.mesh;
        vertices = mesh.vertices;
        for(int i = 0; i < TextMesh.textInfo.characterCount; i++){
            TMP_CharacterInfo c = TextMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;
            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index+1] += offset;
            vertices[index+2] += offset;
            vertices[index+3] += offset;
        }

        mesh.vertices = vertices;
        TextMesh.canvasRenderer.SetMesh(mesh);
    }
    Vector2 Wobble(float time){
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }

    IEnumerator ZoomOut(){
        ResetBtn.gameObject.SetActive(false);
        BackBtn.gameObject.SetActive(false);
        for(int f = 80; f > 8; f -= 1){
            yield return null;
            gameObject.transform.localScale = new Vector3( f, f, 1);
        }
        ResetBtn.gameObject.SetActive(true);
        BackBtn.gameObject.SetActive(true);
    }
    public void StartZoom(){
        StartCoroutine(ZoomOut());
    }

}
