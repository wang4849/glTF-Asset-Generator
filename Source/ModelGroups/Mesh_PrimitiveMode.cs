﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static AssetGenerator.Runtime.MeshPrimitive;

namespace AssetGenerator
{
    internal class Mesh_PrimitiveMode : ModelGroup
    {
        public override ModelGroupId Id => ModelGroupId.Mesh_PrimitiveMode;

        public Mesh_PrimitiveMode(List<string> imageList)
        {
            UseFigure(imageList, "Indices");
            UseFigure(imageList, "Indices_Points");

            // There are no common properties in this model group that are reported in the readme.

            Model CreateModel(Action<List<Property>, Runtime.MeshPrimitive> setProperties)
            {
                var properties = new List<Property>();
                Runtime.MeshPrimitive meshPrimitive = MeshPrimitive.CreateSinglePlane(includeTextureCoords: false, includeIndices: false);

                // Apply the properties that are specific to this gltf.
                setProperties(properties, meshPrimitive);

                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        MetallicFactor = 0
                    },
                };

                // Create the gltf object.
                return new Model
                {
                    Properties = properties,
                    GLTF = CreateGLTF(() => new Runtime.Scene
                    {
                        Nodes = new List<Runtime.Node>
                        {
                            new Runtime.Node
                            {
                                Mesh = new Runtime.Mesh
                                {
                                    MeshPrimitives = new List<Runtime.MeshPrimitive>
                                    {
                                        meshPrimitive
                                    }
                                },
                            },
                        },
                    }),
                };
            }

