using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform fllowing;

    //Vị trí bắt đầu cho đối tượng trò chơi parallax
    Vector2 startPosition;
    //Giá trị z bắt đầu của trò chơi parallax
    float startZ;
    //Khoảng cách mà camera đã di chuyển tính từ vị trí bắt đầu của vật parallax
    Vector2 camMoveStar => (Vector2)cam.transform.position - startPosition;
    float zdistanceFromTarget => transform.position.z - fllowing.transform.position.z;


    //Nếu đối tượng ở phía trước mục tiêu, hãy sử dụng mặt phẳng clip gần. nếu ở phía sau mục tiêu, hãy sử dụng farclippalne
    float clipPlane => (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    //Vật thể càng xa người chơi thì hiệu ứng thị sai sẽ di chuyển càng nhanh. kéo giá trị Z của nó đến gần mục tiêu hơn để di chuyển chậm hơn
    float parallaxFactor => Mathf.Abs(zdistanceFromTarget) / clipPlane;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //khi mục tiêu di chuyển, di chuyển parallax, đối tượng cùng khoảng cách nhân với số nhân
        Vector2 newPosition = startPosition + camMoveStar * parallaxFactor;
        //vị trí x/y thay đổi dựa trên tốc độ di chuyển của mục tiêu nhân với hệ số parallax , nhưng z vẫn khong doi
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
