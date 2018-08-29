using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionableAtBestComputerVision {

	internal readonly struct LayerProcessResult<ResultType> {

		internal LayerProcessResult(Color[,] pixelMap,ResultType result) {
			PixelMap = pixelMap;
			Result = result;
		}

		readonly Color[,] PixelMap;
		readonly ResultType Result;

	}

	internal interface ILayerProcessor<ResultType> {

		LayerProcessResult<ResultType> Process(in Color[,] pixelMap);

		//Be sure to cast ResultType from a generic object
		Color[,] GetPresentationColorMap(in LayerProcessResult<ResultType> result);
	}

}
