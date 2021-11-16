using System;
using GLFW;

namespace SharpEngine
{
    class Program
    {
        static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }

        static float GetRandomFloat(Random random, float min = 0, float max = 1)
        {
            return Lerp(min, max, (float) random.Next() / int.MaxValue);
        }

        static void Main(string[] args)
        {

            var window = new Window();
            var material = new Material("shaders/world-position-color.vert", "shaders/vertex-color.frag");
            var scene = new Scene();
            window.Load(scene);

            var shape = new Triangle(material);
            shape.Transform.CurrentScale = new Vector(0.5f, 1f, 1f);
            scene.Add(shape);

            var ground = new Rectangle(material);
            ground.Transform.CurrentScale = new Vector(10f, 1f, 1f);
            ground.Transform.Position = new Vector(0f, -1f);
            scene.Add(ground);

            // engine rendering loop
            const int fixedStepNumberPerSecond = 30;
            const float fixedStepDuration = 1.0f / fixedStepNumberPerSecond;
            
            double previousFixedStep = 0.0;
            while (window.IsOpen()) {
                while (Glfw.Time > previousFixedStep + fixedStepDuration) {
                    previousFixedStep += fixedStepDuration;

                    if (window.GetKey(Keys.W)) {
                        shape.Transform.Position += new Vector(0f, 0.5f * fixedStepDuration);
                    }
                    if (window.GetKey(Keys.S)) {
                        shape.Transform.Position += new Vector(0f, -0.5f * fixedStepDuration);
                    }
                    if (window.GetKey(Keys.A)) {
                        shape.Transform.Position += new Vector(-0.5f * fixedStepDuration, 0f);
                    }
                    if (window.GetKey(Keys.D)) {
                        shape.Transform.Position += new Vector(0.5f * fixedStepDuration, 0f);
                    }
                }
                window.Render();
            }
        }
    }
} 