            void SetModePoints(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                var pointPositions = new List<Vector3>();
                var cornerPoints = new List<Vector3>
                {
                    new Vector3( 0.5f, -0.5f, 0.0f),
                    new Vector3(-0.5f, -0.5f, 0.0f),
                    new Vector3(-0.5f,  0.5f, 0.0f),
                    new Vector3( 0.5f,  0.3f, 0.0f),
                    new Vector3( 0.5f, -0.5f, 0.0f)
                };

                for (var corner = 0; corner < 4; corner++)
                {
                    for (float x = 256; x > 0; x--)
                    {
                        Vector3 startPoint = cornerPoints[corner];
                        Vector3 endPoint = cornerPoints[corner + 1];
                        float fractionOfLine = x / 256f;
                        pointPositions.Add(GetPointOnLine(startPoint, endPoint, fractionOfLine));
                    }
                }

                meshPrimitive.Mode = ModeEnum.POINTS;
                meshPrimitive.Positions = pointPositions;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeLines(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                        new Vector3( 0.5f,  0.3f, 0.0f),
                        new Vector3( 0.5f,  0.3f, 0.0f),
                        new Vector3( 0.5f, -0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.LINES;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeLineLoop(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.3f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.LINE_LOOP;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeLineStrip(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.3f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f, -0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.LINE_STRIP;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeTriangleStrip(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.TRIANGLE_STRIP;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeTriangleFan(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.TRIANGLE_FAN;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetModeTriangles(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                if (meshPrimitive.Indices == null)
                {
                    meshPrimitive.Positions = new List<Vector3>
                    {
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f, -0.5f, 0.0f),
                        new Vector3( 0.5f,  0.5f, 0.0f),
                        new Vector3(-0.5f,  0.5f, 0.0f),
                    };
                }
                meshPrimitive.Mode = ModeEnum.TRIANGLES;
                properties.Add(new Property(PropertyName.Mode, meshPrimitive.Mode));
            }

            void SetIndicesPoints(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                var pointsIndices = new List<int>();
                for (var x = 0; x < meshPrimitive.Positions.Count(); x++)
                {
                    pointsIndices.Add(x);
                }
                meshPrimitive.Indices = pointsIndices;
                properties.Add(new Property(PropertyName.IndicesValues, $"[0 - {meshPrimitive.Positions.Count() - 1}]"));
            }

            void SetIndicesLines(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = GetSinglePlaneNonReversiblePositions();
                meshPrimitive.Indices = new List<int>
                {
                    0, 3,
                    3, 2,
                    2, 1,
                    1, 0,
                };
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesLineLoop(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = GetSinglePlaneNonReversiblePositions();
                meshPrimitive.Indices = new List<int>
                {
                    0, 3, 2, 1,
                };
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesTriangleFan(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = MeshPrimitive.GetSinglePlanePositions();
                meshPrimitive.Indices = new List<int>
                {
                    0, 3, 2, 1,
                };
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesLineStrip(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = GetSinglePlaneNonReversiblePositions();
                meshPrimitive.Indices = new List<int>
                {
                    0, 3, 2, 1, 0,
                };
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesTriangleStrip(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = MeshPrimitive.GetSinglePlanePositions();
                meshPrimitive.Indices = new List<int>
                {
                    0, 3, 1, 2,
                };
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesTriangles(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Positions = MeshPrimitive.GetSinglePlanePositions();
                meshPrimitive.Indices = MeshPrimitive.GetSinglePlaneIndices();
                properties.Add(new Property(PropertyName.IndicesValues, meshPrimitive.Indices));
            }

            void SetIndicesComponentTypeInt(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.IndexComponentType = IndexComponentTypeEnum.UNSIGNED_INT;
                properties.Add(new Property(PropertyName.IndicesComponentType, meshPrimitive.IndexComponentType));
            }

            void SetIndicesComponentTypeByte(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.IndexComponentType = IndexComponentTypeEnum.UNSIGNED_BYTE;
                properties.Add(new Property(PropertyName.IndicesComponentType, meshPrimitive.IndexComponentType));
            }

            void SetIndicesComponentTypeShort(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.IndexComponentType = IndexComponentTypeEnum.UNSIGNED_SHORT;
                properties.Add(new Property(PropertyName.IndicesComponentType, meshPrimitive.IndexComponentType));
            }

            Models = new List<Model>
            {
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModePoints(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLines(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLineLoop(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLineStrip(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangleStrip(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangleFan(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangles(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModePoints(properties, meshPrimitive);
                    SetIndicesPoints(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLines(properties, meshPrimitive);
                    SetIndicesLines(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLineLoop(properties, meshPrimitive);
                    SetIndicesLineLoop(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeLineStrip(properties, meshPrimitive);
                    SetIndicesLineStrip(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangleStrip(properties, meshPrimitive);
                    SetIndicesTriangleStrip(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangleFan(properties, meshPrimitive);
                    SetIndicesTriangleFan(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangles(properties, meshPrimitive);
                    SetIndicesTriangles(properties, meshPrimitive);
                    SetIndicesComponentTypeInt(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangles(properties, meshPrimitive);
                    SetIndicesTriangles(properties, meshPrimitive);
                    SetIndicesComponentTypeByte(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetModeTriangles(properties, meshPrimitive);
                    SetIndicesTriangles(properties, meshPrimitive);
                    SetIndicesComponentTypeShort(properties, meshPrimitive);
                }),
            };

            GenerateUsedPropertiesList();
        }

        /// <summary>
        /// Creates a point on a line between a start and end point. The exact position of that point is determined by the fraction that is passed in.
        /// E.g. A fraction of 0.25f returns the point 25% from the starting point.
        /// </summary>
        private static Vector3 GetPointOnLine(Vector3 point1, Vector3 point2, float fractionOfSegment)
        {
            return new Vector3
            {
                X = point1.X + fractionOfSegment * (point2.X - point1.X),
                Y = point1.Y + fractionOfSegment * (point2.Y - point1.Y),
                Z = point1.Z + fractionOfSegment * (point2.Z - point1.Z)
            };
        }

        /// <summary>
        ///  Used to generate positions for points and lines modes, so it is easy to see if the model is reversed or not.
        /// </summary>
        private static List<Vector3> GetSinglePlaneNonReversiblePositions()
        {
            return new List<Vector3>
            {
                new Vector3( 0.5f, -0.5f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3(-0.5f,  0.5f, 0.0f),
                new Vector3( 0.5f,  0.3f, 0.0f)
            };
        }
    }
}
