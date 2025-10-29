using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    [SerializeField] private float AngularSpeed = 50f;

    [SerializeField] float coinHeight = 0.7f;

    [SerializeField] float MovementAmplitude = 0.5f;

    [SerializeField] float MovemantFrequecy = 1f;

    [SerializeField] private Transform coinMesh;
   
    void Update()
    {

        coinMesh.Rotate(0f, AngularSpeed * Time.deltaTime, 0f);

        float deltay = MovementAmplitude * Mathf.Sin(MovementAmplitude * Time.deltaTime);

        coinMesh.localPosition = new Vector3 (0f , coinHeight + deltay, 0f);
    }
}
