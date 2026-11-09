using System;
using System.IO;
using System.Collections.Generic;
using Raylib_cs;
using System.Numerics;

class App_main
{
    public static void Main()
    {
        Raylib.InitWindow(800, 600, "pong");
        Raylib.SetTargetFPS(60);
        int RY = 260;
        int CY = 260;
        Rectangle ball = new Rectangle(200, 100, 10,10);
        Vector2 speed = new Vector2(4, 3);
        Random rand = new();
        int pointsYou = 0;
        int pointsCPU = 0;
        bool pause = false;
        while (!Raylib.WindowShouldClose())
        {
            Rectangle racket = new Rectangle(4, RY, 10, 90);
            Rectangle CPU = new Rectangle(786, CY,10, 90);
            bool colision = false;
            bool Ccolision = false;
            if (!pause)
            {
                colision = Raylib.CheckCollisionRecs(racket, ball);
                Ccolision = Raylib.CheckCollisionRecs(CPU, ball);
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    RY -= rand.Next(2, 4);
                }
                if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    RY += rand.Next(2, 4);
                }
                int cpuCenter = CY + 45;
                if (cpuCenter > ball.Y + 5 && CY > 0)
                {
                    CY -= rand.Next(2, 4);
                }
                if (cpuCenter < ball.Y + 5 && CY < 600 - 90)
                {
                    CY += 2;
                }
                ball.Y += speed.Y;
                if (ball.X <= 0 || (ball.X + 10) >= 800)
                {
                    ball.X = 200;
                    ball.Y = 300;
                    speed.X *= -1;
                } else
                {
                    ball.X += speed.X;
                }
                if (ball.X <= 0)
                {
                    pointsCPU++;
                    ball.X = 200;
                    ball.Y = 300;
                    speed = new Vector2(4, 3);
                } else if ((ball.X + 10) >= 800)
                {
                    pointsYou++;
                    ball.X = 200;
                    ball.Y = 300;
                    speed = new Vector2(4, 3);
                }
                if (ball.Y <= 0 || (ball.Y + 10) >= 600)
                {
                    speed.Y *= -1;
                }
                if (colision)
                {
                    if (speed.X < 0)
                    {
                        speed.X *= -1.05f;
                        speed.Y *= 1.05f;
                        ball.X = racket.X + racket.Width;
                    }
                }
                if (Ccolision)
                {
                    if (speed.X > 0)
                    {
                        speed.X *= -1.05f;
                        speed.Y *= 1.05f;
                        ball.X = CPU.X - ball.Width;
                    }
                }
                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    pointsYou += 5;
                }
            }
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawRectangleRec(racket, Color.RayWhite); 
            Raylib.DrawRectangleRec(ball, Color.RayWhite);
            Raylib.DrawRectangleRec(CPU, Color.RayWhite);
            Raylib.DrawText(pointsYou.ToString(), 40, 150, 53, Color.RayWhite);
            Raylib.DrawText(pointsCPU.ToString(), 678, 150, 53, Color.RayWhite);
            if (pointsYou == 15)
            {
                pause = true;
                Raylib.DrawText("YOU WON!", 400, 300, 25, Color.Green);
            } else if (pointsCPU == 15)
            {
                pause = true;
                Raylib.DrawText("GAME OVER :(", 400, 300, 25, Color.Red);
            }
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}