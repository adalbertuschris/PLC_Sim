using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Block_sorting_system
{
    public class Spritebatch
    {
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin, RectangleF? sourceRec = null)
        {
            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(0,1)
            };

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 4; i++)
            {
                if (sourceRec == null)
                {
                    GL.TexCoord2(vertices[i]);
                }
                else
                {
                    GL.TexCoord2((sourceRec.Value.Left + vertices[i].X * sourceRec.Value.Width) / texture.Width,
                       (sourceRec.Value.Top + vertices[i].Y * sourceRec.Value.Height) / texture.Height);
                }

                vertices[i].X *= (sourceRec == null) ? texture.Width : sourceRec.Value.Width;
                vertices[i].Y *= (sourceRec == null) ? texture.Height : sourceRec.Value.Height;
                //vertices[i] -= origin;
                //vertices[i] *= scale;
                vertices[i] += position;

                GL.Vertex2(vertices[i]);
            }
            GL.End();

        }

        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0f, screenWidth, screenHeight, 0f, 0f, 1f);
        }
    }
}
