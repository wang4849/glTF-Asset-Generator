﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AssetGenerator
{
    /// <summary>
    /// A model group is a collection of glTF models that share a property or series of properties in common.
    /// This abstract class contains several helper functions for using images and creating base models.
    /// Inherit from this class to create a new model group.
    /// </summary>
    internal abstract partial class ModelGroup
    {
        public abstract ModelGroupId Id { get; }
        public List<Model> Models { get; protected set; }
        public List<Property> CommonProperties { get; private set; }
        public List<Property> Properties { get; private set; }
        public List<Runtime.Image> UsedTextures { get; private set; }
        public List<Runtime.Image> UsedFigures { get; private set; }
        public bool NoSampleImages { get; protected set; }

        protected ModelGroup()
        {
            CommonProperties = new List<Property>();
            Properties = new List<Property>();
            UsedTextures = new List<Runtime.Image>();
            UsedFigures = new List<Runtime.Image>();
            NoSampleImages = false;
        }

        protected Runtime.Image UseTexture(List<string> imageList, string name)
        {
            Runtime.Image image = GetImage(imageList, name);
            UsedTextures.Add(image);

            return image;
        }

        protected void UseFigure(List<string> imageList, string name)
        {
            UsedFigures.Add(GetImage(imageList, name));
        }

        private Runtime.Image GetImage(List<string> imageList, string name)
        {
            var image = new Runtime.Image
            {
                Uri = imageList.Find(e => e.Contains(name))
            };

            return image;
        }

        /// <summary>
        /// Creates a glTF object.
        /// </summary>
        /// /// <returns>Runtime glTF object with Asset values set.</returns>
        protected static Runtime.GLTF CreateGLTF(Func<Runtime.Scene> createScene, List<Runtime.Animation> animations = null, List<string> extensionsUsed = null, List<string> extensionsRequired = null)
        {
            return new Runtime.GLTF
            {
                Animations = animations,
                Asset = new Runtime.Asset
                {
                    Generator = "glTF Asset Generator",
                    Version = "2.0",
                },
                Scenes = new List<Runtime.Scene>
                {
                    createScene(),
                },
                ExtensionsUsed = extensionsUsed,
                ExtensionsRequired = extensionsRequired,
            };
        }

        protected static partial class MeshPrimitive
        {
            public static Runtime.MeshPrimitive CreateSinglePlane(bool includeTextureCoords = true, bool includeIndices = true, bool includeNormals = false, bool includeTangents = false)
            {
                List<List<Vector2>> textureCoords = includeTextureCoords ? GetSinglePlaneTextureCoordSets() : null;
                List<int> indices = includeIndices ? GetSinglePlaneIndices() : null;
                List<Vector3> normals = includeNormals ? GetSinglePlaneNormals() : null;
                List<Vector4> tangents = includeTangents ? GetSinglePlaneTangents() : null;

                return new Runtime.MeshPrimitive
                {
                    Positions = GetSinglePlanePositions(),
                    TextureCoordSets = textureCoords,
                    Indices = indices,
                    Normals = normals,
                    Tangents = tangents
                };
            }

            public static List<Vector3> GetSinglePlanePositions()
            {
                return new List<Vector3>
                {
                    new Vector3( 0.5f, -0.5f, 0.0f),
                    new Vector3(-0.5f, -0.5f, 0.0f),
                    new Vector3(-0.5f,  0.5f, 0.0f),
                    new Vector3( 0.5f,  0.5f, 0.0f)
                };
            }

            public static List<List<Vector2>> GetSinglePlaneTextureCoordSets()
            {
                return new List<List<Vector2>>
                {
                    new List<Vector2>
                    {
                        new Vector2(1.0f, 1.0f),
                        new Vector2(0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f),
                        new Vector2(1.0f, 0.0f)
                    },
                };
            }

            public static List<int> GetSinglePlaneIndices()
            {
                return new List<int>
                {
                    1, 0, 3,
                    1, 3, 2
                };
            }

            public static List<Vector3> GetSinglePlaneNormals()
            {
                return new List<Vector3>
                {
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f)
                };
            }

            public static List<Vector4> GetSinglePlaneTangents()
            {
                return new List<Vector4>
                {
                    new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    new Vector4(1.0f, 0.0f, 0.0f, 1.0f)
                };
            }

            public static List<Runtime.MeshPrimitive> CreateMultiPrimitivePlane(bool includeTextureCoords = true)
            {
                return new List<Runtime.MeshPrimitive>
                {
                    new Runtime.MeshPrimitive
                    {
                        Positions = new List<Vector3>
                        {
                            new Vector3(-0.5f, -0.5f, 0.0f),
                            new Vector3( 0.5f,  0.5f, 0.0f),
                            new Vector3(-0.5f,  0.5f, 0.0f)
                        },
                        TextureCoordSets = includeTextureCoords ? new List<List<Vector2>>
                        {
                            new List<Vector2>
                            {
                                new Vector2(0.0f, 1.0f),
                                new Vector2(1.0f, 0.0f),
                                new Vector2(0.0f, 0.0f)
                            },
                        } : null,
                        Indices = new List<int>
                        {
                            0, 1, 2,
                        },
                    },

                    new Runtime.MeshPrimitive
                    {
                        Positions = new List<Vector3>
                        {
                            new Vector3(-0.5f, -0.5f, 0.0f),
                            new Vector3( 0.5f, -0.5f, 0.0f),
                            new Vector3( 0.5f,  0.5f, 0.0f)
                        },
                        TextureCoordSets = includeTextureCoords ? new List<List<Vector2>>
                        {
                            new List<Vector2>
                            {
                                new Vector2(0.0f, 1.0f),
                                new Vector2(1.0f, 1.0f),
                                new Vector2(1.0f, 0.0f)
                            },
                        } : null,
                        Indices = new List<int>
                        {
                            0, 1, 2,
                        },
                    }
                };
            }
        }

        /// <summary>
        /// Creates a sorted list with each unique property used by the model group.
        /// </summary>
        protected void GenerateUsedPropertiesList()
        {
            foreach (var model in Models)
            {
                Properties = Properties.Union(model.Properties).ToList();
            }

            SortPropertiesList(CommonProperties);
            SortPropertiesList(Properties);
        }

        /// <summary>
        /// Sorts the list so every readme has the same column order, determined by enum value.
        /// </summary>
        protected void SortPropertiesList(List<Property> properties)
        {
            if (properties.Count > 0)
            {
                properties.Sort((x, y) => x.Name.CompareTo(y.Name));
            }
        }
    }

    /// <summary>
    /// glTF model and related metadata.
    /// </summary>
    internal class Model
    {
        public List<Property> Properties;
        public Runtime.GLTF GLTF;
        public Action<glTFLoader.Schema.Gltf> PostRuntimeChanges;
        public Func<Type, object> CreateSchemaInstance = Activator.CreateInstance;
        public Manifest.Camera Camera = null;
        public bool Animated = false;
        public bool? Loadable = true;
    }

    /// <summary>
    /// The model group enum value associated with a model group should never change after a release
    /// to avoid changes to the asset urls in the README files
    /// </summary>
    internal enum ModelGroupId
    {
        Animation_Node = 0,
        Animation_NodeMisc = 1,
        Animation_Skin = 2,
        Animation_SkinType = 3,
        Buffer_Interleaved = 4,
        Compatibility = 5,
        Material = 6,
        Material_AlphaBlend = 7,
        Material_AlphaMask = 8,
        Material_DoubleSided = 9,
        Material_MetallicRoughness = 10,
        Material_Mixed = 11,
        Material_SpecularGlossiness = 12,
        Mesh_PrimitiveAttribute = 13,
        Mesh_PrimitiveMode = 14,
        Mesh_PrimitiveRestart = 15,
        Mesh_PrimitiveVertexColor = 16,
        Mesh_Primitives = 17,
        Mesh_PrimitivesUV = 18,
        Node_Attribute = 19,
        Node_NegativeScale = 20,
        Texture_Sampler = 21,
        Mesh_NoPosition = 22,
        Animation_SamplerType = 23,
        Instancing = 24,
        Material_AlphaMaskTraining = 25,
    }
}
