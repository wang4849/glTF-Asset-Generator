using System;
using System.Collections.Generic;
using System.Numerics;

namespace AssetGenerator
{
    internal class Mesh_Primitives : ModelGroup
    {
        public override ModelGroupId Id => ModelGroupId.Mesh_Primitives;

        public Mesh_Primitives(List<string> imageList)
        {
            UseFigure(imageList, "Indices_Primitive0");
            UseFigure(imageList, "Indices_Primitive1");

            // Track the common properties for use in the readme.
            var baseColorFactorGreen = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
            var baseColorFactorBlue = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
            var baseColorFactorWhite = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            var baseColorFactorBlack = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            CommonProperties.Add(new Property(PropertyName.Material0WithBaseColorFactor, baseColorFactorGreen));
            CommonProperties.Add(new Property(PropertyName.Material1WithBaseColorFactor, baseColorFactorBlue));
            CommonProperties.Add(new Property(PropertyName.Material1WithBaseColorFactor, baseColorFactorWhite));
            CommonProperties.Add(new Property(PropertyName.Material1WithBaseColorFactor, baseColorFactorBlack));

            Model CreateModel(Action<List<Property>, Runtime.MeshPrimitive, Runtime.MeshPrimitive> setProperties)
            {
                var properties = new List<Property>();
                List<Runtime.MeshPrimitive> meshPrimitives = MeshPrimitive.CreateMultiPrimitivePlane(includeTextureCoords: false);

                // Apply the properties that are specific to this gltf.
                setProperties(properties, meshPrimitives[0], meshPrimitives[1]);

                // Create the gltf object
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
                                    MeshPrimitives = meshPrimitives
                                },
                            },
                        },
                    }),
                };
            }

            void SetPrimitiveZeroGreen(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        BaseColorFactor = baseColorFactorGreen
                    }
                };

                properties.Add(new Property(PropertyName.Primitive0, "Material 0"));
            }

            void SetPrimitiveOneBlue(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        BaseColorFactor = baseColorFactorBlue
                    }
                };

                properties.Add(new Property(PropertyName.Primitive1, "Material 1"));
            }
            // New color
            void SetPrimitiveZeroWhite(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        BaseColorFactor = baseColorFactorWhite
                    }
                };

                properties.Add(new Property(PropertyName.Primitive0, "Material 0"));
            }

            void SetPrimitiveOneBlack(List<Property> properties, Runtime.MeshPrimitive meshPrimitive)
            {
                meshPrimitive.Material = new Runtime.Material
                {
                    MetallicRoughnessMaterial = new Runtime.PbrMetallicRoughness
                    {
                        BaseColorFactor = baseColorFactorBlack
                    }
                };

                properties.Add(new Property(PropertyName.Primitive1, "Material 1"));
            }

            Models = new List<Model>
            {
                CreateModel((properties, meshPrimitiveZero, meshPrimitiveOne) =>
                {
                    SetPrimitiveZeroGreen(properties, meshPrimitiveZero);
                    SetPrimitiveOneBlue(properties, meshPrimitiveOne);
                }),
                // New model
                CreateModel((properties, meshPrimitiveZero, meshPrimitiveOne) =>
                {
                    SetPrimitiveZeroWhite(properties, meshPrimitiveZero);
                    SetPrimitiveOneBlack(properties, meshPrimitiveOne);
                }),
            };

            GenerateUsedPropertiesList();
        }
    }
}
