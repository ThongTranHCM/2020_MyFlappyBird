using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public float generateStartPosition, distanceBetweenWave;
    public float numberOfWaves;
    public GameObject wavePrefab;
    private List<Wave> waves = new List<Wave>();
    // Start is called before the first frame update

    private void FirstGenerate()
    {
        float x = generateStartPosition;
        for (int i = 0; i < numberOfWaves; i++, x += distanceBetweenWave)
        {
            GameObject wObject = Instantiate(wavePrefab, transform);
            Wave wave = wObject.GetComponent(typeof(Wave)) as Wave;
            wave.customTransform.UpdateDelta(Vector3.right * x, Vector3.right * x);
            waves.Add(wave);
        }
    }
    void Start()
    {
        FirstGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        Transform firstChild = transform.GetChild(0);
        Transform lastChild = transform.GetChild(transform.childCount - 1);
        if (firstChild.transform.position.x < Camera.main.transform.position.x - 3)
        {
            Wave wave = firstChild.GetComponent(typeof(Wave)) as Wave;
            wave.position = lastChild.transform.position + Vector3.right * distanceBetweenWave;
            wave.ResetWave();
            firstChild.transform.SetAsLastSibling();
        }
    }
}
