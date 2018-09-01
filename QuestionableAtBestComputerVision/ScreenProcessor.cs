using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace QuestionableAtBestComputerVision {
	internal sealed class ScreenProcessor {

		internal Rectangle Region {
			private get; set;
		}

		internal ILayerProcessor<object>[] Processors {
			set {
				processors = value;
				results = new LayerProcessResult<object>[
					value.Length
				];
			}
		}
		private ILayerProcessor<object>[] processors;
		private LayerProcessResult<object>[] results;

		internal Bitmap GetPresentationBitmap(int processorIndex) {

			ILayerProcessor<object> processor = processors[
				processorIndex
			];

			LayerProcessResult<object> result = results[
				processorIndex
			];

			return ConvertPixelMap(
				processor.GetPresentationColorMap(result)
			);

		}

		private static Bitmap ConvertPixelMap(in Color[,] pixelMap) {
			Bitmap bitmap = new Bitmap(
				pixelMap.GetLength(0),
				pixelMap.GetLength(1)
			);
			for(int x = 0;x<bitmap.Width;x++) {
			for(int y = 0;y<bitmap.Height;y++) {
				bitmap.SetPixel(
					x,y,pixelMap[x,y]
				);
			}}
			return bitmap;
		}
		private static Color[,] ConvertBitmap(in Bitmap bitmap) {
			Color[,] pixelMap = new Color[
				bitmap.Width,
				bitmap.Height
			];
			for(int x = 0;x<bitmap.Width;x++) {
			for(int y = 0;y<bitmap.Height;x++) {
				pixelMap[x,y] = bitmap.GetPixel(x,y);
			}}
			return pixelMap;
		}
		private Color[,] CaptureScreen() {
			using(Bitmap bitmap = new Bitmap(
				Region.Width,
				Region.Height
			)) {
				using(Graphics graphics = Graphics.FromImage(bitmap)) {
					graphics.CopyFromScreen(
						Region.Location,
						Point.Empty,
						Region.Size
					);
				}
				return ConvertBitmap(bitmap);
			}
		}
	}
}
