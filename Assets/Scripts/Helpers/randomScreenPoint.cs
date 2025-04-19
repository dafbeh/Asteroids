using System.Collections;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Helpers
{
    public class SpawnHelper : MonoBehaviour {
        public static Vector3 randomScreenLocation(float offset) {
            int edge = Random.Range(1, 5);
            float x = 0f;
            float y = 0f;

            switch(edge) {
                case 1: // Top
                    x = Random.Range(0f, 1f);
                    y = 1f + offset;
                    break;
                case 2: // Bottom
                    x = Random.Range(0f, 1f);
                    y = 0f - offset;
                    break;
                case 3: // Left
                    x = 0f - offset;
                    y = Random.Range(0f, 1f);
                    break;
                case 4: // Right
                    x = 1f + offset;
                    y = Random.Range(0f, 1f);
                    break;
            }
            return new Vector3(x, y, 0);
        }
    }
}