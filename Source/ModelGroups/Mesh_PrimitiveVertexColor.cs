﻿using System;
using System.Collections.Generic;
using System.Numerics;
using static AssetGenerator.Runtime.MeshPrimitive;

namespace AssetGenerator
{
    internal class Mesh_PrimitiveVertexColor : ModelGroup
    {
        public override ModelGroupId Id => ModelGroupId.Mesh_PrimitiveVertexColor;

        public Mesh_PrimitiveVertexColor(List<string> imageList)
        {
            // There are no common properties in this model group that are reported in the readme.
            var vertexColors = new List<Vector4>
            {
                new Vector4(0.0f, 1.0f, 0.0f, 0.2f),
                new Vector4(1.0f, 0.0f, 0.0f, 0.2f),
                new Vector4(1.0f, 1.0f, 0.0f, 0.2f),
                new Vector4(0.0f, 0.0f, 1.0f, 0.2f),
            };

            Model CreateModel(Action<List<Property>, Runtime.MeshPrimitive> setProperties)
            {
                var properties = new List<Property>();
                Runtime.MeshPrimitive meshPrimitive = MeshPrimitive.CreateSinglePlane(includeTextureCoords: false);

                // Apply the common properties to the gltf. 
                meshPrimitive.Colors = vertexColors;

                // Apply the properties that are specific to this gltf.
                setProperties(properties, meshPrimitive);

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

            void SetVertexColorVec3Float(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.FLOAT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;

                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Float"));
            }

            void SetVertexColorVec3Byte(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_UBYTE;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;

                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Byte"));
            }

            void SetVertexColorVec3Short(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_USHORT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;

                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Short"));
            }

            void SetVertexColorVec4Float(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.FLOAT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC4;

                properties.Add(new Property(PropertyName.VertexColor, "Vector4 Float"));
            }

            void SetVertexColorVec4Byte(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_UBYTE;
                meshPrimitive.ColorType = ColorTypeEnum.VEC4;

                properties.Add(new Property(PropertyName.VertexColor, "Vector4 Byte"));
            }

            void SetVertexColorVec4Short(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_USHORT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC4;

                properties.Add(new Property(PropertyName.VertexColor, "Vector4 Short"));
            }

            Models = new List<Model>
            {
                CreateModel((properties, meshPrimitive) =>
                {
                    SetVertexColorVec3Float(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetVertexColorVec3Byte(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetVertexColorVec3Short(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetVertexColorVec4Float(properties, meshPrimitive);
                }),
                CreateModel((properties,meshPrimitive) =>
                {
                    SetVertexColorVec4Byte(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetVertexColorVec4Short(properties, meshPrimitive);
                }),
            };

            GenerateUsedPropertiesList();
        }
    }
}
