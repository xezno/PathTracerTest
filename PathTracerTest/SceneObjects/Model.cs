using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PathTracerTest.Materials;
using PathTracerTest.MathUtils;
using PathTracerTest.Raytracer;

namespace PathTracerTest.SceneObjects
{
    public class Model : ISceneObject
    {
        public IMaterial material;

        public List<Vector3> vertices = new List<Vector3>();
        public List<Vector2> texCoords = new List<Vector2>();
        public List<Vector3> normals = new List<Vector3>();

        public List<uint> vertexIndices = new List<uint>();
        public List<uint> normalIndices = new List<uint>();
        public List<uint> textureIndices = new List<uint>();

        public Model(string pathToObj, IMaterial material)
        {
            this.material = material;
        }

        private static int GetCharCount(string s, char c)
        {
            var i = 0;
            foreach (char c_ in s) if (c_ == c)  ++i;
            return i;
        }

        public void LoadDataAsset(string fileName, byte[] data)
        {
            using (var memStream = new MemoryStream(data))
            using (var sr = new StreamReader(memStream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line.Length < 1 || line[0] == '#')
                        continue;

                    var elementId = line.Remove(line.IndexOf(' '));
                    var parameterCount = GetCharCount(line, ' ');
                    switch (elementId)
                    {
                        case "v": // Vertex position (xyz[w])
                            // Check whether the optional parameter is present or not
                            if (parameterCount == 3)
                            {
                                // Only xyz
                                var baseLine = line.Remove(0, line.IndexOf(' ') + 1);
                                var x = baseLine.Remove(baseLine.IndexOf(' '));
                                var y = baseLine.Remove(0, baseLine.IndexOf(' ') + 1);
                                y = y.Remove(y.LastIndexOf(' ') - 1);
                                var z = baseLine.Remove(0, baseLine.LastIndexOf(' ') + 1);
                                vertices.Add(new Vector3(float.Parse(x), float.Parse(y), float.Parse(z)));
                            }
                            else if (parameterCount == 4)
                            {
                                // xyzw
                                throw new Exception("Optional parameter not implemented yet.");
                            }
                            else
                            {
                                throw new Exception("obj file is not valid.");
                            }
                            break;
                        case "vt": // Texture coordinate (uv[w])
                            if (parameterCount == 2)
                            {
                                // Only uv
                                var baseLine = line.Remove(0, line.IndexOf(' ') + 1);
                                var u = baseLine.Remove(baseLine.IndexOf(' '));
                                var v = baseLine.Remove(0, baseLine.LastIndexOf(' ') + 1);
                                texCoords.Add(new Vector2(float.Parse(u), float.Parse(v)));
                            }
                            else if (parameterCount == 3)
                            {
                                Console.WriteLine("UVW used");
                                // uvw
                                var baseLine = line.Remove(0, line.IndexOf(' ') + 1);
                                var u = baseLine.Remove(baseLine.IndexOf(' '));
                                var v = baseLine.Remove(0, baseLine.IndexOf(' ') + 1);
                                v = v.Remove(v.LastIndexOf(' ') - 1);
                                var w = baseLine.Remove(0, baseLine.LastIndexOf(' ') + 1);
                                //normals.Add(new Vector3(float.Parse(u), float.Parse(v), float.Parse(w)));
                                texCoords.Add(new Vector2(float.Parse(u), float.Parse(v)));
                            }
                            else
                            {
                                throw new Exception("obj file is not valid.");
                            }
                            break;
                        case "vn": // Vertex normal (xyz)
                            // Check whether the optional parameter is present or not
                            if (parameterCount == 3)
                            {
                                // Only xyz
                                var baseLine = line.Remove(0, line.IndexOf(' ') + 1);
                                var x = baseLine.Remove(baseLine.IndexOf(' '));
                                var y = baseLine.Remove(0, baseLine.IndexOf(' ') + 1);
                                y = y.Remove(y.LastIndexOf(' ') - 1);
                                var z = baseLine.Remove(0, baseLine.LastIndexOf(' ') + 1);
                                normals.Add(new Vector3(float.Parse(x), float.Parse(y), float.Parse(z)));
                            }
                            else
                            {
                                throw new Exception("obj file is not valid.");
                            }
                            break;
                        case "vp": // Parameter space vertex (u[vw])
                            Console.WriteLine("Parameter space vertices are not supported by this mesh loader.");
                            break;
                        case "f": // Face
                            // Indices
                            if (parameterCount == 3)
                            {
                                var baseLine = line.Remove(0, line.IndexOf(' ') + 1);
                                var tmp = baseLine.Split('/');
                                var parameters = new List<string>();
                                bool[] nVal = new bool[9];
                                int i = 0;
                                foreach (string p in tmp)
                                {
                                    foreach (string s in p.Split(' '))
                                    {
                                        parameters.Add(s);
                                        if (string.IsNullOrEmpty(s))
                                        {
                                            nVal[i] = true;
                                            Console.WriteLine("Parameter had no value (" + fileName + ")");
                                        }
                                        i++;
                                    }
                                }
                                vertexIndices.Add((nVal[0] ? 0 : uint.Parse(parameters[0]) - 1)); // v1
                                textureIndices.Add((nVal[1] ? 0 : uint.Parse(parameters[1]) - 1)); // vt1
                                normalIndices.Add((nVal[2] ? 0 : uint.Parse(parameters[2]) - 1)); // vn1

                                vertexIndices.Add((nVal[3] ? 0 : uint.Parse(parameters[3]) - 1)); // v2
                                textureIndices.Add((nVal[4] ? 0 : uint.Parse(parameters[4]) - 1)); // vt2
                                normalIndices.Add((nVal[5] ? 0 : uint.Parse(parameters[5]) - 1)); // vn2

                                vertexIndices.Add((nVal[6] ? 0 : uint.Parse(parameters[6]) - 1)); // v3
                                textureIndices.Add((nVal[7] ? 0 : uint.Parse(parameters[7]) - 1)); // vt3
                                normalIndices.Add((nVal[8] ? 0 : uint.Parse(parameters[8]) - 1)); // vn3
                            }
                            else
                            {
                                Console.WriteLine("Faces must be triangulated."); // TODO: automatic triangulation on load
                            }
                            break;
                        case "l": // Line
                            Console.WriteLine("Polylines are not supported by this mesh loader.");
                            break;
                        case "mtllib": // Define material
                            Console.WriteLine("Materials are not implemented yet.");
                            break;
                        case "usemtl": // Use material
                            Console.WriteLine("Materials are not implemented yet.");
                            break;
                        case "o": // Object
                            break;
                        case "g": // Polygon group
                            break;
                        case "s": // Smooth shading
                            Console.WriteLine("Smooth shading is not supported by this mesh loader.");
                            break;
                        default:
                            Console.WriteLine("OBJ file is not valid!");
                            break;
                    }
                }
            }
        }

        public bool GetHit(Ray ray, float tMin, float tMax, out RayHit rayHit)
        {
            throw new NotImplementedException();
        }
    }
}
