﻿using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

using _3d.UI;

namespace _3d
{
    class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings nws = new NativeWindowSettings() {
                Title = "Hello",
                Size = new Vector2i(1000,720),
                StartFocused = true,
                StartVisible = true,
                APIVersion = new Version(3,2),
                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,
            };
            GameWindowSettings gws = new GameWindowSettings();
            new Game(gws,nws);
        }
    }

    class Game : GameWindow {

        Tile tile;
        Camera camera;
        UserInterface userInterface;

        List<float> vertices = new List<float>();
        List<uint> indices = new List<uint>();
        int mapResolution = 100, id = 0;
        float[] colors = {1,1,1};
        float tileSize = 20;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings,nativeWindowSettings) {
            
            for (int x = -(mapResolution/2); x < mapResolution/2; x++) {
                for (int y = -(mapResolution/2); y < mapResolution/2; y++) {

                    vertices.Add((-.5f+x)*tileSize);
                    vertices.Add( .5f);
                    vertices.Add(( .5f+y)*tileSize);

                    vertices.Add(colors[0]);
                    vertices.Add(colors[1]);
                    vertices.Add(colors[2]);

                    vertices.Add(( .5f+x)*tileSize);
                    vertices.Add( .5f);
                    vertices.Add(( .5f+y)*tileSize);

                    vertices.Add(colors[0]);
                    vertices.Add(colors[1]);
                    vertices.Add(colors[2]);

                    vertices.Add(( .5f+x)*tileSize);
                    vertices.Add( .5f);
                    vertices.Add((-.5f+y)*tileSize);

                    vertices.Add(colors[0]);
                    vertices.Add(colors[1]);
                    vertices.Add(colors[2]);

                    vertices.Add((-.5f+x)*tileSize);
                    vertices.Add( .5f);
                    vertices.Add((-.5f+y)*tileSize);

                    vertices.Add(colors[0]);
                    vertices.Add(colors[1]);
                    vertices.Add(colors[2]);

                    indices.Add((uint)id*4);
                    indices.Add((uint)id*4+1);
                    indices.Add((uint)id*4+2);
                    indices.Add((uint)id*4);
                    indices.Add((uint)id*4+2);
                    indices.Add((uint)id*4+3);
                    id++;
                }
            }

            Run();
        }
        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            tile.Render();
            userInterface.Render();
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);
        }

        protected override void OnLoad() {
            camera = new Camera(this);
            userInterface = new UserInterface(.3f, .4f, UserInterface.PositionType.Left, camera);
            tile = new Tile(camera, vertices, indices, mapResolution);

            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0,0,0,1.0f);
        }
    }
}
