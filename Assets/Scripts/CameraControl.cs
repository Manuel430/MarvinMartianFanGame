using System.Collections;
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
        if(newRoom != null && newRoom != currentRoom && !isTransitioning)
        {
            StartCoroutine(CameraTransition(newRoom));
        }

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
                MoveCameraVertically(currentRoom);
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

    private void MoveCameraVertically(Rooms room)
    {
        Vector2 minBounds = room.roomCenter - room.roomSize / 2;
        Vector2 maxBounds = room.roomCenter + room.roomSize / 2;

        Vector3 followPlayer = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);

        float clampY = Mathf.Clamp(followPlayer.y, Mathf.Round(minBounds.y + CameraHalfHeight()), Mathf.Round(maxBounds.y - CameraHalfHeight()));

        transform.position = new Vector3(room.roomCenter.x, clampY, transform.position.z);
    }

    float CameraHalfWidth()
    {
        return Camera.main.orthographicSize * Camera.main.aspect;
    }

    float CameraHalfHeight()
    {
        return Camera.main.orthographicSize;
    }

    IEnumerator CameraTransition(Rooms newRoom)
    {
        isTransitioning = true;

        Vector3 currentPos = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);

        Vector2 minBounds = newRoom.roomCenter - newRoom.roomSize / 2;
        Vector3 maxBounds = newRoom.roomCenter + newRoom.roomSize / 2;

        float clampX = Mathf.Clamp(currentPos.x, Mathf.Round(minBounds.x + CameraHalfWidth()), Mathf.Round(maxBounds.x - CameraHalfWidth()));
        float clampY = Mathf.Clamp(currentPos.y, Mathf.Round(minBounds.y + CameraHalfHeight()), Mathf.Round(maxBounds.y - CameraHalfHeight()));

        Vector3 targetPos = new Vector3(clampX, clampY, transform.position.z);
        Vector3 startPos = transform.position;

        float elapsedTime = 0;
        float duration = Vector3.Distance(targetPos, startPos) / transitionSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        currentRoom = newRoom;
        isTransitioning = false;
    }

}
