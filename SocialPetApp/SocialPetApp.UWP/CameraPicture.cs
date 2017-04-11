using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace SocialPetApp.UWP
{
    public class CameraPicture
    {
        MediaCapture _mediaCapture;
        bool _isPreviewing;
        public BitmapImage bmp;

        public CameraPicture()
        {
            _mediaCapture = new MediaCapture();
            
        }

        public void takePicture()
        {
            createMediaCapture();
        }

        async private void createMediaCapture()
        {
            await _mediaCapture.InitializeAsync();
            var lowLagCapture = await _mediaCapture.PrepareLowLagPhotoCaptureAsync(ImageEncodingProperties.CreateUncompressed(MediaPixelFormat.Bgra8));

            var capturedPhoto = await lowLagCapture.CaptureAsync();
            var softwareBitmap = capturedPhoto.Frame.SoftwareBitmap;
            
            await lowLagCapture.FinishAsync();
        }

    }
}
