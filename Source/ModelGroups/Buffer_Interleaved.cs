﻿using System;
using System.Collections.Generic;
using System.Numerics;
using static AssetGenerator.Runtime.MeshPrimitive;

namespace AssetGenerator
{
    internal class Buffer_Interleaved : ModelGroup
    {
        public override ModelGroupId Id => ModelGroupId.Buffer_Interleaved;

        public Buffer_Interleaved(List<string> imageList)
        {
            Runtime.Image baseColorTextureImage = UseTexture(imageList, "BaseColor_Grey");

            // Track the common properties for use in the readme.
            CommonProperties.Add(new Property(PropertyName.BaseColorTexture, baseColorTextureImage));

            Model CreateModel(Action<List<Property>, Runtime.MeshPrimitive> setProperties)
            {
                var properties = new List<Property>();
                Runtime.MeshPrimitive meshPrimitive = MeshPrimitive.CreateSinglePlane();

                // Apply the common properties to the gltf.
                meshPrimitive.Interleave = true;
                meshPrimitive.Colors = new[]
                {
                    new Vector4(0.0f, 1.0f, 0.0f, 0.2f),
                    new Vector4(1.0f, 0.0f, 0.0f, 0.2f),
                    new Vector4(1.0f, 1.0f, 0.0f, 0.2f),
                    new Vector4(0.0f, 0.0f, 1.0f, 0.2f),
                };
                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        BaseColorTexture = new Runtime.Texture
                        {
                            Source = baseColorTextureImage,
                            Sampler = new Runtime.Sampler(),
                        },
                    },
                };

                // Apply the properties that are specific to this gltf.
                setProperties(properties, meshPrimitive);

                // Create the gltf object.
                return new Model
                {
                    Properties = properties,
                    GLTF = CreateGLTF(() => new Runtime.Scene
                    {
                        Nodes = new[]
                        {
                            new Runtime.Node
                            {
                                Mesh = new Runtime.Mesh
                                {
                                    MeshPrimitives = new[]
                                    {
                                        meshPrimitive
                                    }
                                },
                            },
                        },
                    }),
                };
            }

            void SetUvTypeFloat(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.TextureCoordsComponentType = TextureCoordsComponentTypeEnum.FLOAT;
                properties.Add(new Property(PropertyName.VertexUV0, "Float"));
            }

            void SetUvTypeTypeByte(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.TextureCoordsComponentType = TextureCoordsComponentTypeEnum.NORMALIZED_UBYTE;
                properties.Add(new Property(PropertyName.VertexUV0, "Byte"));
            }

            void SetUvTypeTypeShort(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.TextureCoordsComponentType = TextureCoordsComponentTypeEnum.NORMALIZED_USHORT;
                properties.Add(new Property(PropertyName.VertexUV0, "Short"));
            }

            void SetColorTypeFloat(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.FLOAT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;
                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Float"));
            }

            void SetColorTypeByte(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_UBYTE;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;
                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Byte"));
            }

            void SetColorTypeShort(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.ColorComponentType = ColorComponentTypeEnum.NORMALIZED_USHORT;
                meshPrimitive.ColorType = ColorTypeEnum.VEC3;
                properties.Add(new Property(PropertyName.VertexColor, "Vector3 Short"));
            }

            Models = new List<Model>
            {
                CreateModel((properties, meshPrimitive) =>
                {
                    SetUvTypeFloat(properties, meshPrimitive);
                    SetColorTypeFloat(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetUvTypeFloat(properties, meshPrimitive);
                    SetColorTypeByte(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetUvTypeFloat(properties, meshPrimitive);
                    SetColorTypeShort(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetUvTypeTypeByte(properties, meshPrimitive);
                    SetColorTypeFloat(properties, meshPrimitive);
                }),
                CreateModel((properties, meshPrimitive) =>
                {
                    SetUvTypeTypeShort(properties, meshPrimitive);
                    SetColorTypeFloat(properties, meshPrimitive);
                }),
            };

            GenerateUsedPropertiesList();
        }
    }
}
