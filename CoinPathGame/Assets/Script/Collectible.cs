using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public GameObject onCollectEffect;

    void Update()
    {
        // 프레임에 독립적으로 초당 rotationSpeed 만큼 회전
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    // 함수가 클래스 { } 중괄호 안에 들어와 있어야 합니다!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
                GameManager.instance.AddScore(10);

            if (onCollectEffect != null)
            {
                // 파티클 생성
                GameObject effect = Instantiate(onCollectEffect, transform.position, transform.rotation);

                // 2초 뒤에 이 파티클 오브젝트를 삭제해라!
                Destroy(effect, 2.0f);
            }

            Destroy(gameObject);
        }
    }
}