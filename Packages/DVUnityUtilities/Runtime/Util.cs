using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DVUnityUtilities
{
    public static class Util
    {
        public static T[,] TransposeArray<T>(T[,] originalArray)
        {
            int rows = originalArray.GetLength(0);
            int cols = originalArray.GetLength(1);

            // Create a new array with dimensions swapped
            T[,] transposedArray = new T[cols, rows];

            // Copy elements from the original array to the transposed array
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposedArray[j, i] = originalArray[i, j];
                }
            }

            return transposedArray;
        }

        public static bool IsPrefab(GameObject go)
        {
            return go.scene.name == null;
        }

        public static bool IsPrefab(Component c)
        {
            return IsPrefab(c.gameObject);
        }

        public static bool IsPrefab(Transform t)
        {
            return IsPrefab(t.gameObject);
        }

        public static string CreateMd5ForFolder(string path)
        {
            using (new PerformanceAnalyzer())
            {
                // default alg
                var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Where(x => Path.GetExtension(x) != ".meta")
                                     .OrderBy(p => p).ToList();

                MD5 md5 = MD5.Create();

                string file, relativePath;
                byte[] contentBytes, pathBytes;
                for (int i = 0; i < files.Count; i++)
                {
                    file = files[i];
                    // hash path
                    relativePath = file.Substring(path.Length + 1);
                    pathBytes = System.Text.Encoding.UTF8.GetBytes(relativePath.ToLower());
                    md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                    // hash contents
                    contentBytes = File.ReadAllBytes(file);
                    if (i == files.Count - 1)
                        md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                    else
                        md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
                }
                return System.BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
            }
        }

        public static async Task<string> CreateMd5ForFolderAsync(string path)
        {
            return await Task.Run<string>(() =>
            {
                return CreateMd5ForFolder(path);
            });
        }

        public static void WriteToJson(object obj, string full_path_with_extension)
        {
            System.IO.File.WriteAllText(full_path_with_extension, JsonUtility.ToJson(obj, true));
        }

        public static bool IsEntityOverMouse(Transform cam, out RaycastHit obj, float max_dist = 1f, int layermask = -1, QueryTriggerInteraction trigger = QueryTriggerInteraction.Ignore)
        {
            if (Physics.Raycast(cam.position, cam.forward, out obj, max_dist, layermask, trigger))
            {
                return true;
            }
            return false;
        }

        public static int BoxCastNonAlloc(Vector3 pos, Vector3 dir, float size, RaycastHit[] res, float maxdist, int layermask, QueryTriggerInteraction ignore)
        {
            Quaternion orientation = Quaternion.LookRotation(dir);
            Vector3 scale = new Vector3(size, size, maxdist);

            pos = pos + dir * maxdist / 2;
            //Util.DebugDrawWireCube(pos, scale, orientation, Color.red, 10);
            return Physics.BoxCastNonAlloc(pos, scale / 2, dir, res, orientation, maxdist, layermask, ignore);
        }

        public static Transform GetUpper(Transform o)
        {
            var up_parent = o.parent;
            if (up_parent == null)
            {
                return o;
            }
            else
            {
                return GetUpper(up_parent);
            }
        }

        public static List<T> GetListCopy<T>(List<T> list)
        {
            var r = new List<T>(list.Count);
            r.AddRange(list);
            return r;
        }

        public static List<Transform> GetObjectChilds(Transform obj, bool recursive = true)
        {
            var objects = new List<Transform>();

            if (obj != null)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    var o = obj.transform.GetChild(i);
                    objects.Add(o);
                    if (recursive) objects.AddRange(GetObjectChilds(o));
                }
            }
            return objects;
        }

        public static List<Transform> GetAllChilds(this Transform t, bool recursive = true)
        {
            return Util.GetObjectChilds(t, recursive);
        }

        public static void RemoveAllChilds(this Transform t)
        {
            Util.GetObjectChilds(t).ForEach(x => GameObject.Destroy(x.gameObject));
        }

        public static float Difference(float number1, float number2)
        {
            return Mathf.Abs(number1 > number2 ? number1 - number2 : number2 - number1);
        }

        public static bool IsVisibleOnScreen(in Vector3 screenPos)
        {
            return screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
        }

        public static bool IsGenericList(this System.Type oType)
        {
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }

        public static void DestroyGameObjectsAndClear<T>(this List<T> objects) where T : Component
        {
            objects.ForEach(x => GameObject.Destroy(x.gameObject));
            objects.Clear();
        }

        public static void DestroyGameObjectsWithoutClear<T>(this IEnumerable<T> objects) where T : Component
        {
            foreach (var x in objects)
            {
                GameObject.Destroy(x.gameObject);
            }
        }

        public static void DestroyGameObjectsWithoutClear(this IEnumerable<GameObject> objects)
        {
            foreach (var x in objects)
            {
                GameObject.Destroy(x.gameObject);
            }
        }

        public static ParticleSystem CreateEffectWithDestroyAfterPlay(in Vector3 pos, in Vector3 rot, ParticleSystem prefab)
        {
            if (prefab == null) return null;

            var ef = GameObject.Instantiate(prefab);
            ef.transform.position = pos;
            ef.transform.eulerAngles = rot;
            ef.Play();
            GameObject.Destroy(ef.gameObject, ef.main.duration);
            return ef;
        }

        public static ParticleSystem CreateEffectWithDestroyAfterPlay(in Vector3 pos, ParticleSystem prefab)
        {
            if (prefab == null) return null;

            var ef = GameObject.Instantiate(prefab);
            ef.transform.position = pos;
            ef.Play();
            GameObject.Destroy(ef.gameObject, ef.main.duration);
            return ef;
        }

        public static T InstantiateWithSameName<T>(T prefab) where T : Object
        {
            var v = GameObject.Instantiate<T>(prefab);
            v.name = prefab.name;
            return v;
        }

        public static GameObject InstantiateWithSameName(GameObject prefab)
        {
            var v = GameObject.Instantiate(prefab);
            v.name = prefab.name;
            return v;
        }

        public static Vector3 FindCenter(List<Vector3> transforms)
        {
            var bound = new Bounds(transforms[0], Vector3.zero);
            for (int i = 1; i < transforms.Count; i++)
            {
                bound.Encapsulate(transforms[i]);
            }
            return bound.center;
        }

        public static Vector3 FindCenter(List<Renderer> renders)
        {
            Vector3 center = Vector3.zero;

            foreach (Renderer renderer in renders)
            {
                if (renderer != null)
                {
                    center += renderer.bounds.center;
                    // Util.DebugDrawWireCube(renderer.bounds.center, renderer.bounds.size, Quaternion.identity, Color.green, 1);
                }
            }

            center /= renders.Count;
            return center;
        }

        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResultsOverMouse());
        }

        public static List<RaycastResult> GetEventSystemRaycastResultsOverMouse()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }

        public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetMinMax(ref float min_v, ref float max_v, float v1, float v2)
        {
            if (v1 > v2)
            {
                max_v = v1;
                min_v = v2;
            }
            else
            {
                min_v = v1;
                max_v = v2;
            }
        }

        public static T AddOrGetComponent<T>(this GameObject go) where T : Component
        {
            var res = go.GetComponent<T>();
            if (res == null) return go.AddComponent<T>();
            return res;
        }

        public static T CreateGameObjectWithComponent<T>(Transform parent = null, bool saveWorldStay = true) where T : Component
        {
            var go = new GameObject($"[{(typeof(T).Name).ToUpper()}]").AddComponent<T>();
            go.transform.position = Vector3.zero;
            go.transform.SetParent(parent, saveWorldStay);
            return go;
        }

        public static T CreateGameObjectWithComponent<T>(string name, in Vector3 pos, Transform parent = null, bool saveWorldStay = true) where T : Component
        {
            var go = new GameObject(name).AddComponent<T>();
            go.transform.position = pos;
            go.transform.SetParent(parent, saveWorldStay);
            return go;
        }

        public static Vector3 GetMouseWorldPosition(Camera camera, int layerMask = -1)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var distance, 1000, layerMask, QueryTriggerInteraction.Ignore)) return distance.point;
            else return Vector3.positiveInfinity;
        }

        public static bool IsAllButtonsPressedAndLastDown(in KeyCode[] keys)
        {
            for (int i = 0; i < keys.Length - 1; i++)
            {
                if (!Input.GetKey(keys[i]))
                {
                    return false;
                }
            }

            if (!Input.GetKeyDown(keys[keys.Length - 1]))
            {
                return false;
            }

            return true;
        }
    }
}