using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 DistancePlayGame;
    [SerializeField] private Vector3 DistanceMenu;
    [SerializeField] private Vector3 DistanceSkin;
    private Vector3 targetPosition;
    private Transform target;
    
    private void Start()
    {
        
        
    }
    private void LateUpdate()
    {
        if(Bot.Instance.PlayerSpawn != null)
        {
            target = Bot.Instance.PlayerSpawn.transform;
            CammeraFl(target);
        }       
    }
    public void CammeraFl(Transform playerTransform)
    {
        if (playerTransform != null && GameManager.IsState(GameState.GamePlay))
        {
            Quaternion rota = Quaternion.Euler(40, 0, 0);
            transform.rotation = rota;

            Vector3 desiredPosition = playerTransform.position + DistancePlayGame;

            // Sử dụng hàm Lerp để di chuyển camera một cách mượt mà
            targetPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Chỉ di chuyển camera nếu sự khác biệt giữa vị trí hiện tại và vị trí mới đủ lớn
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = targetPosition;
            }
        }
        else if (playerTransform != null && GameManager.IsState(GameState.MainMenu))
        {
            //góc quay
            Quaternion rota = Quaternion.Euler(20, 0, 0);
            transform.rotation = rota;

            Vector3 desiredPosition = playerTransform.position + DistanceMenu;
            // Sử dụng hàm Lerp để di chuyển camera một cách mượt mà
            targetPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Chỉ di chuyển camera nếu sự khác biệt giữa vị trí hiện tại và vị trí mới đủ lớn
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = targetPosition;
            }
        }
        else if (playerTransform != null && GameManager.IsState(GameState.SetSkin))
        {
            //góc quay
            Quaternion rota = Quaternion.Euler(20, 0, 0);
            transform.rotation = rota;

            Vector3 desiredPosition = playerTransform.position + DistanceSkin;
            // Sử dụng hàm Lerp để di chuyển camera một cách mượt mà
            targetPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Chỉ di chuyển camera nếu sự khác biệt giữa vị trí hiện tại và vị trí mới đủ lớn
            if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = targetPosition;
            }
        }
    }
    public void UpdateBaseDistance(float newBaseDistance)
    {
        DistancePlayGame += new Vector3(0, newBaseDistance, -newBaseDistance);
    }
    public void ResetCamera()
    {
        DistancePlayGame = new Vector3(0,14,-16);
       
    }
}
