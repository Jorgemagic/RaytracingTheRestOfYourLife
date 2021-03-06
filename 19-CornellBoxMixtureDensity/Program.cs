﻿using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace _19_CornellBoxMixtureDensity
{
    public class Program
    {
        public static Diffuse_light lightMaterial;

        public static Hitable_List Cornell_box(out Camera cam, float aspect)
        {
            Hitable_List objects = new Hitable_List();

            var red = new Lambertian(new Solid_Color(.65f, .05f, .05f));
            var white = new Lambertian(new Solid_Color(.73f, .73f, .73f));
            var green = new Lambertian(new Solid_Color(.12f, .45f, .15f));
            lightMaterial = new Diffuse_light(new Solid_Color(15, 15, 15));

            objects.Add(new Flip_face(new YZ_rect(0, 555, 0, 555, 555, green)));
            objects.Add(new YZ_rect(0, 555, 0, 555, 0, red));
            objects.Add(new Flip_face(new XZ_rect(213, 343, 227, 332, 554, lightMaterial)));
            objects.Add(new Flip_face(new XZ_rect(0, 555, 0, 555, 0, white)));
            objects.Add(new XZ_rect(0, 555, 0, 555, 555, white));
            objects.Add(new Flip_face(new XY_rect(0, 555, 0, 555, 555, white)));

            HitTable box1 = new Box(new Vector3(0, 0, 0), new Vector3(165, 330, 165), white);
            box1 = new Rotate_y(box1, 15);
            box1 = new Translate(box1, new Vector3(265, 0, 295));
            objects.Add(box1);

            HitTable box2 = new Box(new Vector3(0, 0, 0), new Vector3(165, 165, 165), white);
            box2 = new Rotate_y(box2, -18);
            box2 = new Translate(box2, new Vector3(130, 0, 65));
            objects.Add(box2);

            Vector3 lookfrom = new Vector3(278, 278, -800);
            Vector3 lookat = new Vector3(278, 278, 0);
            Vector3 vup = new Vector3(0, 1, 0);
            float dist_to_focus = 10.0f;
            float aperture = 0.0f;
            float vfov = 40.0f;
            float t0 = 0.0f;
            float t1 = 1.0f;

            cam = new Camera(lookfrom, lookat, vup, vfov, aspect, aperture, dist_to_focus, t0, t1);

            return objects;
        }

        static Vector3 Ray_color(Ray r, Vector3 background, HitTable world, int depth)
        {
            Hit_Record rec = default;
            // If we've exceeded the ray bounce limit, no more light is gathered.
            if (depth <= 0)
                return Vector3.Zero;

            // If the ray hits nothing, return the background color.
            if (!world.Hit(r, 0.001f, Helpers.Infinity, ref rec))
                return background;

            Ray scattered;
            Vector3 emitted = rec.Mat_ptr.Emitted(r, rec, rec.U, rec.V, rec.P);
            float pdf_val;
            Vector3 albedo;

            if (!rec.Mat_ptr.Scatter(r, rec, out albedo, out scattered, out pdf_val))
                return emitted;

            HitTable light_shape = new XZ_rect(213, 343, 227, 332, 554, lightMaterial);
            var p0 = new Hittable_pdf(light_shape, rec.P);
            var p1 = new Cosine_pdf(rec.Normal);
            Mixture_pdf p = new Mixture_pdf(p0, p1);

            scattered = new Ray(rec.P, p.Generate(), r.Time);
            pdf_val = p.Value(scattered.Direction);

            return emitted + albedo * rec.Mat_ptr.Scattering_pdf(r, rec, scattered) * Ray_color(scattered, background, world, depth - 1) / pdf_val;
        }

        static void Write_color(StreamWriter file, Vector3 pixel_color, int samples_per_pixel)
        {
            var r = pixel_color.X;
            var g = pixel_color.Y;
            var b = pixel_color.Z;

            // Divide the color total by the number of samples.
            float scale = 1.0f / samples_per_pixel;
            r = (float)Math.Sqrt(scale * r);
            g = (float)Math.Sqrt(scale * g);
            b = (float)Math.Sqrt(scale * b);
            // Write the translated [0,255] value of each color component.            
            file.WriteLine($"{(int)(256 * Math.Clamp(r, 0.0, 0.999))} {(int)(256 * Math.Clamp(g, 0.0, 0.999))} {(int)(256 * Math.Clamp(b, 0.0, 0.999))}");
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int image_width = 500;
            int image_height = image_width;
            float aspect_ratio = (float)image_width / image_height;
            int samples_per_pixel = 100;
            int max_depth = 50;

            string filePath = "image.ppm";

            using (var file = new StreamWriter(filePath))
            {
                file.WriteLine($"P3\n{image_width} {image_height}\n255");

                float viewport_height = 2.0f;
                float viewport_width = aspect_ratio * viewport_height;
                float focal_length = 1.0f;

                var origin = Vector3.Zero;
                var horizontal = new Vector3(viewport_width, 0, 0);
                var vertical = new Vector3(0, viewport_height, 0);
                var lower_left_corner = origin - horizontal / 2 - vertical / 2 - new Vector3(0, 0, focal_length);

                Hitable_List world = Cornell_box(out Camera cam, aspect_ratio);
                Vector3 background = new Vector3(0.0f, 0.0f, 0.0f);

                for (int j = image_height - 1; j >= 0; --j)
                {
                    Console.WriteLine($"\rScanlines remaining: {j}");
                    for (int i = 0; i < image_width; ++i)
                    {
                        Vector3 pixel_color = Vector3.Zero;
                        for (int s = 0; s < samples_per_pixel; ++s)
                        {
                            float u = (float)(i + Helpers.random.NextDouble()) / (image_width - 1);
                            float v = (float)(j + Helpers.random.NextDouble()) / (image_height - 1);
                            Ray r = cam.Get_Ray(u, v);
                            pixel_color += Ray_color(r, background, world, max_depth);
                        }
                        Write_color(file, pixel_color, samples_per_pixel);
                    }
                }

                Console.WriteLine("Done.");
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds,
                                                ts.Milliseconds);
            Console.WriteLine($"Time: {elapsedTime} hh:mm:ss:fff");
        }
    }
}