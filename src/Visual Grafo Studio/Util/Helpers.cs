using QuickGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Visual_Grafo_Studio.Util
{
    class Helpers
    {
        static GraphToImage g = new GraphToImage();
        public static GraphToImage GraphToImage { get { return g; } set { g = value; } }

        public static BidirectionalGraph<object, IEdge<object>> ConvertToGraphSharp(List<Vertice> grafo)
        {
            BidirectionalGraph<object, IEdge<object>>  gr = new BidirectionalGraph<object, IEdge<object>>();
            foreach (Vertice tv in grafo)
            {
                gr.AddVertex(tv);
            }
            foreach (Vertice tv in grafo)
            {
                foreach (tAresta ta in tv.tAdjascencias)
                {
                    gr.AddEdge(new TaggedEdge<object, object>(tv, ta.vertice, ta.peso.ToString()));
                }
            }
            return gr;
        }
        public static BidirectionalGraph<object, IEdge<object>> CopyGraphSharp(BidirectionalGraph<object, IEdge<object>> graph)
        {
            BidirectionalGraph<object, IEdge<object>> gr = new BidirectionalGraph<object, IEdge<object>>();
            foreach (Vertice v in graph.Vertices)
            {
                gr.AddVertex(v);
            }
            foreach (TaggedEdge<object, object> o in graph.Edges)
            {
                gr.AddEdge(o);
            }
            return gr;
        }
    }
    class GraphToImage
    {
        //Set resolution of image.
        const double dpi = 96d;

        //Set pixelformat of image.
        PixelFormat pixelFormat = PixelFormats.Pbgra32;

        /// <summary>
        /// Method exports the graphlayout to an png image.
        /// </summary>
        /// <param name="path">destination of image</param>
        /// <param name="surface">graphlayout you want to print</param>
        public void ExportToPng(GraphSharp.Controls.GraphLayout surface, Uri path)
        {
            //Save current canvas transform
            Transform transform = surface.LayoutTransform;

            //Reset current transform (in case it is scaled or rotated)
            surface.LayoutTransform = null;

            //Get the size of canvas
            Size size = new Size(surface.ActualWidth, surface.ActualHeight);

            //Measure and arrange the surface
            //VERY IMPORTANT
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            //Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(800,600,
                300,
                300,
                pixelFormat);

            //Render the graphlayout onto the bitmap.
            renderBitmap.Render(surface);

            //Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                //Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();

                //Push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                //Save the data to the stream
                encoder.Save(outStream);
            }

            //Restore previously saved layout
            surface.LayoutTransform = transform;
        }
    }
}
