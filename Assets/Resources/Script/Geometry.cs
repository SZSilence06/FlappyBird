using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FlappyBird {
    class Geometry {
        public static Rect GetObjectRect(GameObject obj) {
            float width = obj.GetComponent<SpriteRenderer>().sprite.rect.width / 100.0f;
            float height = obj.GetComponent<SpriteRenderer>().sprite.rect.height / 100.0f;
            Rect rect = new Rect(obj.transform.position.x - width / 2, obj.transform.position.y - height / 2,
                width, height);
            return rect;
        }

        public static bool SegIntersect(float x1,float length1,float x2,float length2){
            float x3 = x1 + length1;
            float x4 = x2 + length2;
            if (x1 < x2) {
                return x3 >= x2;
            }
            return x4 >= x1;
        }

        public static bool RectIntersect(Rect a, Rect b) {
            if(SegIntersect(a.xMin,a.width,b.xMin,b.width) &&
                SegIntersect(a.yMin, a.height, b.yMin, b.height)) {
                    return true;
            }
            return false;
        }

        public static Vector2 PixelStandardize(Vector2 pixelPos) {
            return new Vector2(pixelPos.x / Screen.width * 288, pixelPos.y / Screen.height * 512);
        }
    }
}
