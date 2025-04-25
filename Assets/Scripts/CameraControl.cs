using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Follow Player")]
    [SerializeField] Transform playerPos;

    [Header("Rooms")]
    [SerializeField] List<Rooms> rooms;
    [SerializeField] Rooms currentRoom;

    [Header("Transition")]
    [SerializeField] float transitionSpeed;
    [SerializeField] bool isTransitioning;

    [System.Obsolete]
    private void Awake()
    {
        rooms = new List<Rooms>(FindObjectsOfType<Rooms>());
        currentRoom = GetRoomForPlayer(playerPos.position);
        if(currentRoom != null)
        {
            MoveCameraHorizontally(currentRoom);
        }
    }

    Rooms GetRoomForPlayer(Vector3 playerPos)
    {
        foreach(Rooms room in rooms)
        {
            if(IsPlayerInRoom(playerPos, room))
            { return room; }
        }

        return null;
    }

    bool IsPlayerInRoom(Vector3 playerPos, Rooms room)
    {
        Vector2 min = room.roomCenter - room.roomSize / 2;
        Vector2 max = room.roomCenter + room.roomSize / 2;

        return playerPos.x > min.x && playerPos.x < max.x && playerPos.y > min.y && playerPos.y < max.y;
    }

    private void Update()
    {
        Rooms newRoom = GetRoomForPlayer(playerPos.position);
/*        if(newRoom != null && newRoom != currentRoom && !isTransitioning)
        {
            StartCoroutine(CameraTransition(newRoom));
        }*/

        if(!isTransitioning && currentRoom != null)
        {
            if (currentRoom.horizontalRoom)
            {
                if (currentRoom.verticalRoom)
                {
                    //MoveCameraExpand();
                }
                else
                {
                    MoveCameraHorizontally(currentRoom);
                }
            }
            else if (currentRoom.verticalRoom)
            {
                //MoveCameraVertically();
            }
            else
            {
                MoveCameraCenter(currentRoom);
            }
        }
    }

    private void MoveCameraCenter(Rooms room)
    {
        transform.position = new Vector3(room.roomCenter.x, room.roomCenter.y, transform.position.z);
    }

    private void MoveCameraHorizontally(Rooms room)
    {
        Vector2 minBounds = room.roomCenter - room.roomSize / 2;
        Vector2 maxBounds = room.roomCenter + room.roomSize / 2;

        Vector3 followPlayer = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);

        float clampX = Mathf.Clamp(followPlayer.x, Mathf.Round(minBounds.x + CameraHalfWidth()), Mathf.Round(maxBounds.x - CameraHalfWidth()));

        transform.position = new Vector3(clampX, room.roomCenter.y, transform.position.z);

    }

    float CameraHalfWidth()
    {
        return Camera.main.orthographicSize * Camera.main.aspect;
    }

}
