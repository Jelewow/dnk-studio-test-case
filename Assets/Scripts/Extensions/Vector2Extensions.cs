using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Jelewow.DNK.Extensions
{
    public static class Vector2Extensions
    {
        public static bool IsPointerOverUIObject(this Vector2 touchPosition)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = touchPosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}