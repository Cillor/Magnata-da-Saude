using UnityEngine;
using UnityEngine.UI;
using System.Collections;
    
public class ImageFade : MonoBehaviour {
    public Image img;

    public void ImageFader(Sprite sprite){
        img.gameObject.SetActive(true);
        img.sprite = sprite;
        // fades the image out when you click
        StartCoroutine(FadeImage());
    }
    
    IEnumerator FadeImage()
    {
        img.color = new Color(1, 1, 1, 0);

        // loop over 1 second backwards
        for (float i = 0; i <= 0.5f; i += Time.deltaTime){
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i*2);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // loop over 1 second
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i*2);
            yield return null;
        }
        img.gameObject.SetActive(false);
    }
}
     

